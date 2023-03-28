using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    private Vector3 position;

    private float cameraHeight;
    private float cameraWidth;
    public Camera cameraObject;

    public GameObject backGround;
    private Bounds backGroundBounds;

    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = cameraObject.orthographicSize * 2f;
        cameraWidth = cameraHeight * cameraObject.aspect;

        backGroundBounds = backGround.GetComponent<SpriteRenderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        //sets camera position to player position
        position = new Vector3(player.Position.x, player.Position.y, -10);
        

        //checks to keep the camera from going past the edges of the play area
        if (position.x - cameraWidth/2 < backGroundBounds.min.x)
        {
            position.x = backGroundBounds.min.x + cameraWidth/2;
        }
        //uses else if to avoid doing both checks every single frame
        else if(position.x + cameraWidth/2 > backGroundBounds.max.x)
        {
            position.x = backGroundBounds.max.x - cameraWidth / 2;
        }

        if(position.y - cameraHeight/2 < backGroundBounds.min.y)
        {
            position.y = backGroundBounds.min.y + cameraHeight / 2;

        }
        else if(position.y + cameraHeight/2 > backGroundBounds.max.y)
        {
            position.y = backGroundBounds.max.y - cameraHeight / 2;
        }

        //updates the game object
        transform.position = position;
    }
}
