using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public GameObject[] obstacles;
    GroundSpawner gs;
    public float SpawnInterval = 20f;

    // Start is called before the first frame update
    void Start()
    {
        gs = GameObject.FindObjectOfType<GroundSpawner>();
        for (float i = 10; i <= 100; i+= SpawnInterval)
        {
            SpawnObstacle(i);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gs.SpawnTile();
            Destroy(gameObject, 2);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle(float zPos)
    {
        int spawnPointIdx;
        Transform spawnPoint;
        //Randomly choose from obstacles prefab pool
        int obstacleIdx = Random.Range(0, obstacles.Length);

        //If obstacle is already constructed for all lanes default spawn to middle
        if (obstacles[obstacleIdx].gameObject.CompareTag("WholeObstacle"))
        {
            spawnPointIdx = 3; //Middle
        }
        else
        {
            //Randomly choose which lane to spawn
            spawnPointIdx = Random.Range(2, 5);
            
        }

        //Set the spawn point
        spawnPoint = transform.GetChild(spawnPointIdx).transform;

        //Spawn
        Instantiate(obstacles[obstacleIdx], new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + zPos), Quaternion.identity, transform);
    }


}
