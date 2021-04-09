using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusSpeed : MonoBehaviour
{
    [Range(1f, 5f)] [SerializeField] float malusSpeed;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        level.RemoveSpeed(malusSpeed);
        Destroy(gameObject);
    }

}
