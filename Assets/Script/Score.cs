using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // ✅ 씬 전환을 위해 꼭 필요

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    private float timer = 0f;

    [Header("Milestone UI")]
    public Image milestoneImage;
    public Image milestoneTextImage;

    [Header("Milestone Data")]
    public Sprite[] milestoneSprites;      // milestoneImage에 쓸 스프라이트 추가
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
        if (score >= 600)
        {
            SceneManager.LoadScene("GameResultScene");  // ✅ 600점 도달 시 씬 전환
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
        float baseInterval = 1.0f;
        float intervalReduction = 0.25f * (score / 100);
        return Mathf.Max(baseInterval - intervalReduction, 0.1f);
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
}
