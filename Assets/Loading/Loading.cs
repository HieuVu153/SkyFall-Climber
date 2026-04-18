using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public static string nextScene = "PlayScene";
    
    public GameObject progressBar;
    public Text textPercent;
    private float fixloadingTime = 3f;

    private void Start()
    {
        StartCoroutine(loadSceneASync(nextScene));
    }
    
    public IEnumerator loadSceneASync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.GetComponent<Image>().fillAmount = progress;
            textPercent.text = (progress * 100).ToString("0") + "%";
            
            yield return null;
        }
    }
}
