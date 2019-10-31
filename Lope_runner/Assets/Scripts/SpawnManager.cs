using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    private Vector3 spawnPos = new Vector3(25, 0, 0); // the position it will spawn
    public int obstacleIndex;

    private float startDelay = 2;
    private float repeatRate = 1f;

    private player_controller playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<player_controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
       if (!playerControllerScript.isGameOver)
        {
            int obstacleIndex = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[obstacleIndex], spawnPos,
                obstacles[obstacleIndex].transform.rotation);// spawn obstacle
        }
    }

    
}  
