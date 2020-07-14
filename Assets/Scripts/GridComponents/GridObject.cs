using System.Collections;
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
    }

    public int Y
    {
        get
        {
            return y;
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

    public void SetPosition(int X, int Y)
    {
        this.x = X;
        this.y = Y;
        UpdatePositionToGrid();
    }



    private void UpdatePositionToGrid()
    {
        // Notify the gamegrid that we've changed our position and check for illegals
        GameGrid.INSTANCE.CheckForAndHandleIllegalOverlaps(this);
        GameGrid.INSTANCE.NotifyTriggers(this);

        Vector3 position = gameObject.transform.position;

        Vector2 worldCoordinates = GameGrid.INSTANCE.GridToWorldCoordinates(X, Y);
        
        position.x = worldCoordinates.x;
        position.y = worldCoordinates.y;
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
