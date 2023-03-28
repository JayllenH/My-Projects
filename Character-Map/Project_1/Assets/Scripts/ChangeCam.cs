using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    [SerializeField]
    List<GameObject> cams = new List<GameObject>();

    [SerializeField]
    int activeCam;
    // Start is called before the first frame update
    void Start()
    {
        //for each cam we set it to false
        foreach(GameObject camera in cams)
        {
            camera.SetActive(false);
        }
        //call the method to change cam
        ChangeCamByIndex(activeCam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //change cam with given variable
    public void ChangeCamByIndex(int index)
    {
        //test to see if it works
        Debug.LogFormat("Change to camera: {0}", index);
        //make current false and set index cam to true and update active cam
        cams[activeCam].SetActive(false);

        cams[index].SetActive(true);

        activeCam = index;
    }
}
