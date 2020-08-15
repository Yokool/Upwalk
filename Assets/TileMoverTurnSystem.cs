using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMoverTurnSystem : MonoBehaviour
{


    private static TileMoverTurnSystem instance;
    public static TileMoverTurnSystem INSTANCE => instance;

    private void Awake()
    {
        instance = this;
        movementList = new Dictionary<Turn, List<GridObject>>();

        movementList[Turn.EASY] = new List<GridObject>();
        movementList[Turn.MEDIUM] = new List<GridObject>();
        movementList[Turn.HARD] = new List<GridObject>();
        movementList[Turn.FULL] = new List<GridObject>();

    }

    private Dictionary<Turn, List<GridObject>> movementList = null;

    public void MoveAllTurnTiles(Turn turnType)
    {

        List<GridObject> gridObjects = movementList[turnType];
        List<ObjectNLocation> locationList = new List<ObjectNLocation>();

        // Ask for where all the turn objects will move to next turn
        for(int i = 0; i < gridObjects.Count; ++i)
        {

            GridObject gridObject = gridObjects[i];
            GridObjectTurnMovement movement = gridObject.GetComponent<GridObjectTurnMovement>();

            locationList.Add(new ObjectNLocation(gridObject, movement.GetNextTile()));

        }

        Moveable playerMoveable = PlayerScript.INSTANCE.gameObject.GetComponent<Moveable>();

        // Start the checking system
        // Invalidated objects are marked null
        for (int i = 0; i < locationList.Count; ++i)
        {
            ObjectNLocation location = locationList[i];

            // Invalidated objects are skipped
            if(location == null)
            {
                continue;
            }
            /*
            // Player is treated specially during his lerp
            // This object will not move if it would collide with
            // the player, simple Moveable CanMoveTo method
            // doesn't work on two objects during lerping
            if (playerMoveable.CheckAsyncCollision(location.m_GridObject))
            {
                locationList[i] = null;
                continue;
            }
            */
            for (int j = 0; j < locationList.Count && j != i; ++j)
            {
                ObjectNLocation locationTwo = locationList[j];

                // Invalidated objects
                if(locationTwo == null)
                {
                    continue;
                }

                // If two objects try to move to the same location
                // pick the one that registered first
                if(location.Position.x == locationTwo.Position.x && location.Position.y == locationTwo.Position.y)
                {
                    locationList[j] = null;
                    locationList[j].m_GridObject.GetComponent<Moveable>().FailedToMoveToCallbackInvoke(locationTwo.Position.x, locationTwo.Position.y, locationTwo.m_GridObject);
                }

            }

        }

        // Get rid of all invalidated objects
        locationList.RemoveAll((loc) => { return loc == null; });

        // Move only the validated ones
        for(int i = 0; i < locationList.Count; ++i)
        {
            ObjectNLocation objectNLocation = locationList[i];
            objectNLocation.m_GridObject.GetComponent<Moveable>().MoveObject_Absolute(objectNLocation.Position.x, objectNLocation.Position.y);
        }

    }

    public void RegisterObject(GridObject gridObject)
    {

        OnNextTurnCallback_Base callbackBase = gridObject.GetComponent<OnNextTurnCallback_Base>();
        GridObjectTurnMovement gridObjectTurnMovement = gridObject.GetComponent<GridObjectTurnMovement>();
        

        if(callbackBase == null)
        {
            Debug.LogError($"Class: {nameof(TileMoverTurnSystem)} Method: {nameof(RegisterObject)}: Tried to add a {nameof(GridObjectTurnMovement)} of object: {nameof(gridObject)} which did not have a {nameof(OnNextTurnCallback_Base)} component attached to it to the list.");
            return;
        }

        if(gridObjectTurnMovement = null)
        {
            Debug.LogError($"Class: {nameof(TileMoverTurnSystem)} Method: {nameof(RegisterObject)}: Tried to add a {nameof(GridObjectTurnMovement)} of object: {gridObject} to the list.");
            return;
        }

        movementList[callbackBase.AssociatedTurnType].Add(gridObject);

    }

    public void RemoveObject(GridObject gridObject)
    {
        OnNextTurnCallback_Base callbackBase = gridObject.GetComponent<OnNextTurnCallback_Base>();
        
        if (callbackBase == null)
        {
            Debug.LogError($"Class: {nameof(TileMoverTurnSystem)} Method: {nameof(RemoveObject)}: Tried to add a {nameof(GridObjectTurnMovement)} of object: {nameof(gridObject)} which did not have a {nameof(OnNextTurnCallback_Base)} component attached to it to the list.");
            return;
        }

        movementList[callbackBase.AssociatedTurnType].Remove(gridObject);

    }


}
