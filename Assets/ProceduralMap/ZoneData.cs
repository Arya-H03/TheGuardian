using UnityEngine;

[System.Serializable]
public class ZoneData
{
    public Vector2Int centerCoord;
    public Vector2Int centerPos;
    public GameObject zoneGO;
    public DirectionEnum previousZoneDir;
    public ZoneType zoneType;
    public ZoneLayoutProfile zoneProfile;

    public ZoneData (Vector2Int centerCoord, Vector2Int centerPos, GameObject zoneGO, DirectionEnum previousZoneDir,ZoneType zoneType,ZoneLayoutProfile zoneProfile)
    {
        this.centerCoord = centerCoord;
        this.centerPos = centerPos;
        this.zoneGO = zoneGO;
        this.previousZoneDir = previousZoneDir;
        this.zoneType = zoneType;
        this.zoneProfile = zoneProfile;
    }
    public ZoneData(Vector2Int centerCoord, Vector2Int centerPos)
    {
        this.centerCoord = centerCoord;
        this.centerPos = centerPos;
    }
    
}

public enum ZoneType
{
    openField,
    graveYard,
}