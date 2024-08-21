using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public float levelSpeed;
    public float moveSpeed;
    public Rigidbody rb;
    
    public Vector3 direction;
    public int LifePercent = 100;
    public int damageReceived = 4;
    bool isLowHealth = false;
    public int score = 0;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    MeshRenderer meshRenderer;
    Color origColor;
    float flashTime = 0.15f;
    float lowHealthFlashTime = 1f;

    public GameObject weapon;
    public GameObject explosion;
    public float weaponSpawnDelay = 0.8f;
    public float weaponSpawnTimer = 5f;
    public float weaponSpeed = 10f;
    public Vector3 weaponDirection;


    private Vector3 moveDirection;

    private SpawnObject spawnObject;
    private PlayRandomMusicTrack playRandomMusicTrack;

    public GameObject scoresContainer;
    public GameObject nameInput;
    Dictionary<string, int> highScoresDict = new Dictionary<string, int>();

    bool hasRunEndGame = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;
        spawnObject = gameObject.GetComponent<SpawnObject>();
        playRandomMusicTrack = gameObject.GetComponent<PlayRandomMusicTrack>();
        scoresContainer = GameObject.FindGameObjectWithTag("ScoresContainer");
        nameInput = GameObject.FindGameObjectWithTag("NameInput");
        healthText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        gameOverText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //processing inputs
        ProcessInputs();
        weaponSpawnTimer += Time.deltaTime;
        Shoot(weapon);
        EndGame();
    }

    private void FixedUpdate()
    {
        //physics calculations
        Move();
        scoreText.text = "Score: " + score.ToString();
        healthText.text = "Ship health: " + LifePercent.ToString() + "%";

        if (LifePercent <= 30f)
        {
            flashTime = lowHealthFlashTime;
            StartFlash();
            if (isLowHealth == false)
            {
                PlayLowHealthMusic();
                isLowHealth = true;
            }
        }


    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = levelSpeed; //Input.GetAxisRaw("Vertical");
        float moveY = 0;

        if(Input.GetKey(KeyCode.S) && transform.localPosition.y >= 0f)
        {
            moveY = -1f;
        }
        else if(Input.GetKey(KeyCode.W) && transform.localPosition.y <= 10f) 
        {
            moveY = 1f;
        }
        moveDirection = new Vector3 (moveX * 5, moveY * 5, moveZ);
        direction = moveDirection;
        

        //rotate player
        if (direction.x > 0 )
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (direction.x == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            highScoresDict = scoresContainer.GetComponent<HighScoresDictionary>().scores;
            string playerName = highScoresDict.Last().Key;
            highScoresDict[playerName] = score;
            
            ReturnToMainMenu();
        }

        
    }

    void Move()
    {
        
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed, moveDirection.z * levelSpeed);
         

    }

    private void OnCollisionEnter(Collision collision)
    {
        //***Flash White***
            LifePercent -= damageReceived;
            
            StartFlash();
            playRandomMusicTrack.PlayTrack("Shield Damage");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            score += 1;
            
        }
    }

    void StartFlash()
    {
        meshRenderer.material.color = Color.white;
        Invoke("StopFlash", flashTime);
    }

    void StopFlash()
    {
        meshRenderer.material.color = origColor;
    }

    void Shoot(GameObject weapon)
    {
        if (Input.GetKeyDown(KeyCode.Space) && weaponSpawnTimer >= weaponSpawnDelay)
        {
            GameObject missileInstance;
            missileInstance = spawnObject.Shoot(weapon, transform.position, transform.rotation, weaponSpeed, Vector3.forward, 3f);
            weaponSpawnTimer = 0f;
            Physics.IgnoreCollision(missileInstance.GetComponent<Collider>(), GetComponent<Collider>());
            playRandomMusicTrack.PlayTrack("Shoot");
        }
    }

    void PlayLowHealthMusic()
    {
        GameObject.FindWithTag("Music Player").GetComponent<PlayRandomMusicTrack>().PlayTrack("Low Health");
    }

    void EndGame()
    {
        if(LifePercent <= 0 && !hasRunEndGame)
        {
            score = 0;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            meshRenderer.enabled = false;
            Instantiate(explosion, gameObject.transform);
            GameObject.FindWithTag("Music Player").GetComponent<PlayRandomMusicTrack>().PlayTrack("Game Over");
            gameOverText.enabled = true;
            hasRunEndGame = true;
            Invoke("ReturnToMainMenu", 5f);
        } 
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
