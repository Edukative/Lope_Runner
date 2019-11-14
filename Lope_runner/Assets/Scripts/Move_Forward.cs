using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Move_Forward : MonoBehaviour
{
    public float speed;
    private player_controller playerControllerScript;
    public float leftBound = -15;

    SpawnManager spawnM;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<player_controller>();
        spawnM = GameObject.Find("Spawn_manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
         
        if (!playerControllerScript.isGameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            spawnM.obstaclesDestroyedCount++; //the same as obstacleDestroyedCount +1
            Destroy(gameObject);
        }
    }
}
