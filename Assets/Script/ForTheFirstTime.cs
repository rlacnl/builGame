using UnityEngine;
using UnityEngine.UI;

public class ForTheFirstTime : MonoBehaviour
{
    public GameObject userObject;          // ���� ���ӿ�����Ʈ
    private Vector3 userStartPosition;

    public Text scoreText;                 // ���� UI �ؽ�Ʈ
    public Score scoreScript;              // ���� ��ũ��Ʈ ���� ����

    public StopGameScript stopGameScript; // �Ͻ����� ��ũ��Ʈ ����

    void Start()
    {
        if (userObject != null)
            userStartPosition = userObject.transform.position;
    }

    public void RestartGame()
    {
        // 1. �Ͻ����� ����
        if (stopGameScript != null && StopGameScript.IsPaused)
            stopGameScript.ResumeGame();

        // 2. ���� ��ġ �ʱ�ȭ
        ResetUserPosition();

        // 3. ���� �ʱ�ȭ (Score ��ũ��Ʈ ���� �� UI)
        ResetScore();

        // 4. ������� �ٽ� ���
        if (stopGameScript != null && stopGameScript.bgmSource != null)
        {
            stopGameScript.bgmSource.Stop();
            stopGameScript.bgmSource.Play();
        }

        // 5. �Ͻ����� Ƚ�� �ʱ�ȭ
        if (stopGameScript != null)
            stopGameScript.ResetPauseCount();
    }

    private void ResetUserPosition()
    {
        if (userObject != null)
        {
            userObject.transform.position = userStartPosition;

            Rigidbody2D rb = userObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }

    private void ResetScore()
    {
        if (scoreScript != null)
        {
            scoreScript.ResetScore();
        }

        if (scoreText != null)
        {
            scoreText.text = "score : 0";
        }
    }
}
