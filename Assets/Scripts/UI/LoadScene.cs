using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        LoadSceneManager.Instance.LoadSceneWithFade(sceneName);
    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        LoadSceneManager.Instance.LoadSceneWithFade(SceneManager.GetActiveScene().name);
    }
}