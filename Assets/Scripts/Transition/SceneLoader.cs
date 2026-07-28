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
    public static SceneLoader Instance { get; private set; }

    public Transform _playerTransform;
    [Header("事件监听")] public SceneLoadEventSO loadEventSO;

    [Header("广播")] public VoidEventSO afterSceneLoadEventEO;
    public SceneLoadEventSO sceneUnloadEventSO;

    [Header("场景")] public GameSceneSO firstLoadScene;
    public Vector3 firstLoadPosition;
    public GameSceneSO menuScene;
    public Vector3 menuPosition;
    [Header("加载配置")] public float fadeDuration;


    private GameSceneSO _currentLoadScene;
    private GameSceneSO _sceneToGo;
    private Vector3 _positionToGo;
    private bool _fadeScene;
    private bool isLoading;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        loadEventSO.RaiseLoadRequestEvent(menuScene, menuPosition, true);
    }

    private void OnDisable()
    {
        loadEventSO.loadRequestEvent -= OnLoadEventSO;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void OnEnable()
    {
        loadEventSO.loadRequestEvent += OnLoadEventSO;
    }


    public void StartNewGame()
    {
        loadEventSO.RaiseLoadRequestEvent(firstLoadScene, firstLoadPosition, true);
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
        if (_currentLoadScene != null)
        {
            if (_fadeScene)
            {
                FadeCanvas.Instance.FadeIn(fadeDuration);
            }

            yield return new WaitForSeconds(fadeDuration);
            yield return _currentLoadScene.sceneReference.UnLoadScene();
        }

        sceneUnloadEventSO.RaiseLoadRequestEvent(_sceneToGo, _positionToGo, true);
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
            FadeCanvas.Instance.FadeOut(fadeDuration);
        }

        _playerTransform.position = _positionToGo;
        _playerTransform.gameObject.SetActive(true);
        isLoading = false;
        if (_currentLoadScene.sceneType != SceneType.Menu)
        {
            afterSceneLoadEventEO.RaiseEvent();
        }
    }
}
