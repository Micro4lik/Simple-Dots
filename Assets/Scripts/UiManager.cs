using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    public void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}
