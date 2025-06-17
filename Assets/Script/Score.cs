using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    private float timer = 0f;

    [Header("Milestone UI")]
    public Image milestoneImage;
    public Text milestoneText;

    [Header("Milestone Data")]
    public Sprite[] milestoneSprites;

    private string[] milestoneMessages = new string[]
    {
        "1라운드\n집",
        "2라운드\n버스정류장",
        "3라운드\n삼성여고",
        "4라운드\n운동장",
        "5라운드\n교실",
        "최종라운드\n출석 체크!!!"
    };

    private float[] scoreIntervals = new float[]
    {
        1.0f,     // 0~99
        0.825f,   // 100~199
        0.825f,   // 200~299
        0.8f,     // 300~399
        0.8f,     // 400~499
        0.7f      // 500~599
    };

    private int lastMilestoneIndex = -1;

    void Start()
    {
        UpdateScoreText();
        ShowMilestoneUI(0);
        lastMilestoneIndex = 0; 
        StartGame startGameScript = gameObject.GetComponent<StartGame>();
        if (startGameScript != null)
        {
            startGameScript.enabled = true; // 이건 MonoBehaviour가 제공하는 속성임
        }
    }

    void Update()
    {
        if (score >= 600)
            return;

        timer += Time.deltaTime;
        float currentInterval = GetScoreInterval();

        if (timer >= currentInterval)
        {
            timer = 0f;
            AddScore(1);
        }

        // 🟨 UI는 항상 현재 milestone에 맞춰 갱신
        int currentIndex = score / 100;
        if (currentIndex != lastMilestoneIndex && currentIndex < milestoneSprites.Length)
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
        int index = score / 100;
        if (index >= scoreIntervals.Length)
            return scoreIntervals[scoreIntervals.Length - 1];
        return scoreIntervals[index];
    }

    private void ShowMilestoneUI(int index)
    {
        if (milestoneImage != null && milestoneSprites.Length > index)
        {
            milestoneImage.sprite = milestoneSprites[index];
            milestoneImage.gameObject.SetActive(true);
        }

        if (milestoneText != null && milestoneMessages.Length > index)
        {
            milestoneText.text = milestoneMessages[index];
            milestoneText.gameObject.SetActive(true);
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
}
