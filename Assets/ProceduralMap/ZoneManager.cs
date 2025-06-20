using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ZoneDir
{
    None,
    Right,
    Left,
    Up,
    Down
}

[System.Serializable]
public class ZoneTypeProfiles
{
    public ZoneType zoneType;
    public ZoneLayoutProfile zoneLayoutProfile;
}

public class ZoneManager : MonoBehaviour
{
    public static readonly Vector2Int[] AllDirections = new Vector2Int[]
{
    Vector2Int.right,
    Vector2Int.left,
    Vector2Int.up,
    Vector2Int.down,
    new Vector2Int(1, 1),
    new Vector2Int(1, -1),
    new Vector2Int(-1, 1),
    new Vector2Int(-1, -1),
};

    private GameObject player;

    private int zoneSize = 40;
    private int halfZoneSize;
    private float zoneBuffer = 10f;

    [SerializeField] private GameObject zonePrefab;
    [SerializeField] private GameObject mainGrid;

    [SerializeField] private ZoneConfig zoneConfig;

    [SerializeField]
    public Dictionary<Vector2Int, ZoneData> generatedZones = new Dictionary<Vector2Int, ZoneData>();


    [SerializeField] private List<ZoneTypeProfiles> zoneTypeProfiles;

    private Dictionary<ZoneType, ZoneLayoutProfile> zoneLayoutProfiles;

    public Dictionary<ZoneType, ZoneLayoutProfile> ZoneLayoutProfiles { get => zoneLayoutProfiles; }

    private void Awake()
    {
        zoneLayoutProfiles = new Dictionary<ZoneType, ZoneLayoutProfile>();
        foreach (var profile in zoneTypeProfiles)
        {
            if (!ZoneLayoutProfiles.ContainsKey(profile.zoneType)) ZoneLayoutProfiles.Add(profile.zoneType, profile.zoneLayoutProfile);
        }
        halfZoneSize = zoneSize / 2;
    }

    private void Start()
    {
        player = GameManager.Instance.Player;

        TryGenerateZone((Vector2Int.zero), ZoneDir.None);
    }

  

    private void Update()
    {
        CheckForPlayerEdgeProximity();
    }

    private Vector2Int FindCurrentPlayerZoneCoord()
    {
        Vector3 pos = player.transform.position;
        Vector2Int currentZoneCoord = new Vector2Int(Mathf.FloorToInt((pos.x + halfZoneSize) / zoneSize), Mathf.FloorToInt((pos.y + halfZoneSize) / zoneSize));
     
        return currentZoneCoord;
    }

    private Vector2 FindZoneCenterPosition(Vector2Int centerCoord)
    {
        return new Vector2(centerCoord.x * zoneSize, centerCoord.y * zoneSize);
    }
    private void CheckForPlayerEdgeProximity()
    {
        Vector2 currentZoneCenter = FindZoneCenterPosition(FindCurrentPlayerZoneCoord());

        Vector2 dist = (Vector2)player.transform.position - currentZoneCenter;
        if (Mathf.Abs(dist.x) > zoneBuffer && dist.x > 0) ExpandZones(ZoneDir.Right);
        if (Mathf.Abs(dist.x) > zoneBuffer && dist.x < 0) ExpandZones(ZoneDir.Left);
        if (Mathf.Abs(dist.y) > zoneBuffer && dist.y > 0) ExpandZones(ZoneDir.Up);
        if (Mathf.Abs(dist.y) > zoneBuffer && dist.y < 0) ExpandZones(ZoneDir.Down);

    }

