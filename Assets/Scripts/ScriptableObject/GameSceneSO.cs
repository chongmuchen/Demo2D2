using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "GameSceneSO", menuName = "Game Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;
    public AssetReference sceneReference;
}