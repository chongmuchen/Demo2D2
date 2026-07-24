using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")] public SceneLoadEventSO loadEventSO;
    public GameSceneSO firstLoadScene;

    [SerializeReference] private GameSceneSO _currentLoadScene;
    private GameSceneSO _sceneToGo;
    private Vector3 _positionToGo;
    private bool _fadeScene;
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
        _sceneToGo = scene;
        _positionToGo = posToGo;
        _fadeScene = fadeScene;
        Debug.Log($"OnLoadEventSO: {_sceneToGo.sceneReference.SubObjectName}");
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
        _sceneToGo.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
}