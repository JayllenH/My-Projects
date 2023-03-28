using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{

    private BoxCollider2D collider;
    public Manager manager;

    private PlayerController player;
    private BoxCollider2D playerCollider;

    private List<BoxCollider2D> enemyList;
    private int damage;
    private bool isPlayer = true;

    public Throw throwObject;
    public EnemyThrow enemyThrowObject;
    public bool isThrow;
    //stores the parent enemy of attack boxes, isn't used for player attack boxes so it should be null for those
    private EnemyAI parentEnemy;
    private BoxCollider2D parentEnemyCollider;
    
    private bool isActive;

    public int Damage
    {
        get { return damage; }
        set {  damage = value; }
    }
    public bool IsPlayer
    {
        set { isPlayer = value; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public List<BoxCollider2D> EnemyList
    {
        get { return enemyList; }
        set { enemyList = value; }
    }

    public EnemyAI ParentEnemy
    {
        set { parentEnemy = value; }
    }

    public PlayerController Player
    {
        set { player = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isThrow)
        {
            Debug.Log('t');
        }
        
        isActive = false;

        collider = GetComponent<BoxCollider2D>();

        //stores colliders for each enemy
        enemyList = new List<BoxCollider2D>();

        player = manager.Player;
        playerCollider = player.gameObject.GetComponent<BoxCollider2D>();

        if(parentEnemy != null)
        {
            parentEnemyCollider = parentEnemy.gameObject.GetComponent<BoxCollider2D>();
        }
        for(int i = 0; i < manager.EnemyList.Count; i++)
        {
            enemyList.Add(manager.EnemyList[i].GetComponent<BoxCollider2D>());
        }    
    }

    // Update is called once per frame
    void Update()
    {

       
        //prevents collision hitboxes from killing enemies while they are not being used to attack
        if (isActive)
        {
            if (isPlayer)
            {

             
            
                //checks collisions on each enemy
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i] != null)
                    {
                        if (collider.IsTouching(enemyList[i]))
                        {
                            //prevents player stun locking enemy
                            manager.EnemyList[i].WindUpTimer = 0;

                            if (isThrow)
                            {
                                throwObject.ThrowEnemy(enemyList[i], player.ReturnOrientation, playerCollider, damage);
                                //manager.EnemyList[i].GetComponent<SpriteRenderer>().color = Color.red;
                            }
                            else
                            {
                                //deals damage
                                manager.EnemyList[i].TakeDamage(damage);
                                //manager.EnemyList[i].GetComponent<SpriteRenderer>().color = Color.red;
                                isActive = false;

                               
                            }
                        }
                    }

                }
             

            }
            else
            {
                
                //checks collision on player
                if (collider.IsTouching(playerCollider) && manager.Player.DamageAble)
                {
                   
                    if (isThrow)
                    {
                        //calls throw behavior to move player along arc and deal delayed damage
                        enemyThrowObject.ThrowPlayer(playerCollider, parentEnemyCollider, damage);
                        isActive = false;
                    }
                    else
                    {
                        //throw damage is handled in the throw script itself
                        player.Damage(damage);
                        isActive = false;
                        this.transform.position = Vector3.zero;
                        
                    }
                    
                }
            }
        }
        //prevents attack hit box from being offset when its parent enemy get's thrown
        else
        {
            if (!isPlayer && this.transform.position != parentEnemy.transform.position)
            {
                this.transform.position = parentEnemy.transform.position;
            }
        }
        

       


        
    }
    /*
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if(collision is EnemyAI)
        {
            Debug.Log("Enemy Hit");
        }
    }
    */
}
