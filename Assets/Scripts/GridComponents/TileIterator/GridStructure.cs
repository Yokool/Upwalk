using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GridStructure")]
public class GridStructure : ScriptableObject
{

    [SerializeField]
    public GridTileBuildData[] StructureList = null;


}



