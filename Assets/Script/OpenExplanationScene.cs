using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenExplanationScene : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("explanationScene");  // GameScene이라는 씬을 로드합니다
    }
}
