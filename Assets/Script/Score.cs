using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    private float timer = 0f;

    [Header("Milestone UI")]
    public Image milestoneImage;
    public Image milestoneTextImage;

    [Header("Milestone Data")]
    public Sprite[] milestoneSprites;  
    public Sprite[] milestoneTextSprites;

    private int lastMilestoneIndex = -1;

    void Start()
    {
        UpdateScoreText();
        ShowMilestoneUI(0);
        lastMilestoneIndex = 0;
        StartGame startGameScript = gameObject.GetComponent<StartGame>();
        if (startGameScript != null)
        {
            startGameScript.enabled = true;
        }
    }

    void Update()
    {
        if (StopGameScript.IsPaused) return;

        if (score >= 600)
        {
            SceneManager.LoadScene("GameResultScene");  
            return;
        }

        timer += Time.deltaTime;
        float currentInterval = GetScoreInterval();

        if (timer >= currentInterval)
        {
            timer = 0f;
            AddScore(1);
        }

        int currentIndex = score / 100;
        int maxIndex = Mathf.Min(milestoneSprites.Length, milestoneTextSprites.Length) - 1;

        if (currentIndex != lastMilestoneIndex && currentIndex <= maxIndex)
        {
            ShowMilestoneUI(currentIndex);
            lastMilestoneIndex = currentIndex;
        }
    }

    public void AddScore(int amount)
    {
        if (score >= 600)
            return;

        score += amount;
        UpdateScoreText();
    }

    private float GetScoreInterval()
    {
        return 0.4f;
    }

    private void ShowMilestoneUI(int index)
    {
        if (milestoneImage != null && milestoneSprites.Length > index)
        {
            milestoneImage.sprite = milestoneSprites[index];
            milestoneImage.gameObject.SetActive(true);
        }

        if (milestoneTextImage != null && milestoneTextSprites.Length > index)
        {
            milestoneTextImage.sprite = milestoneTextSprites[index];
            milestoneTextImage.gameObject.SetActive(true);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreText();
        int index = score / 100;
        ShowMilestoneUI(index);
        lastMilestoneIndex = index;
    }

    public int GetScore()
    {
        return score;
    }

    // 기존 Score 클래스 내부

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        ShowMilestoneUI(0);
        lastMilestoneIndex = 0;
    }

}
