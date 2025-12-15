using UnityEngine;

public class RushEnemy : BaseEnemy
{
    [Header("Rush Settings")]
    [SerializeField] private bool dieOnImpact = true; // Умирать ли при таране
    
    // Переопределяем атаку - уникальное поведение Rush врага
    protected override void Attack(GameObject target)
    {
        // Получить компонент который может получать урон
        IDamageable damageable = target.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            // Нанести урон
            damageable.TakeDamage(damage);
            Debug.Log($"Rush enemy dealt {damage} damage to {target.name}");
        }
        
        // Rush враг умирает после тарана
        if (dieOnImpact)
        {
            Die();
        }
    }
}
