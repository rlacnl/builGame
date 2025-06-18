using UnityEngine;
using UnityEngine.UI;

public class ShowFinalScore : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "최종 스코어 : " + ScoreManager.FinalScore.ToString();
    }
}
