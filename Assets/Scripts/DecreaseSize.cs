using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseSize : MonoBehaviour
{
    [Range(0.1f, 0.9f)] [SerializeField] float scaleMultiplier;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        level.DecreaseSize(scaleMultiplier);
        Destroy(gameObject);
    }

}
