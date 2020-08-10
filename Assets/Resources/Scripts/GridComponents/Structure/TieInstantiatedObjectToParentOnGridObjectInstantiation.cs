using UnityEngine;

public class TieInstantiatedObjectToParentOnGridObjectInstantiation : MonoBehaviour, IOnGridObjectStructureObjectInstantation
{
    public void OnInstantation(GridObject parent, GridObject instantiatedObject)
    {
        instantiatedObject.transform.parent = parent.transform;
    }
}
