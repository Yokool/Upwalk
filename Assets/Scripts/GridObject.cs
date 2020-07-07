using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;
    
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
        // order. We have to check if the singleton has not
        // been yet initialized.
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



    void Update()
    {
        
    }
}
