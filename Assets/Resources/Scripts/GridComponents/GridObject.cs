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

    /// <summary>
    /// Automatically initialize should be only used inside scene objects during testing as GridObjects should be spawned through code.
    /// </summary>
    [SerializeField]
    [Tooltip("Should the Setup method be automatically called in OnEnable of this object and de-initialization in OnDisable.")]
    private bool automaticallyEstablishAndDeestablish;
    
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

    /// <summary>
    /// Sets the grid position of this object and automatically changes the world position of this gameObject to
    /// reflect the change.
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    public void SetGridPosition(int X, int Y)
    {
        this.x = X;
        this.y = Y;
        UpdatePositionToGrid();
    }


    /// <summary>
    /// Updates the position of this gameObject by converting the grid coordinates of this object into world coordinates
    /// and applying the to the world position of this object.
    /// </summary>
    private void UpdatePositionToGrid()
    {
        
        Vector3 position = gameObject.transform.position;

        Vector2 worldCoordinates = GameGrid.INSTANCE.GridToWorldCoordinates(X, Y);
        
        position.x = worldCoordinates.x;
        position.y = worldCoordinates.y;
        gameObject.transform.position = position;
    }
    
    /// <summary>
    /// Establishes the GridObject in the GameGrid system. You should call this method after you've fully initialize the gameObject
    /// that you intend to spawn.
    /// </summary>
    public void Establish()
    {
        GameGrid.INSTANCE.AddEntry(this);
        ValidateAndAssertObjectPosition();
    }

    /// <summary>
    /// Validates the position of the object. If a conflict would arise, the validation goes in the benefit of the calling
    /// object, deleting the objects standing in its way.
    /// </summary>
    public void ValidateAndAssertObjectPosition()
    {
        GameGrid.INSTANCE.CheckForAndHandleIllegalOverlaps(this);
        GameGrid.INSTANCE.NotifyTriggers(this);
    }


    private void OnEnable()
    {
        UpdatePositionToGrid();

        if (!automaticallyEstablishAndDeestablish) return;

        GameGrid instance = GameGrid.INSTANCE;
        // Through the power of a uniunified initialization
        // order. We have to be aware of the scenario
        // where the singleton was not yet awoken.
        GameGridUtilities.INSTANCE.AddGridObjectOnStartup(this);
    }

    private void OnDestroy()
    {
        GameGrid.INSTANCE.RemoveEntry(this);
    }
    private void OnDisable()
    {

        if (!automaticallyEstablishAndDeestablish) return;
        
        // Check for recompiling scenarios. So there isn't an exception.
        // Unless there shouldn't be a scenario other than that
        // one that would indicate in this check running true.
        if(GameGrid.INSTANCE == null)
        {
            return;
        }


        GameGrid.INSTANCE.RemoveEntry(this);
    }


}
