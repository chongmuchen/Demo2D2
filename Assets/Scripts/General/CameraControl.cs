using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D _confiner;
    public CinemachineImpulseSource _impulseSource;
    public VoidEventSO voidEventSO;
    public VoidEventSO afterSceneLoadEventEO;

    private void Awake()
    {
        _confiner = GetComponent<CinemachineConfiner2D>();
    }

    // private void Start()
    // {
    //     GetNewCameraBounds();
    // }

    private void OnEnable()
    {
        voidEventSO.OnEventRaised += OnCanemaShakeEvent;
        afterSceneLoadEventEO.OnEventRaised += OnAfterSceneLoaded;
    }


    private void OnDisable()
    {
        voidEventSO.OnEventRaised -= OnCanemaShakeEvent;
        afterSceneLoadEventEO.OnEventRaised -= OnAfterSceneLoaded;
    }

    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
        {
            return;
        }

        _confiner.BoundingShape2D = obj.GetComponent<Collider2D>();
        _confiner.InvalidateBoundingShapeCache();
    }

    private void OnCanemaShakeEvent()
    {
        _impulseSource.GenerateImpulse();
    }


    private void OnAfterSceneLoaded()
    {
        GetNewCameraBounds();
    }
}