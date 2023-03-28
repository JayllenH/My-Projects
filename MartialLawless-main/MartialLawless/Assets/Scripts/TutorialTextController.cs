using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{

    private bool shieldTutorialVisible = false;
    private float shieldTutorialTimer;
    [SerializeField]
    private GameObject shieldTutorialText;
    // Start is called before the first frame update
    void Start()
    {
        shieldTutorialTimer = 4.0f;
        //shieldTutorialVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldTutorialVisible)
        {
            shieldTutorialTimer -= Time.deltaTime;
            if(shieldTutorialTimer <= 0)
            {
                shieldTutorialText.SetActive(false);
                shieldTutorialVisible = false;
            }
        }
    }

    public void ShowShieldTutorial()
    {
        shieldTutorialText.SetActive(true);
        shieldTutorialVisible = true;
    }
}
