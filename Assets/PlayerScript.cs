using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
public class PlayerScript : MonoBehaviour
{

    private GridObject gridObject;
    private Moveable moveable;

    private void Update()
    {
        InputMovePlayer();
    }

    private void InputMovePlayer()
    {

        Direction moveDirection = Direction.NONE;

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = Direction.NORTH;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = Direction.SOUTH;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = Direction.WEST;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = Direction.EAST;
        }

        if (!moveDirection.IsEmpty())
        {
            moveable.MoveObject(moveDirection);
        }
        
    }



    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        moveable = GetComponent<Moveable>();
    }



}
