using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("mainScene");  
    }
}
