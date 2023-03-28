using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    //booleans to determine if the enemy is being thrown and its trajectory
    private bool isVertical;
    private bool isThrown;
    
    //the enemy being thrown and the player
    private BoxCollider2D enemy;
    private BoxCollider2D player;

    //damage dealt to enemy after they land
    private int damage;

    //vectors to determine the trajectory of the enemy
    private Vector3 landingLocation;
    private Vector3 currentLocation;
    private Vector3 centerPivot;
    private Vector3 startCenter;
    private Vector3 endCenter;

    EnemyAI enemyAI;

    //sounds

    void Start()
    {
        isThrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the enemy has been set
        if(enemy != null)
        {
            enemyAI = enemy.GetComponent<EnemyAI>();
            //if the enemy's current position is not the desired landing position
            if (Vector3.Distance(currentLocation, landingLocation) > 0.3)
            {
                //move enemy along arc towards landing position
                ArcPosition(currentLocation, landingLocation, 2);

                //update the enemy's current location
                currentLocation = enemy.GetComponent<EnemyAI>().Position;
            }
            else
            {
                //enemy is damaged
                enemyAI.TakeDamage(damage);

                if(enemyAI is ShieldEnemy)
                {
                    enemy.GetComponent<ShieldEnemy>().RemoveShield();
                }

                //enemy is no longer being thrown in an arc
                enemy = null;
                isThrown = false;
            }

            
        }
        
    }

    //ThrowEnemy is called when an enemy collides with a throw box
    public void ThrowEnemy(BoxCollider2D enemy, Orientation orientation, BoxCollider2D player, int damage)
    {
        //if the enemy is not currently being thrown
        if(!isThrown)
        {

            enemy.GetComponent<EnemyAI>().Position = player.gameObject.transform.position + new Vector3(0.0f, 0.5f);
            //current location is updated to the passed in enemy's position and the
            //passed in enemy is set
            currentLocation = enemy.GetComponent<EnemyAI>().Position;
            this.enemy = enemy;

            //damage is updated
            this.damage = damage;

            switch (orientation)
            {
                //if the player is facing upwards
                case Orientation.up:
                    landingLocation = currentLocation + new Vector3(0.0f, 5.0f);
                    isVertical = true;
                    break;

                //if the player is facing downwards
                case Orientation.down:
                    landingLocation = currentLocation + new Vector3(0.0f, -5.0f);

                    isVertical = true;
                    break;

                //if the player is facing left
                case Orientation.left:
                    landingLocation = currentLocation + new Vector3(-5.0f, -0.5f);
                    isVertical = false;
                    break;

                //if the player is facing right
                case Orientation.right:
                    landingLocation = currentLocation + new Vector3(5.0f, -0.5f);
                    isVertical = false;
                    break;

            }

            //the enemy is now being thrown
            isThrown = true;
           
        }
        
    }
    
    //ArcPosition determines the enemy's trajectory to their landing position
    public void ArcPosition(Vector3 startPos, Vector3 endPos, float offset)
    {
        //center pivot is halfway between the start and end position
        centerPivot = (startPos + endPos) * 0.5f;

        //if the enemy is being thrown vertically
        if (isVertical)
        {
            //the pivot is offset in the X axis
            centerPivot -= new Vector3(offset, 0);
        }
        else
        {
            //the pivot is offset in the Y axis
            centerPivot -= new Vector3(0, offset);
        }

        //the center of the start and end positions are determined from the center pivot
        startCenter = startPos - centerPivot;
        endCenter = endPos - centerPivot;

        //enemy's position is slerped along the determined coordinates
        enemy.GetComponent<EnemyAI>().Position = Vector3.Slerp(startCenter, endCenter, Time.deltaTime * 2.5f) + centerPivot;
        //enemy.GetComponent<EnemyAI>().punch.Position = Vector3.Slerp(startCenter, endCenter, Time.deltaTime * 2.5f) + centerPivot;

    }
}
