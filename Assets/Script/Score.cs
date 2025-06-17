using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;         // 인스펙터에서 연결
    private int score = 0;

    private float scoreInterval = 1.0f;   // 시작 시 1초마다 증가
    private float timer = 0f;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= scoreInterval)
        {
            timer = 0f;
            AddScore(1);
        }
    }

    void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        // 100점 단위로 속도 증가 (최대 한계 설정 가능)
        if (score % 100 == 0 && scoreInterval > 0.2f)
        {
            scoreInterval -= 0.1f;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;             // 인스펙터에서 연결
    private int score = 0;

    private float scoreInterval = 1.0f;   // 시작 시 1초마다 증가
    private float timer = 0f;

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= scoreInterval)
        {
            timer = 0f;
            AddScore(1);
        }
    }

    void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        // 100점 단위로 속도 증가 (최대 한계 설정 가능)
        if (score % 100 == 0 && scoreInterval > 0.2f)
        {
            scoreInterval -= 0.1f;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
