using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public Text playerHealthText;
    public Image fillImage;
    public Slider healthSlider;

    public Text specialText;
    public Slider specialSlider;

    [SerializeField]
    private SpriteRenderer bloodTint;
    

    [SerializeField]
    private Text waveCountText;

    public static System.Random random = new System.Random();

    public int waveCount;
    public PlayerController player;

    private float timeBetweenSpawn;

    private float damageMultiplier = 1.25f;

    //when set to true spawns new wave of enemies, when set to false wave is in progress
    private bool isSpawning;
    public List<EnemyAI> enemyList;
    public EnemyAI enemyPrefab;
    public ShieldEnemy sheildEnemyPrefab;
    public List<EnemyAI> basicEnemySpawnPool = new List<EnemyAI>();
    public List<ShieldEnemy> shieldEnemySpawnPool = new List<ShieldEnemy>();
    private Vector2 enemyPoolPosition = new Vector2(40.0f, 0.0f);

    private List<HealthDrop> healthDropPool;
    private List<HealthDrop> activeHealthDrops;
    public GameObject healthDropPrefab;
    private const float healthDropPickupRadius = 0.75f;
    private const float healthDropDuration = 5.0f;
    private Vector2 healthPoolPosition = new Vector2(40.0f, 5.0f);

    private float healthDropRate = 20.0f; // percent

    //variable for special move
    public SpecialMove special;

    /* failed idea may be useful later so I'm not deleting
    public GameObject topSpawn;
    public GameObject bottomSpawn;
    public GameObject leftSpawn;
    public GameObject rightSpawn;
    */
    private float cameraHeight;
    private float cameraWidth;
    public Camera cameraObject;

    //health
    float healthFill;

    //variable for tracking/controlling the special attack bar
    private int specialAmountFull;

    //sounds
    public AudioSource beginningWavesSound;
    public AudioSource endingWavesSound;
    public AudioSource healthPickupSound;
    //used by pause controller to hold which track needs to be paused/restarted
    public AudioSource currentMusic;

    [SerializeField]
    private PauseController pauseController;
    [SerializeField]
    private TutorialTextController tutorialText;
    private bool firstShieldSpawn;

    //and getter setter for special attack bar
    public int SpecialAmountFull
    {
        get { return specialAmountFull; }
        set { specialAmountFull = value; }
    }

    public PlayerController Player
    {
        get { return player; }
    }
    public List<EnemyAI> EnemyList
    {
        get { return enemyList; }
    }

    private ScoreTracker scoreTracker;

    private int enemiesKilledThisWave;

    private float trapSwitchInterval;
    private float trapSwitchTimer;

    // Start is called before the first frame update
    void Start()
    {
        trapSwitchInterval = 6.0f;
        trapSwitchTimer = trapSwitchInterval;

        beginningWavesSound.enabled = true;
        if (beginningWavesSound != null)
        {
            currentMusic = beginningWavesSound;
            beginningWavesSound.Play();
            //Debug.Log("beginningWavesSound Played");
        }

        healthSlider.GetComponent<Slider>();
        specialSlider.GetComponent<Slider>();

        scoreTracker = gameObject.GetComponent<ScoreTracker>();

        timeBetweenSpawn = 0.2f;
        //waveCount = 1;
        UpdateWaveCountText();

        isSpawning = true;
        enemyList = new List<EnemyAI>();

        healthDropPool = new List<HealthDrop>();
        activeHealthDrops = new List<HealthDrop>();

        for (int i = 0; i < 20; i++)
        {
            HealthDrop drop = Instantiate(healthDropPrefab).GetComponent<HealthDrop>();
            drop.transform.position = healthPoolPosition;
            healthDropPool.Add(drop);
        }

        cameraHeight = cameraObject.orthographicSize * 2f;
        cameraWidth = cameraHeight * cameraObject.aspect;

        //populating spawn queue this sets the maximum number of enemies that can be on the screen at one time
        
        for (int i = 0; i < 20; i++)
        {
            EnemyAI newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = enemyPoolPosition;
            newEnemy.Player = player;
            newEnemy.PlayerTransform = player.transform;
            newEnemy.gameObject.SetActive(false);
            newEnemy.gameManager = this;
            newEnemy.PauseController = pauseController;
            basicEnemySpawnPool.Add(newEnemy);

            ShieldEnemy newShield = Instantiate(sheildEnemyPrefab);
            newShield.transform.position = enemyPoolPosition;
            newShield.Player = player;
            newShield.PlayerTransform = player.transform;
            newShield.gameObject.SetActive(false);
            newShield.gameManager = this;
            newShield.PauseController = pauseController;
            shieldEnemySpawnPool.Add(newShield);

        }

        //sets the initial value for player health
        UpdatePlayerUI();
        player.DamageAble = true;

        firstShieldSpawn = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.Health <= 0)
        {
            //sets health to 0
            player.Health = 0;
            UpdatePlayerUI();

            float alpha = bloodTint.color.a;
            alpha += Time.deltaTime;
            bloodTint.color = new Color(245, 0, 0, alpha);
            //prevents the player from moving during the fade to red
            player.PlayerState = State.isIdle;
            player.moveSpeed = 0;
            if (alpha >= 1)
            {
                //takes the player to a game over screen when the fade is complete
                SceneManager.LoadScene("LossScene");

            }

        }
        else
        {
            if (!pauseController.IsPaused)
            {


                if (isSpawning)
                {
                    for(int i = 0; i < waveCount - enemiesKilledThisWave; i++)
                    {
                        Spawning();
                    }

                    isSpawning = false;
                }
                else
                {

                    if (enemyList.Count == 0)
                    {
                        isSpawning = true;

                        if(enemiesKilledThisWave >= waveCount)
                        {
                            waveCount++;
                            UpdateWaveCountText();
                            enemiesKilledThisWave = 0;
                        }
                        
                    }

                    //when there are more than three enemies one will be toggled to start or stop a different movement behavior where they try to box the player in 
                    if (enemyList.Count > 3)

                    {
                        trapSwitchTimer -= Time.deltaTime;

                        if (trapSwitchTimer <= 0)
                        {
                            trapSwitchTimer = trapSwitchInterval;

                            int rng = Random.Range(0, enemyList.Count);

                            enemyList[rng].toggleTrapping();
                            //Debug.Log("toggling");
                        }
                    }

                    foreach (EnemyAI enemy in enemyList)
                    {
                    
                        
                        

                        
                        

                        if (enemy.Health <= 0)
                        {
                            //keeps track of al the enemies killed
                            //scoreTracker.enemies

                            //if the player is not currently using their special
                            if (!special.IsActive && specialAmountFull < 10)
                            {
                                //increases special bar for each enemy killed
                                specialAmountFull++;
                                //Debug.Log("enemy killed");
                            }


                            if (random.NextDouble() * 100 < healthDropRate)
                            {
                                HealthDrop drop;
                                if (healthDropPool.Count > 0)
                                {
                                    //Debug.Log("Drop pulled from pool");
                                    drop = healthDropPool[0];
                                    activeHealthDrops.Add(drop);
                                    healthDropPool.RemoveAt(0);
                                }
                                else
                                {
                                    drop = Instantiate(healthDropPrefab).GetComponent<HealthDrop>();
                                    activeHealthDrops.Add(drop);
                                }
                                drop.transform.position = enemy.Position;
                                drop.StartTimer();
                            }

                            enemy.transform.position = enemyPoolPosition;

                            enemy.PunchObj.IsActive = false;
                            enemy.PunchObj.transform.position = enemy.transform.position;

                            enemyList.Remove(enemy);

                            enemy.gameObject.SetActive(false);

                            enemy.Health = enemyPrefab.Health;
                            //returns the enemy to the spawning pool for reuse
                            if (enemy is ShieldEnemy)
                            {
                                ShieldEnemy temp = (ShieldEnemy)enemy;
                                temp.AddShield();
                                temp.Health = sheildEnemyPrefab.Health;
                                shieldEnemySpawnPool.Add(temp);

                            }
                            else
                            {
                                
                                basicEnemySpawnPool.Add(enemy);
                            }

                            
                            ScoreTracker.enemiesKilled++;
                            enemiesKilledThisWave++;
                        }
                    }

                    foreach (HealthDrop healthDrop in activeHealthDrops)
                    {
                        // Check if any of the health drops are close enough to the player
                        if ((healthDrop.transform.position - player.Position).sqrMagnitude <= Mathf.Pow(healthDropPickupRadius, 2))
                        {
                            // If they are, heal the player and send them back to the pool
                            CollectHealthDrop(healthDrop, true);
                        }

                        // If it's reached it's despawn time threshold
                        if (healthDrop.Timer >= healthDropDuration)
                        {
                            CollectHealthDrop(healthDrop, false);
                        }
                    }

                }
            }


            //outside of else statement so player health is updated when it reaches 0
            UpdatePlayerUI();

            
        }
    }


    public void UpdatePlayerUI()
    {
        //Player health
        healthFill = player.Health / 100f;
        healthSlider.value = healthFill;
        playerHealthText.text = "Player Health: " + player.Health;

        // Special
        specialSlider.value = specialAmountFull / 10.0f;
        specialText.text = "Special: " + specialAmountFull;
    }

    public void UpdateWaveCountText()
    {
        waveCountText.text = "Wave Count: " + waveCount;
    }

    public void CollectHealthDrop(HealthDrop drop, bool pickedUp)
    {
        if (pickedUp)
        {
            Player.Heal(20);
            healthPickupSound.Play();
        }
        activeHealthDrops.Remove(drop);
        healthDropPool.Add(drop);
        drop.transform.position = new Vector3(100.0f, 0.0f, 0.0f);
        drop.ResetTimer();
    }

    
    private void Spawning()
    {
        int rng = Random.Range(0, 6);
        //random chance to spawn either a basic enemy or sheild enemy
        if(rng <= 4)
        {
            SpawnBasic();
        }
        else
        {
            SpawnShield();
        }
        
    }

    private void SpawnBasic()
    {
        if (basicEnemySpawnPool.Count > 0)
        {
                // creates new enemy from the spawning pool
                EnemyAI newEnemy = basicEnemySpawnPool[0];

                //adds to the list of active enemies and removes from the spawning pool
                enemyList.Add(newEnemy);
                basicEnemySpawnPool.Remove(newEnemy);

                //chooses a random spawn point for the new enemy
                int doorSelect = Random.Range(0, 4);

            //positions enemy at chosen spawnpoint
                if (doorSelect == 0)
                {
                    
                    newEnemy.Position = new Vector3(0, cameraHeight / 2 + 5, 0);
                }
                else if (doorSelect == 1)
                {
                    newEnemy.Position = new Vector3(0, cameraHeight / -2 - 5, 0);
                }
                else if (doorSelect == 2)
                {
                    newEnemy.Position = new Vector3(cameraWidth / -2 - 5, 0, 0);
                }
                else
                {
                    newEnemy.Position = new Vector3(cameraWidth / 2 + 5, 0, 0);
                }

                //makes the enemy visible
                newEnemy.gameObject.SetActive(true);

        }

        

       
    }

    private void SpawnShield()
    {

        if (shieldEnemySpawnPool.Count > 0)
        {



            if (!firstShieldSpawn)
            {
                firstShieldSpawn = true;
                tutorialText.ShowShieldTutorial();

                if (endingWavesSound.isPlaying == false)
                {
                    beginningWavesSound.Stop();
                    endingWavesSound.enabled = true;
                    currentMusic = endingWavesSound;
                    endingWavesSound.Play();
                }
            }

            ShieldEnemy newEnemy = shieldEnemySpawnPool[0];


            enemyList.Add(newEnemy);
            shieldEnemySpawnPool.Remove(newEnemy);

            //chooses a random spawn point for the new enemy
            int doorSelect = Random.Range(0, 4);

            if (doorSelect == 0)
            {
                //constant value makes it so enemy doesnt pop in on screen ll
                newEnemy.Position = new Vector3(0, cameraHeight / 2 + 5, 0);
            }
            else if (doorSelect == 1)
            {
                newEnemy.Position = new Vector3(0, cameraHeight / -2 - 5, 0);
            }
            else if (doorSelect == 2)
            {
                newEnemy.Position = new Vector3(cameraWidth / -2 - 5, 0, 0);
            }
            else
            {
                newEnemy.Position = new Vector3(cameraWidth / 2 + 5, 0, 0);
            }


            newEnemy.gameObject.SetActive(true);
            

        }
    }
}
