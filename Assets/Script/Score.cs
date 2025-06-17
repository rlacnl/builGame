using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;         // �ν����Ϳ��� ����
    private int score = 0;

    private float scoreInterval = 1.0f;   // ���� �� 1�ʸ��� ����
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

        // 100�� ������ �ӵ� ���� (�ִ� �Ѱ� ���� ����)
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
    public Text scoreText;             // �ν����Ϳ��� ����
    private int score = 0;

    private float scoreInterval = 1.0f;   // ���� �� 1�ʸ��� ����
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

        // 100�� ������ �ӵ� ���� (�ִ� �Ѱ� ���� ����)
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
