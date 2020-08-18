using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
[DisallowMultipleComponent]
public class PlayerScript : MonoBehaviour
{

    private GridObject gridObject;
    private Moveable moveable;

    private static PlayerScript instance;
    public static PlayerScript INSTANCE => instance;

    private Vector2 lastPosition = Vector2.zero;
    Vector2 delta = Vector2.zero;
    private bool dragging = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        InputMovePlayer();
    }

    private void InputMovePlayer()
    {

        Direction moveDirection = Direction.NONE;

        // Stopped dragging
        if (Input.GetMouseButtonUp(0))
        {
            
            if (dragging)
            {
                Debug.Log(delta);
                if(System.Math.Abs(delta.x) > System.Math.Abs(delta.y))
                {
                    moveDirection = DirectionExtension.GetDirectionFromVector((int)delta.x, 0);
                }
                else
                {
                    moveDirection = DirectionExtension.GetDirectionFromVector(0, (int)delta.y);
                }

                Debug.Log(moveDirection);
                
                // Move the object
                if (!moveDirection.IsEmpty())
                {
                    moveable.MoveObject_RelativeDirectional(moveDirection);
                }
                else
                {
                    TurnSystem.INSTANCE.NextTurn();
                }

                // Reset fields
                lastPosition = Vector2.zero;
                dragging = false;
                delta = Vector2.zero;

                return;
            }

        }
        
        // Not dragging at all
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        // Inprogress Dragging
        Vector2 currScreenPosition = Input.mousePosition;

        if (!dragging)
        {
            dragging = true;
        }
        else
        {

            Vector2 d = currScreenPosition - lastPosition;

            delta += (d);
        }

        lastPosition = currScreenPosition;
        
    }



    private void OnEnable()
    {
        gridObject = GetComponent<GridObject>();
        moveable = GetComponent<Moveable>();
    }



}
