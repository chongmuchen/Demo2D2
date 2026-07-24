using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")] public CharacterEventSO healthEvent;
    public SceneLoadEventSO loadEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        loadEvent.loadRequestEvent += OnSceneLoad;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        loadEvent.loadRequestEvent -= OnSceneLoad;
    }

    private void OnSceneLoad(GameSceneSO scene, Vector3 arg1, bool arg2)
    {
        if (scene.sceneType == SceneType.Menu)
        {
            playerStatBar.gameObject.SetActive(false);
        }
    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(percentage);
        playerStatBar.OnPowerChange(character);
    }
}