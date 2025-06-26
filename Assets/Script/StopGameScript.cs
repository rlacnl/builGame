using UnityEngine;
using UnityEngine.UI;

public class StopGameScript : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject gameStopPanel;
    public Animator groundAnimator;
    public User userScript;
    public AudioSource bgmSource;
    public MakePrefab makePrefabScript;
    public Score scoreScript;

    public int maxPauseCount = 3; // 최대 일시정지 횟수
    public int remainingPauseCount; // 남은 일시정지 횟수 (public)

    public Text pauseCountText; // UI용 텍스트

    void Start()
    {
        ResetPauseCount();
    }

    public void StopGame()
    {
        if (remainingPauseCount <= 0)
        {
            Debug.Log("더 이상 일시정지 할 수 없습니다.");
            return;
        }

        gameStopPanel.SetActive(true);

        groundAnimator.speed = 0;
        if (userScript != null) userScript.enabled = false;
        if (bgmSource != null) bgmSource.Pause();
        if (makePrefabScript != null) makePrefabScript.enabled = false;
        if (scoreScript != null) scoreScript.enabled = false;

        if (makePrefabScript != null && makePrefabScript.activePrefabs != null)
        {
            foreach (var obj in makePrefabScript.activePrefabs)
            {
                if (obj != null)
                    Destroy(obj);
            }
            makePrefabScript.activePrefabs.Clear();
        }

        IsPaused = true;

        remainingPauseCount--;
        UpdatePauseCountUI();

        Debug.Log($"일시정지 횟수 남음: {remainingPauseCount}");
    }

    public void ResumeGame()
    {
        gameStopPanel.SetActive(false);

        groundAnimator.speed = 1;
        if (userScript != null) userScript.enabled = true;
        if (bgmSource != null) bgmSource.Play();
        if (makePrefabScript != null) makePrefabScript.enabled = true;
        if (scoreScript != null) scoreScript.enabled = true;

        IsPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) ResumeGame();
            else StopGame();
        }
    }

    private void UpdatePauseCountUI()
    {
        if (pauseCountText != null)
        {
            pauseCountText.text = $"일시정지 남은 횟수: {remainingPauseCount}";
        }
    }

    // 다시하기 버튼 눌렀을 때 호출할 메서드
    public void ResetPauseCount()
    {
        remainingPauseCount = maxPauseCount;
        UpdatePauseCountUI();
        Debug.Log("일시정지 횟수 초기화됨");
    }
}
