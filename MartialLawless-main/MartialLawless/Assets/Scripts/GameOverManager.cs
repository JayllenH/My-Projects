using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    public AudioSource gameOverSong;

    // Start is called before the first frame update
    Scene mainMenu;
    void Start()
    {
        scoreText.text = "Enemies Killed: " + ScoreTracker.enemiesKilled;
        gameOverSong.enabled = true;
        if (gameOverSong != null)
        {
            gameOverSong.Play();
            Debug.Log("Sound Played");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        gameOverSong.Stop();
        SceneManager.LoadScene("MainMenu");
        ScoreTracker.enemiesKilled = 0;
    }
}
