using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        level.AddCheckPoint();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        level.RemoveCheckPoint();
    }


}
