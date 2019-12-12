using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    private Vector3 spawnPos = new Vector3(25, 0, 0); // the position it will spawn
    private Vector3 spawnPosHumans = new Vector3(25, 0, 0);
    public int obstacleIndex;

    private float startDelay = 2;
    private float repeatRate = 1f;

    private player_controller playerControllerScript;

    public int obstaclesDestroyedCount;

    public float repeatRateMin = 1;
    public float repeatRateMax = 2;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<player_controller>();
        Invoke("SpawnObstacle", (Random.Range(repeatRateMin, repeatRateMax)));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.restart)
        {
            Invoke("SpawnObstacle", (Random.Range(repeatRateMin, repeatRateMax)));
            playerControllerScript.restart = false;
        }
    }

    void SpawnObstacle()
    {
       if (!playerControllerScript.isGameOver)
        {
            int obstacleIndex = Random.Range(0, obstacles.Length);
           
            if(obstacleIndex == 3 || obstacleIndex == 4)
            {
                GameObject obstacle = Instantiate(obstacles[obstacleIndex], spawnPosHumans,
               obstacles[obstacleIndex].transform.rotation);// spawn obstacle
                Move_Forward obshumanScript = obstacle.GetComponent<Move_Forward>();
                obshumanScript.speed = obshumanScript.speed + (float)obstaclesDestroyedCount; // transform  int to f
                float randomDelay = Random.Range(repeatRateMin, repeatRateMax);
                Debug.Log("Random interval" + randomDelay);
                Invoke("SpawnObstacle", randomDelay);
            }
            else
            {
                GameObject obstacle = Instantiate(obstacles[obstacleIndex], spawnPos,
               obstacles[obstacleIndex].transform.rotation);// spawn obstacle
                MoveLeft obsScript = obstacle.GetComponent<MoveLeft>(); //retrieve script from spawned obstacles
                obsScript.speed = obsScript.speed + (float)obstaclesDestroyedCount; // transform  int to float

                float randomDelay = Random.Range(repeatRateMin, repeatRateMax);
                Debug.Log("Random interval" + randomDelay);
                Invoke("SpawnObstacle", randomDelay);
            }
           
            
        } 
    }

    
}  
