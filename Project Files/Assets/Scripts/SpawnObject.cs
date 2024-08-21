using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public Vector3 showVelocity;
    void Spawn(GameObject spawnObject, Transform position)
    {
            GameObject instance = Instantiate(spawnObject, position);
    }

    public GameObject SpawnRandomly(GameObject spawnObject, GameObject parent, Vector3 position, float xMin, float xMax, float yMin, float yMax, float zMin, float zMax, Quaternion rotation)
    {
        Vector3 randomRange = GenerateRandomPosition(xMin /*+ parent.transform.localPosition.x*/, xMax /*+ parent.transform.localPosition.x*/, yMin, yMax, zMin /*+ parent.transform.localPosition.z*/, zMax/* + parent.transform.localPosition.z*/);
        Vector3 spawnPos = position + randomRange;

        GameObject instance = Instantiate(spawnObject, spawnPos, rotation);
        return instance;
    }

    public GameObject Shoot(GameObject spawnObject, Vector3 position, Quaternion rotation, float speed, Vector3 direction, float objectLife)
    {
        direction = Vector3.Normalize(direction);
        direction *=  speed;
        GameObject instance = Instantiate(spawnObject, position, rotation);
        Rigidbody instanceRB = instance.GetComponent<Rigidbody>();
        instanceRB.velocity = direction;
        showVelocity = direction;
        instance.GetComponent<Rigidbody>().velocity = instanceRB.velocity;
        Destroy(instance, objectLife);
        return instance;
    }

    Vector3 GenerateRandomPosition(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        float xPos = UnityEngine.Random.Range(minX, maxX);
        float yPos = UnityEngine.Random.Range(minY, maxY);
        float zPos = UnityEngine.Random.Range(minZ, maxZ);

        Vector3 position = new Vector3(xPos, yPos, zPos);
        return position;
    }
}
