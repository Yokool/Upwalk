using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStructureBehaviour : MonoBehaviour
{
    [SerializeField]
    private GridStructure gridStructure = null;

    [SerializeField]
    private bool destroyOnBuild = false;

    [SerializeField]
    private bool buildOnCreate = false;

    private void Awake()
    {
        gridStructure.Init();
    }


    private void Start()
    {
        if (buildOnCreate)
        {
            BuildItself();
        }
        
    }

    public void BuildItself()
    {

        GridObject structureGridObject = GetComponent<GridObject>();


        int structureGridX = structureGridObject.X;
        int structureGridY = structureGridObject.Y;


        foreach (GridTileBuildData structData in gridStructure.StructureList)
        {

            GameObject objectToBuild = Instantiate(structData.m_GameObject);

            GridObject objectToBuildGridObj = objectToBuild.GetComponent<GridObject>();

            if (!objectToBuildGridObj)
            {
                Debug.LogError($"Object contained in {structData} does not have a GridObject mono behaviour attached to it.");
                return;
            }

            objectToBuildGridObj.X = structureGridX + structData.RelativeX;
            objectToBuildGridObj.Y = structureGridY + structData.RelativeY;


        }

        if (destroyOnBuild)
        {
            Destroy(gameObject);
        }


    }
}