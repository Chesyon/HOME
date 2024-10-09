using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public int score;
    // Update is called once per frame
    public void IncreaseScore(int points)
    {
        score += points;
        ScoreText.text = score.ToString();
    }
}
