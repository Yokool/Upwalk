using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
[RequireComponent(typeof(Moveable))]
public class GridCamera : MonoBehaviour
{
    private GameObject gameCamera;

    private GridObject gridObject;
    private Moveable moveable;

    [SerializeField]
    private float secondsBetween;

    private Vector3 savedWorldPos;

    private static GridCamera instance;
    public static GridCamera INSTANCE => instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        string cameraName = "GameCamera";
        gameCamera = GameObject.Find(cameraName);
        if(gameCamera == null)
        {
            Debug.LogError($"GridCamera script could not find camera with name {cameraName}.");
        }

        gridObject = GetComponent<GridObject>();
        moveable = GetComponent<Moveable>();

        gameCamera.transform.parent = gameObject.transform;
    }

    public void StartMovingCamera()
    {
        StartCoroutine(MoveCamera());
    }
    
    private IEnumerator MoveCamera()
    {

        while (true)
        {
            moveable.MoveObject(Direction.NORTH);
            OnMoveInvoke();
            yield return new WaitForSeconds(secondsBetween);
        }

    }

    private void OnMoveInvoke()
    {
        GameGrid.INSTANCE.SpawnRandomRow();
    }


}
