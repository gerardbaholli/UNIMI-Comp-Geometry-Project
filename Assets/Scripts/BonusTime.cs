using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTime : MonoBehaviour
{
    [SerializeField] int timeInSeconds;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        level.AddTime(timeInSeconds);
        Destroy(gameObject);
    }

}