    private void ExpandZones(ZoneDir dir)
    {
        Vector2Int currentZoneCoord = FindCurrentPlayerZoneCoord();

        switch (dir)
        {
            case ZoneDir.Right:
                TryGenerateZone(currentZoneCoord + new Vector2Int(1, 0), ZoneDir.Left);
                TryGenerateZone(currentZoneCoord + new Vector2Int(1, 1), ZoneDir.Left);
                TryGenerateZone(currentZoneCoord + new Vector2Int(1, -1), ZoneDir.Left);
                break;
            case ZoneDir.Left:
                TryGenerateZone(currentZoneCoord + new Vector2Int(-1, 0), ZoneDir.Right);
                TryGenerateZone(currentZoneCoord + new Vector2Int(-1, 1), ZoneDir.Right);
                TryGenerateZone(currentZoneCoord + new Vector2Int(-1, -1), ZoneDir.Right);
                break;
            case ZoneDir.Up:
                TryGenerateZone(currentZoneCoord + new Vector2Int(0, 1), ZoneDir.Down);
                TryGenerateZone(currentZoneCoord + new Vector2Int(1, 1), ZoneDir.Down);
                TryGenerateZone(currentZoneCoord + new Vector2Int(-1, 1), ZoneDir.Down);
                break;
            case ZoneDir.Down:
                TryGenerateZone(currentZoneCoord + new Vector2Int(0, -1), ZoneDir.Up);
                TryGenerateZone(currentZoneCoord + new Vector2Int(1, -1), ZoneDir.Up);
                TryGenerateZone(currentZoneCoord + new Vector2Int(-1, -1), ZoneDir.Up);
                break;
        }
    }

    private void TryGenerateZone(Vector2Int centerCoord, ZoneDir expansionDir)
    {
        if (!generatedZones.ContainsKey(centerCoord))
        {
            Vector3 newZonePos = FindZoneCenterPosition(centerCoord);         
            ZoneType zoneType = GenerateZoneType(centerCoord);

            GameObject zoneGO = CreateZone(centerCoord, expansionDir, zoneType, newZonePos);
            zoneGO.transform.parent = mainGrid.transform;
        }
    }

    private GameObject CreateZone(Vector2Int centerCoord, ZoneDir dir, ZoneType zoneType, Vector3 pos)
    {
        ZoneLayoutProfile layoutProfile = zoneLayoutProfiles[zoneType];
        GameObject zone = Instantiate(zonePrefab, pos, Quaternion.identity);
        generatedZones.Add(centerCoord, new ZoneData(centerCoord, FindZoneCenterPosition(centerCoord), zone, dir, zoneType, layoutProfile));

       
        switch (zoneType)
        {
            case ZoneType.graveYard:
                GraveyardHandler zoneHandler;
                zoneHandler = zone.AddComponent<GraveyardHandler>();
                zoneHandler.ZoneData = generatedZones[centerCoord];
                zoneHandler.ZoneConfig = zoneConfig;
                zoneHandler.ZoneLayoutProfile = layoutProfile;

                GameObject boundsTilemapGO = Instantiate(layoutProfile.boundsTilemapGO, zoneHandler.transform.position, Quaternion.identity);
                boundsTilemapGO.transform.parent = zoneHandler.transform;
                zoneHandler.BoundsTilemap = boundsTilemapGO.GetComponent<Tilemap>();
                break;
            case ZoneType.plain:
                ZoneHandler zoneHandler1;
                zoneHandler1 = zone.AddComponent<ZoneHandler>();
                zoneHandler1.ZoneData = generatedZones[centerCoord];
                zoneHandler1.ZoneConfig = zoneConfig;
                zoneHandler1.ZoneLayoutProfile = layoutProfile;

                GameObject boundsTilemapGO1 = Instantiate(layoutProfile.boundsTilemapGO, zoneHandler1.transform.position, Quaternion.identity);
                boundsTilemapGO1.transform.parent = zoneHandler1.transform;
                
                break;
        }

        return zone;
    }
    private ZoneType GenerateZoneType(Vector2Int centerCoord)
    {
        //ZoneType[] availableTypes = new ZoneType[]
        //{
        //    ZoneType.plain,
        //    ZoneType.graveYard,
        //    //ZoneType.forest
        //};

        //ZoneType randomZoneType = availableTypes[Random.Range(0, availableTypes.Length)];
        //if (randomZoneType == ZoneType.plain) return ZoneType.plain;
        //else
        //{
        //    bool temp = true;
        //    foreach (var dir in AllDirections)
        //    {
        //        Vector2Int neighborCoord = centerCoord + dir;

        //        if (generatedZones.TryGetValue(neighborCoord, out ZoneData neighborZone))
        //        {
        //            if (neighborZone.zoneType == randomZoneType)
        //            {
        //                temp = false;
        //            }
        //        }
        //    }
        //    if (temp == true) return randomZoneType;
        //    else return ZoneType.plain;

        return ZoneType.graveYard;
    }

    }

    



