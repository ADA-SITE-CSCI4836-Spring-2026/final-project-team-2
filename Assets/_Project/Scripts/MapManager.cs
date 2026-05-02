using UnityEngine;
using UnityEngine.UI; // Needed to interact with Button components
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Header("Map Nodes (Assign in Inspector)")]
    public Button[] layer1Nodes;
    public Button[] layer2Nodes;
    public Button[] layer3Nodes;

    void Start()
    {
        // 1. Standard UI Setup
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameManager.Instance != null)
        {
            // 2. Preserve Logic: Freeze the timer while we are on the map
            GameManager.Instance.isTimerRunning = false;

            // 3. New Logic: Update which nodes the player can click
            UpdateMapUnlocks(GameManager.Instance.currentLayer);
        }
    }

    private void UpdateMapUnlocks(int currentLayer)
    {
        // Lock EVERYTHING first to ensure a clean state
        SetButtonsState(layer1Nodes, false);
        SetButtonsState(layer2Nodes, false);
        SetButtonsState(layer3Nodes, false);

        // Unlock ONLY the layer the player is currently on
        if (currentLayer == 1) SetButtonsState(layer1Nodes, true);
        else if (currentLayer == 2) SetButtonsState(layer2Nodes, true);
        else if (currentLayer == 3) SetButtonsState(layer3Nodes, true);
        else 
        {
            Debug.Log("YOU BEAT THE GAME!"); 
            // Victory screen logic can be added here
        }
    }

    private void SetButtonsState(Button[] buttons, bool isUnlocked)
    {
        foreach (Button btn in buttons)
        {
            if (btn != null)
            {
                btn.interactable = isUnlocked; // Unity's way to grey out/disable a button
            }
        }
    }

    // Replaces OnNodeClicked to handle different scenes for different nodes
    public void LoadSpecificLevel(int sceneToLoad)
    {
        Debug.Log("Loading Gameplay Level: " + sceneToLoad);
        
        // Preserve Logic: Start the timer right before we jump into the level
        if (GameManager.Instance != null)
        {
            GameManager.Instance.isTimerRunning = true;
        }
        
        SceneManager.LoadScene(sceneToLoad);
    }
}