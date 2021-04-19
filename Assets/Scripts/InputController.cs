using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private bool _isDragging;

    public Action<Dot> onDotTouched;
    public Action onGetMouseButtonUp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
        }

        if (_isDragging)
        {
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, Mathf.Infinity);
            if (hit.collider)
            {
                var dot = hit.transform.GetComponent<Dot>();
                if (dot)
                {
                    onDotTouched?.Invoke(dot);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            onGetMouseButtonUp?.Invoke();
        }
    }
}