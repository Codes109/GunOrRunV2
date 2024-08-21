using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    SpawnObject spawnObject;
    public GameObject weapon;
    Collider parentObjectCollider;

    public GameObject target;

    public enum FireMode { Single, Spiral, Fan,};
    public FireMode fireMode;

    public float firingDelay = 1f;
    float firingTimer = 0f;
    public float shootSpeed = 1f;
    float firingAngle;

    public bool isOn;

    private void Start()
    {
        
        spawnObject = GetComponent<SpawnObject>();
        parentObjectCollider = gameObject.GetComponentInParent<Collider>();
    }

    private void FixedUpdate()
    {
        target = GameObject.Find("Players_Ship");
        if (isOn)
        {
            firingTimer += Time.deltaTime;
            if (fireMode == FireMode.Single)
            {
                if (firingTimer >= firingDelay)
                {
                    gameObject.transform.LookAt(target.transform.position);

                    GameObject missileInstance;
                    missileInstance = spawnObject.Shoot(weapon, transform.position, gameObject.GetComponentInParent<Transform>().rotation, shootSpeed, Vector3.up, 3f);
                    /*missileInstance.transform.position = Vector3.MoveTowards(missileInstance.transform.position, target.transform.position, 10f);*/
                    Physics.IgnoreCollision(missileInstance.GetComponent<Collider>(), parentObjectCollider);
                    firingTimer = 0f;
                }
            }
        }
    }
}
