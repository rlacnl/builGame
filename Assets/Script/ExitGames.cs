using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExitGames : MonoBehaviour
{
    public void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // ������ ��� ����
#else
        Application.Quit(); // ����� ���� ����
#endif
    }
}
