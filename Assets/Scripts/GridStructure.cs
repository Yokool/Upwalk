using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GridStructure")]
public class GridStructure : ScriptableObject
{

    [SerializeField]
    private GridTileBuildData[] structureListInit = null;

    [SerializeField]
    private LinkedList<GridTileBuildData> structureList = null;

    public LinkedList<GridTileBuildData> StructureList => structureList;



    /// <summary>
    /// Due to unity awake and on enable shenanigans. We need to init this
    /// SO manually.
    /// </summary>
    public void Init()
    {
        structureList = new LinkedList<GridTileBuildData>(structureListInit);
    }


}



