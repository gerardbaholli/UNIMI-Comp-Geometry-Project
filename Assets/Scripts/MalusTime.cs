using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusTime : MonoBehaviour
{
    [SerializeField] int removeTimeInSeconds;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        level.RemoveTime(removeTimeInSeconds);
        Destroy(gameObject);
    }

}
