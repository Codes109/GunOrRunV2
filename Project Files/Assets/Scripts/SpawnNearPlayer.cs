using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnNearPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject objectToSpawn;

    GameObject instance;

    public bool isEnemy;
    public float enemyMoveSpeed = 1f;

    public float xRange;

    public float zMax;

    SpawnObject spawnObject;

    public float spawnFrequency = 5f;
    public float spawnTimer = 0f;

    public float levelTime;
    public int timesCounted;


    private void Start()
    {
        
        spawnObject = GetComponent<SpawnObject>();
    }


    public void Update()
    {
    
         player = GameObject.FindGameObjectWithTag("Player");
           
       

        spawnTimer += Time.deltaTime;
        levelTime += Time.deltaTime;
        if (spawnTimer >= spawnFrequency && isEnemy)
        {
            SpawnEnemy(objectToSpawn);
            if (instance != null)
            {
                Destroy(instance, 5f);
            }
        }
        else if(spawnTimer >= spawnFrequency)
        {
            SpawnItem(objectToSpawn);
            if (instance != null)
            {
                Destroy(instance, 10f);
            }
        }

        if(levelTime >= 10f && isEnemy && spawnFrequency >= 1f)
        {
            timesCounted++;
            spawnFrequency /= 1.5f;
            levelTime = 0f;
        }

    }


    GameObject SpawnEnemy(GameObject objectToSpawn)
    {
        instance = spawnObject.SpawnRandomly(objectToSpawn, player, new Vector3(player.transform.localPosition.x, transform.position.y, player.transform.localPosition.z), -xRange, xRange, 5f, 13f, 20f, zMax, objectToSpawn.transform.rotation);
        instance.transform.LookAt(player.transform.position);
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        Rigidbody playerRB = player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, playerRB.velocity.y, playerRB.velocity.z);
        instance.transform.position = Vector3.MoveTowards(instance.transform.position, player.transform.position, enemyMoveSpeed);

        spawnTimer = 0f;

        return instance;
    }

    GameObject SpawnItem(GameObject objectToSpawn)
    {
        
        instance = spawnObject.SpawnRandomly(objectToSpawn, player, new Vector3(player.transform.localPosition.x, transform.position.y, player.transform.localPosition.z), -xRange, xRange, 5f, 10f, 200f, zMax, objectToSpawn.transform.rotation);

        spawnTimer = 0f;
        
        return instance;
    }
}
