using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeStatus : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] int life;

    public TextMeshProUGUI LifeText { get => lifeText; set => lifeText = value; }
    public int Life { get => life; set => life = value; }

    private void Awake()
    {
        int liveStatusCounter = FindObjectsOfType<LifeStatus>().Length;

        if (liveStatusCounter > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        LifeText.text = "Life: " + Life.ToString();
    }


    public void ResetLifeForNewLevel()
    {
        Destroy(gameObject);
    }




}
