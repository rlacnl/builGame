using UnityEngine;

public class RestartGame1 : MonoBehaviour
{
   public StopGameScript stopGameScript; // 인스펙터에서 연결

    public void OnPauseResumeButtonClick()
    {
        if (stopGameScript == null)
        {
            Debug.LogError("StopGameScript가 연결되지 않았습니다!");
            return;
        }

        if (StopGameScript.IsPaused)
        {
            stopGameScript.ResumeGame();
        }
        else
        {
            stopGameScript.StopGame();
        }
    }
}
