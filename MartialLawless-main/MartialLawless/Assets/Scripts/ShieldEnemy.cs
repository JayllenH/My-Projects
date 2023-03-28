using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : EnemyAI
{
    [SerializeField]
    private GameObject shield;
    public PauseController temp;

    private bool hasShield;

    [SerializeField]
    private float shieldChangeInterval;
    private float shieldChangeTimer;

    public bool HasShield
    {
        set { hasShield = value; }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //base.PauseController = temp;
        windUp = 0.8f;
        shieldChangeTimer = shieldChangeInterval;
        hasShield = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (hasShield)
        {
            //uses short timer before checking to change sheild position to prevent it swaping back and forth every frame
            shieldChangeTimer -= Time.deltaTime;
            if(shieldChangeTimer <= 0)
            {
                shieldChangeTimer = shieldChangeInterval;
                switch (Orientation)
                {
                    case Orientation.up:
                        shield.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);
                        shield.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                        break;
                    case Orientation.down:
                        shield.transform.localPosition = new Vector3(0.0f, -0.6f, 0.0f);
                        shield.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                        break;
                    case Orientation.left:
                        shield.transform.localPosition = new Vector3(-0.6f, 0.0f, 0.0f);
                        shield.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        break;
                    case Orientation.right:
                        shield.transform.localPosition = new Vector3(0.6f, 0.0f, 0.0f);
                        shield.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                        break;
                }
            }
            
        }
       
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("Sheild enemy take damage");
        if (hasShield)
        {
            switch (Orientation)
            {
                case Orientation.up:
                    //if the player is attacking from above when the shield is positioned up it is blocked by the sheild and take damage isn't called
                    if (playerTransform.position.y < Position.y)
                    {
                        base.TakeDamage(damage);
                    }
                    break;
                case Orientation.down:
                    //if the player is attacking from below when the shield is positioned down it is blocked by the sheild and take damage isn't called
                    if (playerTransform.position.y >= Position.y)
                    {
                        base.TakeDamage(damage);
                    }
                    break;
                case Orientation.left:
                    //if the player is attacking from the left when the shield is to the left it is blocked by the sheild and take damage isn't called
                    if (playerTransform.position.x >= Position.x)
                    {
                        base.TakeDamage(damage);
                    }
                    break;
                case Orientation.right:
                    //if the player is attacking from the right when the shield is to the right it is blocked by the sheild and take damage isn't called
                    if (playerTransform.position.x < Position.x)
                    {
                        base.TakeDamage(damage);
                    }
                    break;
            }
        }
        else
        {
            base.TakeDamage(damage);
        }
    }


    public void RemoveShield()
    {
        hasShield = false;
        shield.SetActive(false);
    }

    public void AddShield()
    {
        hasShield = true;
        shield.SetActive(true);
    }
}
