using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneLoadEventSO", menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> loadRequestEvent;

    public void RaiseLoadRequestEvent(GameSceneSO sceneToLoad, Vector3 screenPos, bool fadeLoad)
    {
        loadRequestEvent?.Invoke(sceneToLoad, screenPos, fadeLoad);
    }
}