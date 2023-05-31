using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Slider _loadingBarFill;
    [SerializeField] TextMeshProUGUI _valueProgress;

    public static LoadingScene Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    public void LoadScene(int sceneToLoad)
    {
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(int sceneToLoad)
    {
        _loadingScreen.SetActive(true);

        _loadingBarFill.value = 0;
        _valueProgress.text = "0%";

        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);


        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            _loadingBarFill.value = progressValue;
            _valueProgress.text = progressValue * 100f + "%";

            yield return null;
        }
    }
}
