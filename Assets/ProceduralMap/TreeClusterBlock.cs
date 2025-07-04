using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Cell;
public class TreeClusterBlock : PropsBlock
{
    protected override void PopulateBlock(CellGrid cellGrid, ZoneLayoutProfile zoneLayoutProfile)
    {
       
        if (zoneLayoutProfile is GraveYardLayoutProfile graveYardLayoutProfile)
        {
            TilePaint[] tilePaints = { new TilePaint { tilemap = ZoneManager.Instance.PropsTilemap, ruleTile = graveYardLayoutProfile.treeRuletile }, new TilePaint { tilemap = ZoneManager.Instance.GroundTilemap, ruleTile = graveYardLayoutProfile.leafRuleTile } };

            for (int y = 0; y < cellGrid.CellPerCol; y++)
            {
                for (int x = 0; x < cellGrid.CellPerRow; x++)
                {
                    Vector3Int pos = new Vector3Int((int)celLGrid.Cells[x, y].CellPos.x, (int)celLGrid.Cells[x, y].CellPos.y, 0);
                    //ZoneManager.Instance.PropsTilemap.SetTile(pos, zoneLayoutProfile.treeRuletile);
                    //ZoneManager.Instance.GroundTilemap.SetTile(pos, graveYardLayoutProfile.leafRuleTile);
                    cellGrid.Cells[x, y].AddToTilePaints(tilePaints);
                    //Debug.Log(cellGrid.Cells[x, y].TilePaintsList[0].tilemap + " / " + cellGrid.Cells[x, y].TilePaintsList[0].ruleTile);

                    //GameObject propPrefab = graveYardLayoutProfile.GetRandomProps(graveYardLayoutProfile.treePrefabs);

                    //Bounds bounds = ProceduralUtils.GetCombinedBounds(propPrefab);
                    //Vector3 propsScale = new Vector3(bounds.size.x, bounds.size.y , 1);

                    //GameObject prop = Instantiate(propPrefab, cellGrid.Cells[x, y].CellPos, Quaternion.identity);
                    //prop.transform.parent = propsHolder.transform;

                    //for (int i = cellGrid.Cells[x, y].CellID.x; i < cellGrid.Cells[x, y].CellID.x + propsScale.x; i++)
                    //{
                    //    for (int j = cellGrid.Cells[x, y].CellID.y; j < cellGrid.Cells[x, y].CellID.y + propsScale.y; j++)
                    //    {
                    //        if (i >= 0 && i < cellGrid.CellPerRow && j >= 0 && j < cellGrid.CellPerCol)
                    //        {
                    //            cellGrid.Cells[i, j].IsOccupied = true;

                    //            GroundTilemap.SetTile(new Vector3Int((int)cellGrid.Cells[x, y].CellPos.x - (int)GroundTilemap.transform.position.x, (int)cellGrid.Cells[x, y].CellPos.y - (int)GroundTilemap.transform.position.y, 0), graveYardLayoutProfile.leafRuleTile);
                    //        }
                    //    }
                    //}

                }
            }

        }
    }

  
}
