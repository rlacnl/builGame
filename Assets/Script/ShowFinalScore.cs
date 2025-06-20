using UnityEngine;
using UnityEngine.UI;

public class ShowFinalScore : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "Score : " + ScoreManager.FinalScore.ToString();
    }
}
