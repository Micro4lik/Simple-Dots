using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private DotGraphic dotGraphic;
    public Vector2Int Coordinates { get; private set; }
    public int ColorId { get; private set; }

    public bool isTouching;


    public void Setup(int colorId, Color32 color, Vector2Int coordinates)
    {
        ColorId = colorId;
        Coordinates = coordinates;
        
        dotGraphic.SetColor(color);
    }
}