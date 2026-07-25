using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private List<ISaveable> saveables = new List<ISaveable>();
    private Data saveData;
    [Header("事件监听")] public VoidEventSO saveDataEvent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        saveData = new Data();
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadData();
        }
    }

    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += SaveData;
    }


    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= SaveData;
    }

    private void SaveData()
    {
        foreach (ISaveable saveable in saveables)
        {
            saveable.GetSaveData(saveData);
        }

        foreach (var item in saveData.characterPosDict)
        {
            Debug.Log($"SaveData {item.Key}:{item.Value}");
        }
    }


    private void LoadData()
    {
        foreach (ISaveable saveable in saveables)
        {
            saveable.LoadData(saveData);
        }
    }

    public void RegisterSaveable(ISaveable saveable)
    {
        if (!saveables.Contains(saveable))
        {
            saveables.Add(saveable);
        }
    }

    public void UnRegisterSaveable(ISaveable saveable)
    {
        saveables.Remove(saveable);
    }
}