using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SceneLoadEventSO", menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;

    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}