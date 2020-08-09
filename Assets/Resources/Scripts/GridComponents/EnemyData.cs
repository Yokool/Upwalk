using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private int damageAmount;
    public int DamageAmount => damageAmount;
}