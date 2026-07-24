using UnityEngine;

public class Chest : MonoBehaviour, IIteractable
{
    public void TriggerAction()
    {
        Debug.Log("Open Chest");
    }
}