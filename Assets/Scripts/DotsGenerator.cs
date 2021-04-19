using System.Collections.Generic;
using UnityEngine;

public class DotsGenerator : MonoBehaviour
{
    [SerializeField] private Transform dotsGrid;
    [SerializeField] private Dot emptyDot;

    private readonly List<Dot> _dots = new List<Dot>();
    private readonly Dictionary<int, Color32> _colorPalette = new Dictionary<int, Color32>();

    public void GenerateDotsGrid(int rows, int columns, int colorsAmount)
    {
        GenerateColors(colorsAmount);

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                CreateDot(row, column);
            }
        }
    }


    public void CreateDot(int xPos, int yPos)
    {
        Vector2 dotPos = new Vector2(xPos / 1.25f, yPos / 1.25f);
        var dot = Instantiate(emptyDot, dotPos, Quaternion.identity, dotsGrid);
        dot.name = $"Dot ({xPos}) ({yPos})";
        var randomIndex = Random.Range(0, _colorPalette.Count);
        dot.Setup(randomIndex, _colorPalette[randomIndex], new Vector2Int(xPos, yPos));
        _dots.Add(dot);
    }

    private void GenerateColors(int colorsAmount)
    {
        var baseColor = UnityEngine.Random.ColorHSV(0, 1, 0.5f, 1f, 1f, 1f, 1f, 1f);
        Color.RGBToHSV(baseColor, out var baseH, out var baseS, out var baseV);
        for (int i = 0; i < colorsAmount; i++)
        {
            var convertedColor = Color.HSVToRGB((baseH + ((float) i) / colorsAmount) % 1f, baseS, baseV);
            _colorPalette.Add(i,
                new Color32((byte) (convertedColor.r * 255), (byte) (convertedColor.g * 255),
                    (byte) (convertedColor.b * 255), 255));
        }
    }
}