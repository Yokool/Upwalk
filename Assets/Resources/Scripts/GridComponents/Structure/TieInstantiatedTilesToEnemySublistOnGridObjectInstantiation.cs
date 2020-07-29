using UnityEngine;

public class TieInstantiatedTilesToEnemySublistOnGridObjectInstantiation : MonoBehaviour, IOnGridObjectStructureInstantation
{
    public void OnInstantation(GridObject parent, GridObject instantiatedObject)
    {
        EnemyBehaviour enemyBehaviour = parent.GetComponent<EnemyBehaviour>();

        if(enemyBehaviour == null)
        {
            Debug.LogError($"TieToEnemySublist OnInstantiation called on a child of {parent} which does not contain a component {nameof(EnemyBehaviour)}.\n" +
                $"Returning early.");
            return;
        }

        enemyBehaviour.AddEnemyTile(instantiatedObject);

    }
}