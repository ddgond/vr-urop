using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTeleporter : MonoBehaviour {
    public string targetSceneName;
    public TextMeshPro textComponent;

    private bool loading = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !loading)
        {
            print("loading scene " + targetSceneName);
            loading = true;
            textComponent.text = "Loading new scene...";
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
