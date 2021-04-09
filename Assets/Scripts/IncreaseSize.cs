using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour
{
    [Range(1.1f, 2f)][SerializeField] float scaleMultiplier;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        level.IncreaseSize(scaleMultiplier);
        Destroy(gameObject);
    }

}
