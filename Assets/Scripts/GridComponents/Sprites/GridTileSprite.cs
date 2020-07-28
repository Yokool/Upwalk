using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(SpriteRenderer))]
public class GridTileSprite : MonoBehaviour
{

    private GridObject gridObject;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        gridObject = GetComponent<GridObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetLayer();
    }

    private void SetLayer()
    {
        spriteRenderer.sortingOrder = TileTypeDataDatabase.TileTypeDatabase[gridObject.m_TileType].spriteOrder;
    }

}
