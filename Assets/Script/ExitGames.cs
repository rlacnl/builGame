using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitGames : MonoBehaviour
{
    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 에디터 모드 종료
#else
        Application.Quit(); // 빌드된 게임 종료
#endif
    }
}
