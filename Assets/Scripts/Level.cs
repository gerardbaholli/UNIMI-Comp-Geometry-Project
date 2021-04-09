using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject ship;
    [Range(1f, 20f)] [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] int placeableSplinePoints;
    [SerializeField] float countdown;


    LifeStatus lifeStatus;
    GameStatus gameStatus;
    GameObject shipInstance;
    Vector3[] splinePointsPath = null;
    bool hasCountdownStarted = false;
    bool isSplineCompleted = false;
    bool hasCountdownFinished = false;
    float secondsRemaining;
    int checkPoints;
    int indexOnSpline = 0;

    public int PlaceableSplinePoints { get => placeableSplinePoints; set => placeableSplinePoints = value; }

    private void Start()
    {
        lifeStatus = FindObjectOfType<LifeStatus>();
        gameStatus = FindObjectOfType<GameStatus>();
        pointsText.text = "Points: " + PlaceableSplinePoints.ToString();
        resultText.text = "";
    }

    private void FixedUpdate()
    {
        MoveSpaceShipOnSpline();
        CountdownTimer();
    }


    /* 
     * Moves the ship along the spline.
     */
    private void MoveSpaceShipOnSpline()
    {
        if (isSplineCompleted && !hasCountdownFinished && indexOnSpline < splinePointsPath.Length)
        {
            var targetPosition = splinePointsPath[indexOnSpline];
            var movementThisFrame = speed * Time.deltaTime;

            shipInstance.transform.position = Vector2.MoveTowards
                (shipInstance.transform.position, targetPosition, movementThisFrame);

            if (shipInstance.transform.position == targetPosition)
                indexOnSpline++;
        }
    }

    public void StartCountdownTimer()
    {
        hasCountdownStarted = true;
    }

    /* 
     * Instantiate ship and starts the game.
     */
    public void StartShip()
    {
        if (hasCountdownStarted)
        {
            GetPositionsFromSpline();
            shipInstance = Instantiate(ship, splinePointsPath[0], Quaternion.identity);
            DestroySpline();
            isSplineCompleted = true;
        }
    }

    /* 
     * Gets spline positions and puts them in the array.
     */
    private void GetPositionsFromSpline()
    {
        splinePointsPath = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(splinePointsPath);
    }

    /* 
     * Remove the spline from the scene.
     */
    private void DestroySpline()
    {
        Point[] splinePoints = FindObjectsOfType<Point>();
        Spline generatePoint = FindObjectOfType<Spline>();

        for (int i = 0; i < splinePoints.Length; i++)
            Destroy(splinePoints[i].gameObject);

        Destroy(generatePoint.gameObject);
    }

    

    /* 
     * Prints countdown timer on game canvas.
     */
    private void CountdownTimer()
    {
        if (hasCountdownStarted)
        {
            if (!hasCountdownFinished)
            {
                if (countdown > 1)
                {
                    countdown -= Time.deltaTime;
                    secondsRemaining = Mathf.FloorToInt(countdown);
                    timerText.text = secondsRemaining.ToString();
                }
                else
                {
                    ResetLevel();
                }
            }
        }

    }

    public void ResetLevel()
    {
        if (hasCountdownStarted && !hasCountdownFinished)
        {
            RemoveLife();
        }
    }

    private void RemoveLife()
    {
        if (lifeStatus.Life > 1)
        {
            lifeStatus.Life--;
            lifeStatus.LifeText.text = "Life: " + lifeStatus.Life.ToString();
            resultText.text = lifeStatus.Life.ToString() + "\nattempts left";
            hasCountdownFinished = true;
            Destroy(shipInstance);
            FindObjectOfType<SceneLoader>().ReloadScene(1.5f);
        }
        else
        {
            resultText.text = "Game Over\n" + "Final score - " + gameStatus.Score;
            hasCountdownFinished = true;
            Destroy(shipInstance);
            lifeStatus.ResetLifeForNewLevel();
            gameStatus.ResetScore();
            FindObjectOfType<SceneLoader>().LoadStartingScene(5f);
        }
    }




    /* 
     * Called onStart by CheckPoint.cs, add checkPoint, need it to 
     * count checkPoints on the begging of the level.
     */
    public void AddCheckPoint()
    {
        checkPoints++;
    }


    /* 
     * Called by CheckPoint.cs, remove it from total count and 
     * check if they are all collected (win condition).
     */
    public void RemoveCheckPoint()
    {
        checkPoints--;

        if (checkPoints <= 0)
        {
            hasCountdownFinished = true;
            lifeStatus.ResetLifeForNewLevel();
            NextLevel();
        }
    }


    private void NextLevel()
    {
        gameStatus.Score += Mathf.FloorToInt(secondsRemaining);
        resultText.text = "Success\nYour score - " + gameStatus.Score;
        FindObjectOfType<SceneLoader>().LoadNextScene(2f);
    }

    public void RemovePlacebleSplinePoint()
    {
        PlaceableSplinePoints--;
        pointsText.text = "Points: " + PlaceableSplinePoints.ToString();
    }


    /* 
     * Called by BonusTime.cs, gives value seconds.
     */
    public void AddTime(int value)
    {
        countdown += value;
    }


    /* 
     * Called by MalusTime.cs, removes value seconds.
     */
    public void RemoveTime(int value)
    {
        countdown -= value;
    }

    /* 
     * Called by BonusSpeed.cs, add speed to the ship.
     */
    public void AddSpeed(float value)
    {
        speed += value;
    }

    /* 
     * Called by BonusSpeed.cs, remove speed to the ship.
     */
    public void RemoveSpeed(float value)
    {
        speed -= value;
    }

    /* 
     * Called by IncreaseSpeed.cs, scale the ship and make it bigger.
     */
    public void IncreaseSize(float value)
    {
        shipInstance.transform.localScale *= value;
    }

    /* 
     * Called by DecreaseSpeed.cs, scale the ship and make it smaller.
     */
    public void DecreaseSize(float value)
    {
        shipInstance.transform.localScale *= value;
    }



}
