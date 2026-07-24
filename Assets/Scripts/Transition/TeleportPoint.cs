using System;
using UnityEngine;

public class TeleportPoint : MonoBehaviour, IIteractable
{
    public SceneLoadEventSO loadEventSO;

    public GameSceneSO sceneToGo;
    public Vector3 positionToGo;


    public void TriggerAction()
    {
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);
    }
}