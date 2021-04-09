using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{

    GameStatus gameStatus;
    LifeStatus lifeStatus;
    int numberOfScenes;

    private void Start()
    {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;
        gameStatus = FindObjectOfType<GameStatus>();
        lifeStatus = FindObjectOfType<LifeStatus>();
    }

    public void LoadStartingScene(float delayOnSceneLoad)
    {
        gameStatus.ResetScore();
        lifeStatus.ResetLifeForNewLevel();
        StartCoroutine(WaitForSceneLoad(0, delayOnSceneLoad));
    }

    public void LoadNextScene(float delayOnSceneLoad)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        if (currentSceneIndex + 1 < numberOfScenes)
        {
            StartCoroutine(WaitForSceneLoad(currentSceneIndex + 1, delayOnSceneLoad));
        }
        else
        {
            gameStatus.ResetScore();
            LoadStartingScene(delayOnSceneLoad + 1f);
        }
    }

    public void ReloadScene(float delayOnSceneLoad)
    {
        StartCoroutine(WaitForSceneLoad(SceneManager.GetActiveScene().buildIndex, delayOnSceneLoad));
    }

    private IEnumerator WaitForSceneLoad(int sceneIndex, float delayOnSceneLoad)
    {
        yield return new WaitForSeconds(delayOnSceneLoad);
        SceneManager.LoadScene(sceneIndex);
    }


}
