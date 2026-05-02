using UnityEngine;
using UnityEngine.UI; // This tells Unity to use Legacy UI

public class HUDController : MonoBehaviour
{
    public Text timeTextDisplay; // Using standard Text now

    void Update()
    {
        if (GameManager.Instance != null)
        {
            // Rounds the time to the nearest whole number and displays it
            float timeToShow = GameManager.Instance.currentTimer;
            timeTextDisplay.text = Mathf.CeilToInt(timeToShow).ToString();
        }
    }
}