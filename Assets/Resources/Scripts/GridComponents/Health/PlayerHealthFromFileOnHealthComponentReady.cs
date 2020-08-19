using UnityEngine;

public class PlayerHealthFromFileOnHealthComponentReady : MonoBehaviour, IOnHealthComponentReady
{
    public void OnHealthComponentReady()
    {
        GetComponent<HealthComponent>().SetHealth(PersistentFiles.PlayerHealthData.HealthAmount);
    }
}