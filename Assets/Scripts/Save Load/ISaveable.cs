using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISaveable
{
    DataDefination GetDataID();

    void RegisterEvent()
    {
        DataManager.instance.RegisterSaveable(this);
    }

    void UnregisterEvent()
    {
        DataManager.instance.UnRegisterSaveable(this);
    }

    void GetSaveData(Data data);
    void LoadData(Data data);
}