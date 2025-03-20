using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen; 
    [SerializeField] private Slider _progressBar; 

    public static SceneLoader Instance;

    private void Awake()
    {
        _loadingScreen.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNewScene(int sceneIndex, System.Action onSceneLoaded = null)
    {
        _loadingScreen.SetActive(true); // Show loading screen

        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false; // Prevent auto-activation

        StartCoroutine(TrackLoadingProgress(asyncLoad, onSceneLoaded));
    }

    private IEnumerator TrackLoadingProgress(AsyncOperation asyncLoad, System.Action onSceneLoaded)
    {
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            // Update the progress bar (asyncLoad.progress goes from 0 to 0.9 before finishing)
            _progressBar.value = Mathf.Lerp(_progressBar.value, asyncLoad.progress, Time.deltaTime * 5); // Smooth transition

            // When loading is 90% done, we can activate the scene (asyncLoad.progress is 0.9)
            if (asyncLoad.progress >= 0.9f)
            {
                _progressBar.value = 1f; // Set the progress bar to full
                asyncLoad.allowSceneActivation = true; // Allow the scene to activate
            }

            yield return null;
        }

        // Ensure the final progress bar value is set to 100% (1.0)
        _progressBar.value = 1f;


        // Wait a short moment before activating to prevent visual pop-in
        yield return new WaitForSeconds(0.2f);

        asyncLoad.allowSceneActivation = true; // Now activate the scene

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Hide loading screen after the scene is active
        _loadingScreen.SetActive(false);

        // Call the callback function AFTER the scene is fully active
        onSceneLoaded?.Invoke();
    }
}
