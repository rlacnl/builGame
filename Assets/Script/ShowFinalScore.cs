using UnityEngine;
using UnityEngine.UI;

public class ShowFinalScore : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        finalScoreText.text = "���� ���ھ� : " + ScoreManager.FinalScore.ToString();
    }
}
