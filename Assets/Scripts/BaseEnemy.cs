using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected int maxHealth = 1;
    [SerializeField] protected int damage = 1;
    
    [Header("Detection")]
    [SerializeField] protected float towerDetectionRadius = 10f;
    [SerializeField] protected float targetUpdateInterval = 0.5f;
    
    [Header("Debug")]
    [SerializeField] protected bool showDebugGizmos = true;
    
    protected int currentHealth;
    protected Transform currentTarget;
    protected Transform coreTransform;
    protected float lastTargetUpdateTime;
    protected bool isDead = false;
    
    protected virtual void Start()
    {
        currentHealth = maxHealth;
        
        // Найти Core если не был передан
        if (coreTransform == null)
        {
            GameObject core = GameObject.FindGameObjectWithTag("Core");
            if (core != null)
                coreTransform = core.transform;
            else
                Debug.LogError("Core not found! Make sure Core has 'Core' tag.");
        }
        
        // Начальная цель - Core
        currentTarget = coreTransform;
    }
    
    protected virtual void Update()
    {
        if (isDead) return;
        
        // Обновлять цель периодически
        if (Time.time - lastTargetUpdateTime > targetUpdateInterval)
        {
            UpdateTarget();
            lastTargetUpdateTime = Time.time;
        }
        
        // Двигаться к цели
        if (currentTarget != null)
        {
            MoveToTarget();
        }
    }
    
    protected virtual void UpdateTarget()
    {
        // Искать ближайшую башню в радиусе
        Transform nearestTower = FindNearestTower();
        
        if (nearestTower != null)
        {
            currentTarget = nearestTower;
        }
        else
        {
            currentTarget = coreTransform;
        }
    }
    
    protected Transform FindNearestTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        
        Transform nearest = null;
        float minDistance = towerDetectionRadius;
        
        foreach (GameObject tower in towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = tower.transform;
            }
        }
        
        return nearest;
    }
    
    protected virtual void MoveToTarget()
    {
        if (currentTarget == null) return;
        
        // Направление к цели
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        
        // Движение (кинематика)
        transform.position += direction * speed * Time.deltaTime;
        
        // Поворот к цели
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    
    protected virtual void OnCollisionEnter(Collision collision)
    {
        // Проверка столкновения с башней или Core
        if (collision.gameObject.CompareTag("Tower") || collision.gameObject.CompareTag("Core"))
        {
            Attack(collision.gameObject);
        }
    }
    
    // АБСТРАКТНЫЙ метод - каждый враг реализует свою атаку
    protected abstract void Attack(GameObject target);
    
    public virtual void TakeDamage(int damageAmount)
    {
        if (isDead) return;
        
        currentHealth -= damageAmount;
        
        Debug.Log($"{gameObject.name} took {damageAmount} damage. HP: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        
        Debug.Log($"{gameObject.name} died!");
        
        // Здесь позже:
        // - Эффект разбития на осколки
        // - Спавн света/денег
        // - Звук смерти
        
        Destroy(gameObject);
    }
    
    // Метод для установки Core из спавнера
    public void SetCore(Transform core)
    {
        coreTransform = core;
        currentTarget = core;
    }
    
    // Визуализация в редакторе
    protected virtual void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;
        
        // Радиус обнаружения башен (желтый)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, towerDetectionRadius);
        
        // Линия к текущей цели (красная)
        if (currentTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, currentTarget.position);
        }
    }
}
