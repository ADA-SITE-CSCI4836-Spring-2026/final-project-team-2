using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject controlsPanel;

    private void Start()
    {
        controlsPanel.SetActive(false);
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnControls()
    {
        controlsPanel.SetActive(true);
    }

    public void OnCloseControls()
    {
        controlsPanel.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void Update()
    {
        if (controlsPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            OnCloseControls();
    }
}