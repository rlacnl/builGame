using UnityEngine;

public class StopGameScript : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject gameStopPanel;
    public Animator groundAnimator;
    public User userScript;
    public AudioSource bgmSource;
    public MonoBehaviour spawnScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                ResumeGame();
            else
                StopGame();
        }
    }

    public void OnStopButtonClick()
    {
        StopGame();
    }

    public void OnResumeButtonClick()
    {
        ResumeGame();
    }

    private void StopGame()
    {
        gameStopPanel.SetActive(true);
        groundAnimator.speed = 0;
        userScript.enabled = false;
        bgmSource.Pause();
        spawnScript.enabled = false;

        IsPaused = true;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        gameStopPanel.SetActive(false);
        groundAnimator.speed = 1;
        userScript.enabled = true;
        bgmSource.Play();
        spawnScript.enabled = true;

        IsPaused = false;
        Time.timeScale = 1f;
    }
}
