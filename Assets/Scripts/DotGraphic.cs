using UnityEngine;

public class DotGraphic : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;

    public void SetColor(Color32 color)
    {
        image.color = color;
    }
}
