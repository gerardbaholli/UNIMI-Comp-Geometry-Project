using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{

    private bool isFirstPoint = true;
    private bool isSecondPoint = false;
    private List<Point> listOfPoints = new List<Point>();

    [SerializeField] Point point;
    [SerializeField] Point controlPoint;

    public LineRenderer lineRenderer;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    void Update()
    {
        AddPoint();
        DrawCurve();
    }


    private void InstantiatePoint(Point point, Vector2 pos)
    {
        Point pointInstance = Instantiate(point, pos, Quaternion.identity) as Point;
        pointInstance.SetContext(UpdateAnchorPosition);
        listOfPoints.Add(pointInstance);
    }


    private void AddPoint()
    {
        if (level.PlaceableSplinePoints > 0)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.A))
            {
                Vector2 mousePosition = Input.mousePosition / Screen.width * 38.4f;

                if (isFirstPoint)
                {
                    isFirstPoint = !isFirstPoint;

                    InstantiatePoint(point, new Vector2(mousePosition.x, mousePosition.y));

                    isSecondPoint = true;

                    level.StartCountdownTimer();
                }
                else if (isSecondPoint)
                {
                    isSecondPoint = !isSecondPoint;

                    InstantiatePoint(controlPoint, new Vector2(listOfPoints[listOfPoints.Count - 1].CurrentPosition.x + 1f, listOfPoints[listOfPoints.Count - 1].CurrentPosition.y + 1f));

                    InstantiatePoint(controlPoint, new Vector2(mousePosition.x - 1f, mousePosition.y - 1f));

                    InstantiatePoint(point, new Vector2(mousePosition.x, mousePosition.y));

                }
                else
                {

                    InstantiatePoint(controlPoint, listOfPoints[listOfPoints.Count - 1].CurrentPosition * 2 - listOfPoints[listOfPoints.Count - 2].CurrentPosition);

                    InstantiatePoint(controlPoint, new Vector2(mousePosition.x - 1f, mousePosition.y - 1f));

                    InstantiatePoint(point, new Vector2(mousePosition.x, mousePosition.y));

                }

                level.RemovePlacebleSplinePoint();
            }
        }
        
    }


    private void DrawCurve()
    {
        if (listOfPoints.Count >= 4)
        {
            var pointList = new List<Vector3>();

            for (int i = 0; i < (listOfPoints.Count / 3); i++)
            {
                for (float ratio = 0f; ratio <= 1.01f; ratio += .05f)
                {
                    var cubicBezierCurve = CubicLerp(
                        listOfPoints[i * 3].CurrentPosition,
                        listOfPoints[i * 3 + 1].CurrentPosition,
                        listOfPoints[i * 3 + 2].CurrentPosition,
                        listOfPoints[i * 3 + 3].CurrentPosition,
                        ratio);

                    pointList.Add(cubicBezierCurve);
                }
            }


            lineRenderer.positionCount = pointList.Count;
            lineRenderer.SetPositions(pointList.ToArray());

            //lineRenderer.material.color = Color.white;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
    }


    // Invoke
    private void UpdateAnchorPosition()
    {
        if (listOfPoints.Count >= 7)
        {
            for (int i = 1; i < listOfPoints.Count - 1; i++)
            {
                if (listOfPoints[i].TypeColor == Point.PointColor.green)
                {
                    listOfPoints[i + 1].SetPosition(listOfPoints[i].transform.position * 2 - listOfPoints[i - 1].transform.position);
                    listOfPoints[i - 1].SetPosition(listOfPoints[i].transform.position * 2 - listOfPoints[i + 1].transform.position);
                }
            }
        }

    }


    public Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return Vector3.Lerp(a, b, t);
    }


    public Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }


    public Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ac = QuadraticLerp(a, b, c, t);
        Vector3 bd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(ac, bd, t);
    }


}