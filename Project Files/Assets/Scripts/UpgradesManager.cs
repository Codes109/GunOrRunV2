using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradesManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerPrefab2;

    public GameObject missile;
    public GameObject fastShip;


    bool doOnce = false;
    public bool hasSpawnedPlayer = false;
    public bool isMissile;
    public bool isRapidFire = false;
    public bool isFastShip = false;

    private void Update()
    {
        

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 1") && !doOnce)
        {
            GameObject plane = GameObject.FindGameObjectWithTag("Plane");
            if (isFastShip)
            {
                playerPrefab = playerPrefab2;
            }
            Instantiate(playerPrefab, plane.transform);
            GameObject player = playerPrefab.transform.GetChild(0).gameObject;
            hasSpawnedPlayer = true;
            if (isMissile)
            {
                player.GetComponent<PlayerScript>().weapon = missile;
            }
            else if (isRapidFire)
            {
                player.GetComponent<PlayerScript>().weaponSpawnDelay = 0.3f;
            }
            else if (isFastShip)
            {
                player.GetComponent<PlayerScript>().moveSpeed = 10f;
                player.GetComponent<PlayerScript>().damageReceived = 10;
            }
            doOnce = true;
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main Menu"))
        {
            doOnce = false;
        }
        
    }
}
