using UnityEngine;
using UnityEngine.UI;

public class ForTheFirstTime : MonoBehaviour
{
    public GameObject userObject;          // 유저 게임오브젝트
    private Vector3 userStartPosition;

    public Text scoreText;                 // 점수 UI 텍스트
    public Score scoreScript;              // 점수 스크립트 직접 참조

    public StopGameScript stopGameScript; // 일시정지 스크립트 참조

    void Start()
    {
        if (userObject != null)
            userStartPosition = userObject.transform.position;
    }

    public void RestartGame()
    {
        // 1. 일시정지 해제
        if (stopGameScript != null && StopGameScript.IsPaused)
            stopGameScript.ResumeGame();

        // 2. 유저 위치 초기화
        ResetUserPosition();

        // 3. 점수 초기화 (Score 스크립트 변수 및 UI)
        ResetScore();

        // 4. 배경음악 다시 재생
        if (stopGameScript != null && stopGameScript.bgmSource != null)
        {
            stopGameScript.bgmSource.Stop();
            stopGameScript.bgmSource.Play();
        }

        // 5. 일시정지 횟수 초기화
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
