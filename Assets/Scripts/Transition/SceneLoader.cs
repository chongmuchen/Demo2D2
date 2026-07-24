using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour
{
    public Transform _playerTransform;
    [Header("事件监听")] public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    [SerializeReference] private GameSceneSO _currentLoadScene;
    private GameSceneSO _sceneToGo;
    private Vector3 _positionToGo;
    private bool _fadeScene;
    private bool isLoading;
    public float fadeDuration;


    private void Awake()
    {
        _currentLoadScene = firstLoadScene;
        firstLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnDisable()
    {
        loadEventSO.loadRequestEvent -= OnLoadEventSO;
    }


    private void OnEnable()
    {
        loadEventSO.loadRequestEvent += OnLoadEventSO;
    }


    private void OnLoadEventSO(GameSceneSO scene, Vector3 posToGo, bool fadeScene)
    {
        if (isLoading)
        {
            return;
        }

        _playerTransform.gameObject.SetActive(false);
        isLoading = true;
        _sceneToGo = scene;
        _positionToGo = posToGo;
        _fadeScene = fadeScene;
        StartCoroutine(UnloadPreviousScene());
    }

    private IEnumerator UnloadPreviousScene()
    {
        if (_fadeScene)
        {
        }

        yield return new WaitForSeconds(fadeDuration);
        if (_currentLoadScene != null)
        {
            yield return _currentLoadScene.sceneReference.UnLoadScene();
        }

        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadOption = _sceneToGo.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadOption.Completed += OnLoadedCompleted;
    }

    private void OnLoadedCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        _currentLoadScene = _sceneToGo;
        if (_fadeScene)
        {
        }
        _playerTransform.position = _positionToGo;
        _playerTransform.gameObject.SetActive(true);
        isLoading = false;
    }
}