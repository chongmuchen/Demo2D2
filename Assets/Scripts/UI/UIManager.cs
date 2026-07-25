using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")] public CharacterEventSO healthEvent;
    public SceneLoadEventSO loadEvent;
    public VoidEventSO loadDataEvent;
    public VoidEventSO gameOverEvent;

    [Header("组件")] public GameObject gameOverPanel;
    public GameObject restartBtn;

    private void Awake()
    {
        OnLoadEvent();
    }

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        loadEvent.loadRequestEvent += OnSceneLoad;
        loadDataEvent.OnEventRaised += OnLoadEvent;
        gameOverEvent.OnEventRaised += OnGameOverEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        loadEvent.loadRequestEvent -= OnSceneLoad;
        loadDataEvent.OnEventRaised -= OnLoadEvent;
        gameOverEvent.OnEventRaised -= OnGameOverEvent;
    }


    private void OnLoadEvent()
    {
        gameOverPanel.SetActive(false);
    }


    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartBtn);
    }


    private void OnSceneLoad(GameSceneSO scene, Vector3 arg1, bool arg2)
    {
        var isActive = scene.sceneType != SceneType.Menu;
        playerStatBar.gameObject.SetActive(isActive);
    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(percentage);
        playerStatBar.OnPowerChange(character);
    }
}