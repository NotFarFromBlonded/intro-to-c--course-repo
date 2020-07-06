using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    //needed for spawning
    [SerializeField]
    GameObject prefabRock;

    //saved for efficiency
    [SerializeField]
    Sprite greenRock;
    [SerializeField]
    Sprite magentaRock;
    [SerializeField]
    Sprite whiteRock;

    //GameObjects List
    GameObject[] ListOfRocks;

    //spawn control 
    const float MinSpawnDelay = 1;
    const float MaxSpawnDelay = 2;
    Timer spawnTimer;

    const int SpawnBorderSize = 100;
    int minSpawnX;
    int maxSpawnX;
    int minSpawnY;
    int maxSpawnY;

  
    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.width - SpawnBorderSize;

        // create and start timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        ListOfRocks = GameObject.FindGameObjectsWithTag("prefabRock");

        if (ListOfRocks.Length<3 && spawnTimer.Finished )
        {
            print(ListOfRocks.Length);
            SpawnRock();

            spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
            spawnTimer.Run();
        }
    }
    void SpawnRock()
    {
        // generate random location and create new rock
        Vector3 location = new Vector3(Random.Range(minSpawnX, maxSpawnX),
                                       Random.Range(minSpawnY, maxSpawnY),
                                       -Camera.main.transform.position.z);
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
        GameObject Rock = Instantiate(prefabRock) as GameObject;
        Rock.transform.position = worldLocation;

        // set random sprite for ne rock
        SpriteRenderer spriteRenderer = Rock.GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        if (spriteNumber == 0)
        {
            spriteRenderer.sprite = greenRock;
        }
        else if (spriteNumber == 1)
        {
            spriteRenderer.sprite = magentaRock;
        }
        else
        {
            spriteRenderer.sprite = whiteRock;
        }
    }
}
