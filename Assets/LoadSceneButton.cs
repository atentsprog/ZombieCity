using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName;
    void Start()
    {
        transform.AddOrGetComponent<Button>().AddListener(this, LoadScene);
    }

    public LoadingUI loadingUI;
    private void LoadScene()
    {
        var progrerss = SceneManager.LoadSceneAsync(sceneName);
        loadingUI.StartLoadUI(progrerss);
    }
}
