using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D _confiner;
    public CinemachineImpulseSource _impulseSource;
    public VoidEventSO voidEventSO;

    private void Awake()
    {
        _confiner = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        GetNewCameraBounds();
    }

    private void OnEnable()
    {
        voidEventSO.OnEventRaised += OnCanemaShakeEvent;
    }


    private void OnDisable()
    {
        voidEventSO.OnEventRaised -= OnCanemaShakeEvent;
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
}