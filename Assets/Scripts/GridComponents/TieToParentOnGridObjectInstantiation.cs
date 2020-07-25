using UnityEngine;

public class TieToParentOnGridObjectInstantiation : MonoBehaviour, IOnGridObjectInstantiation
{
    public void OnInstantation(GridObject parent, GridObject instantiatedObject)
    {
        instantiatedObject.transform.parent = parent.transform;
    }
}