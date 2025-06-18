using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ✅ 이 줄 추가 필요

public class GameResultReStartGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");  // GameScene이라는 씬을 로드합니다
    }
}
