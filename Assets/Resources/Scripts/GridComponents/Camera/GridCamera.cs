using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private int counterCheck;
    private int counter = 0;
    [SerializeField]
    private int distanceToClean;

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
            moveable.MoveObject_RelativeDirectional(Direction.NORTH);
            OnMoveInvoke();
            yield return new WaitForSeconds(secondsBetween);
        }

    }

    private void OnMoveInvoke()
    {
        GameGrid.INSTANCE.SpawnRandomRow();

        counter++;
        if(counter >= counterCheck)
        {
            DeleteFarObjects();
            counter = 0;
        }



    }

    private void DeleteFarObjects()
    {

        GridObject[] filteredObjects = GameGrid.INSTANCE.GetAllGridObjects().Where(

            (grObj) =>
            {
                return GridObjectUtility.Distance(gridObject, grObj) >= distanceToClean && TileTypeDataDatabase.TileTypeDatabase[grObj.m_TileType].shouldBeCleaned;
            }).ToArray();

        for(int i = 0; i <filteredObjects.Length; ++i)
        {
            Destroy(filteredObjects[i].gameObject);
        }

    }


}
