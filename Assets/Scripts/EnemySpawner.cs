using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Area Settings")]
    [SerializeField] private float safeZoneRadius = 10f; // Внутренний радиус (safe zone)
    [SerializeField] private float spawnZoneRadius = 20f; // Внешний радиус (зона спавна)
    
    [Header("Gizmo Settings")]
    [SerializeField] private GizmoShape gizmoShape = GizmoShape.Circle;
    [SerializeField] private Color safeZoneColor = Color.green;
    [SerializeField] private Color spawnZoneColor = Color.red;
    [SerializeField] private bool showGizmos = true;
    
    [Header("Enemy Settings")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform coreTransform;
    
    [Header("Spawn Timing")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private bool autoSpawn = true;
    
    [Header("Group Settings (временно не используется)")]
    [SerializeField] private int groupSizeMin = 1;
    [SerializeField] private int groupSizeMax = 1;
    
    private void Start()
    {
        // Найти Core если не назначен
        if (coreTransform == null)
        {
            GameObject core = GameObject.FindGameObjectWithTag("Core");
            if (core != null)
                coreTransform = core.transform;
        }
        
        // Запустить автоспавн
        if (autoSpawn)
            StartCoroutine(SpawnRoutine());
    }
    
    private void OnValidate()
    {
        // Автоматически подправлять радиусы если внешний меньше внутреннего
        if (spawnZoneRadius < safeZoneRadius)
        {
            spawnZoneRadius = safeZoneRadius + 5f;
        }
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }
    
    public void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!");
            return;
        }
        
        // Выбрать рандомного врага
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        
        // Получить позицию спавна в кольце
        Vector3 spawnPosition = GetRandomPositionInRing();
        
        // Создать врага
        GameObject enemy = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        
        // Передать ссылку на Core
        BaseEnemy enemyScript = enemy.GetComponent<BaseEnemy>();
        if (enemyScript != null && coreTransform != null)
        {
            enemyScript.SetCore(coreTransform);
        }
    }
    
    private Vector3 GetRandomPositionInRing()
    {
        // Случайный угол (0-360 градусов)
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        
        // Случайное расстояние МЕЖДУ safeZoneRadius и spawnZoneRadius
        float distance = Random.Range(safeZoneRadius, spawnZoneRadius);
        
        // Вычислить позицию на плоскости XZ
        float x = transform.position.x + Mathf.Cos(angle) * distance;
        float z = transform.position.z + Mathf.Sin(angle) * distance;
        
        // Y координата (чуть выше земли)
        float y = 0.5f;
        
        return new Vector3(x, y, z);
    }
    
    // Для тестирования
    [ContextMenu("Spawn Test Enemy")]
    public void SpawnTestEnemy()
    {
        SpawnEnemy();
    }
    
    // Визуализация в редакторе
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        
        Vector3 center = transform.position;
        
        if (gizmoShape == GizmoShape.Circle)
        {
            // Safe Zone (внутренняя окружность)
            Gizmos.color = safeZoneColor;
            DrawCircle(center, safeZoneRadius);
            
            // Spawn Zone (внешняя окружность)
            Gizmos.color = spawnZoneColor;
            DrawCircle(center, spawnZoneRadius);
        }
        else if (gizmoShape == GizmoShape.Sphere)
        {
            // Safe Zone
            Gizmos.color = safeZoneColor;
            Gizmos.DrawWireSphere(center, safeZoneRadius);
            
            // Spawn Zone
            Gizmos.color = spawnZoneColor;
            Gizmos.DrawWireSphere(center, spawnZoneRadius);
        }
    }
    
    // Рисует окружность на плоскости XZ
    private void DrawCircle(Vector3 center, float radius, int segments = 50)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(radius, 0, 0);
        
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector3(
                Mathf.Cos(angle) * radius,
                0,
                Mathf.Sin(angle) * radius
            );
            
            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}

// Enum для выбора формы Gizmo
public enum GizmoShape
{
    Circle,  // Окружность на плоскости
    Sphere   // Сфера в 3D
}
