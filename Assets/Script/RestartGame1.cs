using UnityEngine;

public class RestartGame1 : MonoBehaviour
{
   public StopGameScript stopGameScript; // �ν����Ϳ��� ����

    public void OnPauseResumeButtonClick()
    {
        if (stopGameScript == null)
        {
            Debug.LogError("StopGameScript�� ������� �ʾҽ��ϴ�!");
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
