using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAI : MonoBehaviour
{
    private Orientation orientation;

    [SerializeField]
    protected Transform playerTransform;
    private PlayerController player;

    
    private float moveSpeed = 2.0f; // units per second
    private float stopDistance = 1.35f; // units away the enemy stops to attack the player
    public float attackTimer = 0.0f; // seconds
    private float attackCooldown = 1.0f; // seconds between attacks
    private float throwTimer = 0.0f;
    private float throwDuration = 0.5f; // seconds
    private float kickDuration = 0.3f; // seconds
    private float punchDuration = 0.05f; // seconds
    private bool onCooldown = true;

    protected float windUp;
    private float windUpTimer = 0.0f;

    private Vector2 position;

    private List<EnemyAI> enemies;

    // Copied from PlayerController.cs
    private State state;

    //stats are serialized so they can be edited in the inspector
    [SerializeField]
    private int punchDamage = 10;
    [SerializeField]
    private int kickDamage = 20;
    [SerializeField]
    private int throwDamage = 25;
    

    //different sprites to show for each pose
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    private float animatorTimer = 0.0f;
    private bool animationAttacking = false;

    //variables for controlling combat
    [SerializeField]
    private AttackCollision punch;
    [SerializeField]
    private AttackCollision kick;
    [SerializeField]
    private AttackCollision throwBox;
    private EnemyThrow throwScript;

    private List<AttackCollision> attacks;

    public Manager gameManager;
    [SerializeField]
    public AudioSource gruntSound;

    private PauseController pauseController;

    private float hitIndicatorInterval;
    private float hitIndicatorTimer;

    private bool isTrapping;

    public AttackCollision PunchObj
    {
        get { return punch; }
    }

    public Vector3 Position
    {
        get{return position;}
        set{position = value;}
    }

    public Transform PlayerTransform
    {
        set { playerTransform = value; }
    }

    public Orientation Orientation
    {
        get { return orientation;}
    }

    public PauseController PauseController
    {
        set { pauseController = value; }
    }

    [SerializeField]
    private int health = 1;

     public int Health
    {
        set { health = value; }
        get { return health; }
    }

    public float WindUpTimer
    {
        get { return windUpTimer; }
        set { windUpTimer = value; }
    }

    public PlayerController Player
    {
        set { player = value; }
        get { return player; }
    }

    
    // Start is called before the first frame update
    protected void Start()
    {
        hitIndicatorInterval = 0.4f;
        hitIndicatorTimer = hitIndicatorInterval;

        orientation = Orientation.up;
        state = State.isMoving;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        attacks = new List<AttackCollision>();


        //intializes the punch hit box
        punch.manager = gameManager;
        punch.Damage = punchDamage;
        punch.IsPlayer = false;
        punch.ParentEnemy = this;
        punch.Player = this.player;
       

        //initializes the kick hit box
        kick.manager = gameManager;
        kick.Damage = kickDamage;
        kick.IsPlayer = false;
        kick.ParentEnemy = this;
        kick.Player = this.player;

        throwBox.manager = gameManager;
        throwBox.Damage = throwDamage;
        throwBox.IsPlayer = false;
        throwBox.isThrow = true;
        throwBox.ParentEnemy = this;
        throwBox.Player = this.player;
        throwScript = throwBox.GetComponent<EnemyThrow>();
        throwScript.PlayerController = player;

        gruntSound = GameObject.FindGameObjectWithTag("gun").GetComponent<AudioSource>();
        onCooldown = true;

            windUp = 0.6f;

        isTrapping = false;
        
    }

    // Update is called once per frame
    protected void Update()
    {
        //checks if the game has been paused before updating
        if (!pauseController.IsPaused)
        {


            //gruntSound.enabled = true;

            // Get the player's position this frame
            Vector2 playerPosition = (Vector2)playerTransform.position;
            //position = transform.position;
            // Get the vector from this enemy to the player
            Vector2 moveVector;
            if (isTrapping)
            {
                if(playerPosition.y > transform.position.y)
                {
                    moveVector = ( new Vector2(playerPosition.x, playerPosition.y + 5)) - (Vector2)transform.position;
                }
                else
                {
                    moveVector = (new Vector2(playerPosition.x, playerPosition.y - 5)) - (Vector2)transform.position;
                }
            }
            else
            {
                 moveVector = playerPosition - (Vector2)transform.position;
            }
           

            // Get an updated list of other active enemies
            enemies = gameManager.EnemyList;

            Vector2 personalSpaceVector = Vector2.zero;
            for (int i = 0; i < enemies.Count; i++)
            {
                // If this enemy is in this enemy's personal space
                if ((enemies[i].Position - transform.position).sqrMagnitude < Mathf.Pow(stopDistance, 2))
                {
                    // Move away from them
                    personalSpaceVector += (Vector2)(transform.position - enemies[i].Position).normalized;
                }
            }

            moveVector = moveVector.normalized;
            moveVector += personalSpaceVector.normalized;

            moveVector = moveVector.normalized;

            if (state != State.isIdle)
            {
                // UP
                if (moveVector.y > Mathf.Abs(moveVector.x) && moveVector.y >= 0)
                {
                    orientation = Orientation.up;
                }
                // DOWN
                else if (moveVector.y < 0 && Mathf.Abs(moveVector.y) > Mathf.Abs(moveVector.x))
                {
                    orientation = Orientation.down;
                }
                // RIGHT
                else if (moveVector.x > 0)
                {
                    orientation = Orientation.right;
                }
                // LEFT
                else if (moveVector.x < 0)
                {
                    orientation = Orientation.left;
                }
                //Debug.Log("Orientation: " + orientation);
            }

            // Animation logic
            if (animationAttacking && animatorTimer <= 0.7f)
            {
                animatorTimer += Time.deltaTime;
            }
            else
            {
                animationAttacking = false;
                animatorTimer = 0.0f;
                animator.SetBool("isPunching", false);
                animator.SetBool("isKicking", false);
                animator.SetBool("isMoving", state == State.isMoving);
                animator.SetInteger("orientation", (int)orientation);
            }

            /*
            if (onCooldown)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackCooldown)
                {
                    onCooldown = false;
                    attackTimer = 0.0f;
                }
            }
            */

            if (spriteRenderer.color == Color.red)
            {
                if (hitIndicatorTimer <= 0)
                {
                    hitIndicatorTimer = hitIndicatorInterval;
                    spriteRenderer.color = Color.white;
                }
                else
                {
                    hitIndicatorTimer -= Time.deltaTime;
                }
            }

            switch (state)
            {
                case State.isIdle:
                    // If this enemy is out of range
                    if ((playerPosition - (position + (moveVector * moveSpeed * Time.deltaTime))).sqrMagnitude > Mathf.Pow(stopDistance, 2) && windUpTimer == 0)
                    {
                        state = State.isMoving;
                    }

                    break;

                case State.isMoving:
                    // If it's already inside the radius
                    if ((playerPosition - position).sqrMagnitude <= Mathf.Pow(stopDistance, 2))
                    {
                        // Don't move
                        state = State.isIdle;
                    }
                    // If the new position would be inside the stopDistance radius
                    else if ((playerPosition - (position + (moveVector * moveSpeed * Time.deltaTime))).sqrMagnitude < Mathf.Pow(stopDistance, 2))
                    {
                        // Apply the movement but only to the edge of that circle
                        position += moveVector * ((playerPosition - position).magnitude - stopDistance);
                        state = State.isIdle;
                    }
                    else
                    {
                        position += moveVector * moveSpeed * Time.deltaTime;
                    }

                    transform.position = position;

                    break;
                //currently not being implimented
                case State.isBlocking:

                    break;

                case State.isKicking:
                    attackTimer += Time.deltaTime;

                    if (attackTimer > kickDuration)
                    {
                        //after 60 cycles the player is able to move again
                        //onCooldown = true;
                        attackTimer = 0;
                        //returns punch hitbox to intial position
                        kick.gameObject.transform.position = position;
                        kick.IsActive = false;
                        state = State.isMoving;
                    }

                    break;

                case State.isPunching:
                    attackTimer += Time.deltaTime;

                    if (attackTimer >= punchDuration)
                    {
                        //after 60 cycles the player is able to move again
                        //onCooldown = true;
                        attackTimer = 0;
                        punch.gameObject.transform.position = position;
                        punch.IsActive = false;
                        state = State.isMoving;
                    }

                    break;

                case State.isThrowing:
                    attackTimer += Time.deltaTime;

                    if (attackTimer >= throwDuration)
                    {
                        onCooldown = true;
                        attackTimer = 0.0f;
                        throwBox.gameObject.transform.position = position;
                        throwBox.IsActive = false;
                        state = State.isMoving;
                    }


                    break;

                case State.isStunned:

                    break;

            }


            if (state == State.isIdle)
            {
                if (windUpTimer >= windUp)
                {
                    windUpTimer = 0;
                    //Throw();
                    
                    //randomly selects the enemy's attack when they are in range
                    int selector = Random.Range(0, 11);
                    if (selector <= 5)
                    {
                        Punch();
                    }
                    else if (selector < 9)
                    {
                        Kick();

                    }
                    else
                    {
                        Throw();
                    }
                    
                    
                }
                else
                {
                    windUpTimer += Time.deltaTime;
                }


            }
        }
    }

    private void Punch()
    {

        
        gruntSound.enabled = true;

        if (gruntSound != null)
        {
            Debug.Log(gruntSound.isActiveAndEnabled);
            gruntSound.Play();
            Debug.Log("grunt Played");
        }
        Debug.Log("Enemy punch");
        state = State.isPunching;

        animationAttacking = true;
        animator.SetBool("isPunching", true);

        //AttackCollision newPunch;

        //checks for orientation and spawns a hitbox in front of the player
        //checks for orientation and spawns a hitbox in front of the player
        switch (orientation)
        {

            case Orientation.up:
                //punch.gameObject.SetActive(true);
                punch.gameObject.transform.position = new Vector2(position.x, position.y + 0.5f);

                break;

            case Orientation.down:
                punch.gameObject.transform.position = new Vector2(position.x, position.y - 0.5f);


                break;

            case Orientation.left:
                punch.gameObject.transform.position = new Vector2(position.x - 0.5f, position.y);

                break;
            case Orientation.right:
                punch.gameObject.transform.position = new Vector2(position.x + 0.5f, position.y);

                break;
        }

        punch.IsActive = true;

        
    }

    private void Kick()
    {

        
        Debug.Log("Enemy kick");
        state = State.isKicking;

        animationAttacking = true;
        animator.SetBool("isKicking", true);

        //AttackCollision newKick = null;

        //checks for orientation and spawns a hitbox in front of the player
        //checks for orientation and spawns a hitbox in front of the player
        switch (orientation)
        {
            case Orientation.up:
                kick.gameObject.transform.position = new Vector2(position.x, position.y + 0.5f);

                break;
            case Orientation.down:
                kick.gameObject.transform.position = new Vector2(position.x, position.y - 0.5f);

                break;
            case Orientation.left:
                kick.gameObject.transform.position = new Vector2(position.x - 0.5f, position.y);

                break;
            case Orientation.right:
                kick.gameObject.transform.position = new Vector2(position.x + 0.5f, position.y);

                break;
        }
        //sound effect here
        gruntSound.enabled = true;
        if (gruntSound != null)
        {
            gruntSound.Play();
            Debug.Log("grunt Played");
        }
        kick.IsActive = true;
        
    }

    private void Throw()
    {
        Debug.Log("Enemy throw");
        state = State.isThrowing;

        //AttackCollision newKick = null;

        //checks for orientation and spawns a hitbox in front of the player
        //checks for orientation and spawns a hitbox in front of the player
        switch (orientation)
        {
            case Orientation.up:
                throwBox.gameObject.transform.position = new Vector2(position.x, position.y + 0.5f);

                break;
            case Orientation.down:
                throwBox.gameObject.transform.position = new Vector2(position.x, position.y - 0.5f);

                break;
            case Orientation.left:
                throwBox.gameObject.transform.position = new Vector2(position.x - 0.5f, position.y);

                break;
            case Orientation.right:
                throwBox.gameObject.transform.position = new Vector2(position.x + 0.5f, position.y);

                break;
        }

        throwScript.orientation = this.orientation;

        //sound effect here
        gruntSound.enabled = true;
        if (gruntSound != null)
        {
            gruntSound.Play();
            Debug.Log("grunt Played");
        }
        throwBox.IsActive = true;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        spriteRenderer.color = Color.red;

    }
    public void ScaleDamage(float multiplier)
    {
        punchDamage = (int)Mathf.Floor(punchDamage * multiplier);
        kickDamage = (int)Mathf.Floor(kickDamage * multiplier);
        throwDamage = (int)Mathf.Floor(throwDamage * multiplier);

    }

    public void toggleTrapping()
    {
        isTrapping = !isTrapping;
    }
}
