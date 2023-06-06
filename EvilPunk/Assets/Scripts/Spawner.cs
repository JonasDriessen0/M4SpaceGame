using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnInterval = 1f;
    public float spawnAngle = 40.828f; // Angle in degrees
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        Quaternion rotation = Quaternion.Euler(spawnAngle, 0f, 0f);
        Vector3 position = new Vector3(transform.position.x, 0f, 0f);
        Instantiate(prefabToSpawn, position, rotation);
    }
}
