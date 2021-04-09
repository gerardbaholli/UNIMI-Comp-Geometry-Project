using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] PointColor typeColor;
    Action onPositionChanged;

    public enum PointColor
    {
        none = 0,
        green = 1,
        white = 2
    }

    private bool locked;
    private Vector3 mouseOffset;
    private float mouseZCoordinate;

    public Vector2 CurrentPosition => this.transform.position;
    public PointColor TypeColor => typeColor;

    public void SetContext(Action onPositionChanged)
    {
        this.onPositionChanged = onPositionChanged;
    }

    void OnMouseDown()
    {
        locked = true;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        locked = true;
        transform.position = GetMouseWorldPos() + mouseOffset;
        onPositionChanged?.Invoke();
    }

    private void OnMouseUp()
    {
        locked = false;
        onPositionChanged?.Invoke();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoordinate;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void SetPosition(Vector2 position)
    {
        if (!locked)
        {
            this.transform.position = position;
        }
    }


}
