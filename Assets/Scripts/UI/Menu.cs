using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject newGameButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

    public void NewGame()
    {
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("Cannot start a new game: SceneLoader is missing.");
            return;
        }

        SceneLoader.Instance.StartNewGame();
    }
}
