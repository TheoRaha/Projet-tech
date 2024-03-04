using UnityEngine;

public class SpawnOnPositions : MonoBehaviour
{
    [SerializeField] 
    private GameObject cubePrefab;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int minSpawnCount = 2;

    [SerializeField]
    private int maxSpawnCount = 8;

    void Start()
    {
        int spawnCount = Random.Range(minSpawnCount, maxSpawnCount + 1);
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRandomObject();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandomObject();
        }
    }

    private void SpawnRandomObject()
    {
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject instantiated = Instantiate(cubePrefab);
        instantiated.transform.position = randomPoint.position;
    }
}
