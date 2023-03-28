using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour
{
    //booleans to determine if the enemy is being thrown and its trajectory
    private bool isVertical;
    private bool isThrown;

    //the player being thrown and the enemy doing the throw
    private BoxCollider2D enemy;
    private BoxCollider2D playerCollider;
    private PlayerController playerController;
    //damage dealt to enemy after they land
    private int damage;

    //vectors to determine the trajectory of the enemy
    private Vector3 landingLocation;
    private Vector3 currentLocation;
    private Vector3 centerPivot;
    private Vector3 startCenter;
    private Vector3 endCenter;

    //sounds

    public Orientation orientation;

    public PlayerController PlayerController
    {
        set { playerController = value; }
    }

    private AttackCollision hitBox;

    void Start()
    {
        isThrown = false;
        hitBox = gameObject.GetComponent<AttackCollision>();
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (playerCollider != null)
        {
            //if the enemy's current position is not the desired landing position
            if (Vector3.Distance(currentLocation, landingLocation) > 0.3)
            {
                //move enemy along arc towards landing position
                ArcPosition(currentLocation, landingLocation, 2);

                //update the enemy's current location
                currentLocation = playerController.Position;
            }
            else
            {
                //player is damaged
                playerController.Damage(damage);
                
                //player is no longer being thrown in an arc
                playerCollider = null;
                isThrown = false;
            }


        }
    }

    public void ThrowPlayer(BoxCollider2D player, BoxCollider2D enemy, int damage)
    {

        //if the enemy is not currently being thrown
        if (!isThrown)
        {

            player.GetComponent<PlayerController>().Position = player.gameObject.transform.position + new Vector3(0.0f, 0.5f);
            //current location is updated to the passed in enemy's position and the
            //passed in enemy is set
            currentLocation = player.GetComponent<PlayerController>().Position;
            this.playerCollider = player;

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
        playerCollider.GetComponent<PlayerController>().Position = Vector3.Slerp(startCenter, endCenter, Time.deltaTime * 2.5f) + centerPivot;
        //enemy.GetComponent<EnemyAI>().punch.Position = Vector3.Slerp(startCenter, endCenter, Time.deltaTime * 2.5f) + centerPivot;

    }
}
