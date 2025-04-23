using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public GameObject loadingImage;

    public void StartGameEvent()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(0.5f); //wait for 0.5 seconds
        SceneManager.LoadScene(1);
    }
}
