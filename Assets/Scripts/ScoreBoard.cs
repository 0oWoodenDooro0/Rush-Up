using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public static int Score;
    private Text showScore;

    private void Start()
    {
        showScore = GetComponent<Text>();
        Score = 0;
    }

    private void Update()
    {
        show();
    }

    private void show()
    {
        showScore.text = "Score:" + Score;
    }
}