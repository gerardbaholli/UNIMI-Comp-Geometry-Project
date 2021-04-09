using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeed : MonoBehaviour
{
    [Range(1f, 5f)] [SerializeField] float bonusSpeed;

    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        level.AddSpeed(bonusSpeed);
        Destroy(gameObject);
    }

}
