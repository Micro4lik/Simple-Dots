using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    public int dotsGridRowsCount = 6;
    public int dotsGridColumnsCount = 6;

    public int dotsColorsCount = 3;
    
    public readonly string PlayerPrefsScoreKey = "ScoreKey";
}