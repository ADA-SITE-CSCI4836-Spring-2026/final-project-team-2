using UnityEngine;
using UnityEngine.UI; // Needed to click Buttons

public class TraderLogic : MonoBehaviour
{
    [Header("UI Setup")]
    public GameObject traderUIPrefab; 
    private GameObject activeUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && activeUI == null)
        {
            OpenTraderMenu();
        }
    }

    private void OpenTraderMenu()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        activeUI = Instantiate(traderUIPrefab);

        // Hook up the Exit Button
        Transform exitTransform = activeUI.transform.Find("Background_Panel/Exit_Button");
        if (exitTransform != null)
        {
            Button exitBtn = exitTransform.GetComponent<Button>();
            exitBtn.onClick.AddListener(CloseTraderMenu);
        }

        // Hook up the Upgrade Button
        Transform upgradeTransform = activeUI.transform.Find("Background_Panel/Upgrade_Button_1");
        if (upgradeTransform != null)
        {
            Button upgrade1Btn = upgradeTransform.GetComponent<Button>();
            upgrade1Btn.onClick.AddListener(BuyTimeOnKillUpgrade);
        }
    }

    public void CloseTraderMenu()
    {
        if (activeUI != null)
        {
            Destroy(activeUI);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    // THIS IS THE NEW PURCHASE LOGIC
    public void BuyTimeOnKillUpgrade()
    {
        // 1. Check if the player has at least 20 seconds to spend
        if (GameManager.Instance != null && GameManager.Instance.currentTimer >= 20f)
        {
            // 2. Deduct the time!
            GameManager.Instance.UpdateTimer(-20f);

            GameManager.Instance.timeOnKillBonus += 5f;

            Debug.Log("SUCCESS: You bought +5s Time on Kill!");
            
            // 3. Close the menu automatically after buying
            CloseTraderMenu(); 
        }
        else
        {
            Debug.Log("FAILED: Not enough time or GameManager missing!");
        }
    }
}