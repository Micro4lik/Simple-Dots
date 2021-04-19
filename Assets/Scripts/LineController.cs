using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void DrawLine(List<Dot> selectedDots)
    {
        lineRenderer.enabled = true;

        lineRenderer.positionCount = selectedDots.Count;
        lineRenderer.SetPositions(selectedDots.Select(d => d.transform.position).ToArray());
    }

    public void SetActiveLineRenderer(bool state)
    {
        lineRenderer.enabled = state;
    }
}