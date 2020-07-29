using UnityEngine;

public class TieInstantiatedObjectToParentOnGridObjectInstantiation : MonoBehaviour, IOnGridObjectStructureInstantation
{
    public void OnInstantation(GridObject parent, GridObject instantiatedObject)
    {
        instantiatedObject.transform.parent = parent.transform;
    }
}
