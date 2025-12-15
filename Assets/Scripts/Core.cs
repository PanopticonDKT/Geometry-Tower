using UnityEngine;

public class Core : MonoBehaviour, IDamageable
{
    [Header("Core Settings")]
    [SerializeField] private int maxHealth = 100;
    
    private int currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
        
        // Убедиться что тег установлен
        if (!gameObject.CompareTag("Core"))
        {
            Debug.LogWarning("Core object doesn't have 'Core' tag!");
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        Debug.Log($"Core took {damage} damage! HP: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Debug.Log("CORE DESTROYED! GAME OVER!");
            // Здесь потом Game Over логика
        }
    }
}
