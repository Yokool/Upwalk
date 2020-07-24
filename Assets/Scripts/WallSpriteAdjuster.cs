using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(GridObject))]
public class WallSpriteAdjuster : MonoBehaviour
{

    private GridObject gridObject;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // This is very unreliable for objects who do not
        // have their position already set when being enabled
        // UpdateSprite is mostly managed through the observer callbacks
        UpdateSprite();
    }

    [ContextMenu("UpdateSpriteTest")]
    public void _UpdateSpriteTest()
    {
        UpdateSprite();
    }

    /// <summary>
    /// Updates the wall sprite depending if there exists a wall next to it.
    /// The sprites are formed through a concatenation of a string suffix which
    /// represents if the direction contains a wall.
    /// 
    /// The suffix directly affects the selected sprite. Such sprites are taken
    /// from the grid sprites class through reflection, thus the fields in that class
    /// should not be changed, lest this method gets broken.
    /// 
    /// The sprites follow the naming convention of the Never Eat Sea Weed rule or
    /// NESW.
    /// 
    /// </summary>
    public void UpdateSprite()
    {
        string suffix = "";

        GridObject[] N_arr = GameGrid.INSTANCE.ObjectsAtRelativeDirectional(gridObject, Direction.NORTH);
        N_arr = N_arr.ToList().Where((gridObject) => { return gridObject.m_TileType == TileType.WALL; }).ToArray();
        if (N_arr.Length > 0)
        {
            suffix += "N";
        }

        GridObject[] E_arr = GameGrid.INSTANCE.ObjectsAtRelativeDirectional(gridObject, Direction.EAST);
        E_arr = E_arr.ToList().Where((gridObject) => { return gridObject.m_TileType == TileType.WALL; }).ToArray();
        if (E_arr.Length > 0)
        {
            suffix += "E";
        }

        GridObject[] S_arr = GameGrid.INSTANCE.ObjectsAtRelativeDirectional(gridObject, Direction.SOUTH);
        S_arr = S_arr.ToList().Where((gridObject) => { return gridObject.m_TileType == TileType.WALL; }).ToArray();
        if (S_arr.Length > 0)
        {
            suffix += "S";
        }

        GridObject[] W_arr = GameGrid.INSTANCE.ObjectsAtRelativeDirectional(gridObject, Direction.WEST);
        W_arr = W_arr.ToList().Where((gridObject) => { return gridObject.m_TileType == TileType.WALL; }).ToArray();
        if (W_arr.Length > 0)
        {
            suffix += "W";
        }



        
        // Technically will assign a sprite
        bool assignedASprite = (suffix.Contains('N') || suffix.Contains('E') || suffix.Contains('S') || suffix.Contains('W'));
        

        if (!assignedASprite)
        {
            // This is the default sprite, but assigning it is more clear
            spriteRenderer.sprite = GameSprites.Wall;
            return;
        }
        
        Sprite wallSprite = typeof(GameSprites).GetField($"Wall_{suffix}").GetValue(null) as Sprite;

        spriteRenderer.sprite = wallSprite;


    }



}
