using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class GridStructureBehaviour : MonoBehaviour
{
    [SerializeField]
    private GridStructure gridStructure = null;

    [SerializeField]
    [Tooltip("Determines if BuildItself() should be called in the Start method of this mono behaviour.")]
    private bool buildOnCreate = false;

    [SerializeField]
    [Tooltip("Determines if the gameObject that this script is attached to should destroy itself once BuildItself finishes.")]
    private bool destroyOnCompletion = false;



    private void Start()
    {
        if (buildOnCreate)
        {
            BuildItself();
        }
        
    }

    public void BuildItself()
    {
        GridObject structureGridObject = gameObject.GetComponent<GridObject>();
        

        int structureGridX = structureGridObject.X;
        int structureGridY = structureGridObject.Y;

        // Save a list of references so we can enable all objects later once we disable them
        GridObject[] instantiatedPrefabList = new GridObject[gridStructure.StructureList.Length];

        for (int i = 0; i < gridStructure.StructureList.Length; ++i)
        {
            GridTileBuildData structData = gridStructure.StructureList[i];

            GameObject instantiatedPrefab = Instantiate(structData.m_GameObject);

            
            GridObject instantiatedGridObject = instantiatedPrefab.GetComponent<GridObject>();
            
            instantiatedPrefabList[i] = instantiatedGridObject;

            if (instantiatedGridObject == null)
            {
                Debug.LogError($"Object contained in {structData} does not have a GridObject mono behaviour attached to it.");
                continue;
            }
            
            instantiatedGridObject.SetGridPosition(structureGridX + structData.RelativeX, structureGridY + structData.RelativeY);
            
            
        }

        foreach (GridObject instantiatedGridObject in instantiatedPrefabList)
        {
            instantiatedGridObject.Establish();

            IOnGridObjectStructureInstantation[] onGridObjectStructureInstantations = instantiatedGridObject.GetComponents<IOnGridObjectStructureInstantation>();
            foreach (IOnGridObjectStructureInstantation onInstantiationCallback in onGridObjectStructureInstantations)
            {
                onInstantiationCallback.OnInstantation(structureGridObject, instantiatedGridObject);
            }

        }


        if (destroyOnCompletion)
        {
            Destroy(gameObject);
        }

    }

}
