using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum PropsBlockTypeEnum
{
    GraveCluster,
    TreeCluster
}
public class PropsBlock : MonoBehaviour
{
    protected BoxCollider2D boxCollider2D;

    protected GameObject propsHolder;
    protected ZoneLayoutProfile zoneLayoutProfile;
    protected ZoneHandler zoneHandler;

    protected int cellSize = 1;
    protected int blockWidth = 1;
    protected int blockHeight = 1;

    protected SubCellGrid subCellGrid;
    protected CellGrid parentCellGrid;

    protected bool isPlayerOnThisBlock = false;


    public ZoneHandler ZoneHandler { get => zoneHandler; set => zoneHandler = value; }
    public bool IsPlayerOnThisBlock { get => isPlayerOnThisBlock; set => isPlayerOnThisBlock = value; }

    protected virtual void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        propsHolder = transform.GetChild(1).transform.gameObject;

    }
    public void Init(ZoneHandler zoneHandler, CellGrid parentCellGrid, Vector3Int firstCellPos, BoundsInt zoneBounds, ZoneLayoutProfile zoneLayoutProfile)
    {
        this.zoneHandler = zoneHandler;

        //transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 0f);
        transform.GetChild(0).localScale = new Vector3(zoneBounds.size.x, zoneBounds.size.y, zoneBounds.size.z);

        blockWidth = zoneBounds.size.x;
        blockHeight = zoneBounds.size.y;

        boxCollider2D.size = new Vector2(blockWidth - 1, blockHeight - 1);
        boxCollider2D.offset = new Vector2(blockWidth * 0.5f, blockHeight * 0.5f);


        this.parentCellGrid = parentCellGrid;
        this.zoneLayoutProfile = zoneLayoutProfile;

        subCellGrid = new SubCellGrid(parentCellGrid, blockWidth, blockHeight, firstCellPos);


        PopulateBlock(subCellGrid);

    }
    //protected void VisualizeOccupiedCells(SubCellGrid subCellGrid, ZoneLayoutProfile zoneLayoutProfile)
    //{
    //    for (int i = 0; i < subCellGrid.CellPerCol; i++)
    //    {
    //        for (int j = 0; j < subCellGrid.CellPerRow; j++)
    //        {
    //            GameObject go = Instantiate(zoneLayoutProfile.spawnablePropsBlock, (Vector3Int)subCellGrid.Cells[j, i].GlobalCellPos, Quaternion.identity);

    //            if (!subCellGrid.Cells[j, i].IsOccupied)
    //            {
    //                go.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
    //            }
    //            else if (subCellGrid.Cells[j, i].IsOccupied)
    //            {
    //                go.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.2f);
    //            }
    //        }
    //    }
    //}

    protected virtual void PopulateBlock(SubCellGrid subCellGrid)
    {


    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {


    }



    protected virtual void OnTriggerExit2D(Collider2D collision)
    {

    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {

    }

}
