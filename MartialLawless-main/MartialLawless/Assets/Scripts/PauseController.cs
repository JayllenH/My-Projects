using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseController : MonoBehaviour
{
    private bool isPaused;

    [SerializeField]
    private Image greyFilter;
    [SerializeField]
    private GameObject pauseContent;
    [SerializeField]
    private AudioSource pauseMusic;
    [SerializeField]
    private Manager manager;

    public bool IsPaused
    {
        get { return isPaused; }
    }
    // Start is called before the first frame update
    void Start()
    {
        //greyFilter.color = new Color(190, 190, 190, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPauseScreen()
    {
        isPaused = true;

        //greys out game content
        greyFilter.color = new Color(190, 190, 190, 0.7f);

        //shows pause menu
        pauseContent.SetActive(true);

        //starts the pause music and pauses the game music
        pauseMusic.Play();
        manager.currentMusic.Pause();
    
    }

    public void HidePauseScreen()
    {
        isPaused = false;
        //makes the filter transparent
        greyFilter.color = new Color(190, 190, 190, 0.0f);
        //hides pause menus
        pauseContent.SetActive(false);

        //ends the pause music and restarts the game music
        pauseMusic.Stop();
        manager.currentMusic.Play();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
