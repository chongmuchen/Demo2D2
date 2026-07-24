using UnityEngine;

public class TeleportPoint : MonoBehaviour, IIteractable
{
    public Vector3 positionToGo;
    public void TriggerAction()
    {
        Debug.Log("Teleport");
    }
}