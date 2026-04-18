using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public static string nextScene = "Map";
    
    public GameObject progressBar;
    public Text textPercent;
    private float fixloadingTime = 2f;

    private void Start()
    {
        StartCoroutine(loadSceneASync(nextScene));
    }
    
    public IEnumerator loadSceneASync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // delay giả để thanh chạy mượt (fix loading time)
            timer += Time.deltaTime / fixloadingTime;
            float displayProgress = Mathf.Min(progress, timer);

            progressBar.GetComponent<Image>().fillAmount = displayProgress;
            textPercent.text = (displayProgress * 100).ToString("0") + "%";

            // Khi load xong (>=0.9) và thanh đã đầy
            if (displayProgress >= 1f)
            {
                yield return new WaitForSeconds(0.3f); // nghỉ chút cho đẹp
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
