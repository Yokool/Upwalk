﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;

    [SerializeField]
    private TileType m_tileType;

    public int X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
            UpdatePositionToGrid();
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
            UpdatePositionToGrid();
        }
    }

    public TileType m_TileType
    {
        get
        {
            return m_tileType;
        }

        set
        {
            m_tileType = value;
        }
    }


    private void UpdatePositionToGrid()
    {
        Vector3 position = gameObject.transform.position;
        position.x = (X * GameGrid.INSTANCE.GridX);
        position.y = (Y * GameGrid.INSTANCE.GridY);
        gameObject.transform.position = position;
    }


    private void OnEnable()
    {
        GameGrid instance = GameGrid.INSTANCE;
        // Through the power of a uniunified initialization
        // order. We have to be aware of the scenario
        // where the singleton was not yet awoken.
        GameGridUtilities.INSTANCE.AddGridObjectOnStartup(this);
    }

    private void OnDisable()
    {
        // Check for recompiling scenarios. So there isn't an exception.
        // Unless there shouldn't be a scenario other than that
        // one that would indicate in this check running true.
        if(GameGrid.INSTANCE == null)
        {
            return;
        }
        GameGrid.INSTANCE.RemoveEntry(this);
    }



    void Start()
    {
        UpdatePositionToGrid();
    }

}