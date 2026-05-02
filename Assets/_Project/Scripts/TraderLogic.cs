using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems; // Needed to check for the EventSystem

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
        // 1. CRITICAL SAFETY CHECK: UI will be dead without an Event System!
        if (FindObjectOfType<EventSystem>() == null)
        {
            Debug.LogError("CRITICAL: There is no EventSystem in your scene! UI Buttons will not click. Right click Hierarchy -> UI -> Event System.");
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        activeUI = Instantiate(traderUIPrefab);

        // --- EXIT BUTTON ---
        Transform exitTransform = activeUI.transform.Find("Background_Panel/Exit_Button");
        if (exitTransform != null) exitTransform.GetComponent<Button>().onClick.AddListener(CloseTraderMenu);
        else Debug.LogError("Could not find Exit_Button! Check prefab spelling.");

        // --- UPGRADE 1 ---
        Transform upg1 = activeUI.transform.Find("Background_Panel/Upgrade_Button_1");
        if (upg1 != null)
        {
            if (GameManager.Instance.hasBoughtUpgrade1) upg1.gameObject.SetActive(false);
            else upg1.GetComponent<Button>().onClick.AddListener(BuyUpgrade1);
        }
        else Debug.LogError("Could not find Upgrade_Button_1!");

        // --- UPGRADE 2 ---
        Transform upg2 = activeUI.transform.Find("Background_Panel/Upgrade_Button_2");
        if (upg2 != null)
        {
            if (GameManager.Instance.hasBoughtUpgrade2) upg2.gameObject.SetActive(false);
            else upg2.GetComponent<Button>().onClick.AddListener(BuyUpgrade2);
        }

        // --- UPGRADE 3 ---
        Transform upg3 = activeUI.transform.Find("Background_Panel/Upgrade_Button_3");
        if (upg3 != null)
        {
            if (GameManager.Instance.hasBoughtUpgrade3) upg3.gameObject.SetActive(false);
            else upg3.GetComponent<Button>().onClick.AddListener(BuyUpgrade3);
        }
    }

    public void CloseTraderMenu()
    {
        if (activeUI != null) Destroy(activeUI);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    // --- PURCHASE LOGIC ---

    public void BuyUpgrade1()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentTimer >= 20f)
        {
            GameManager.Instance.UpdateTimer(-20f);
            GameManager.Instance.bonusTimePerKill += 5f;
            GameManager.Instance.hasBoughtUpgrade1 = true;
            CloseTraderMenu(); 
        }
        else Debug.Log("Not enough time to buy!");
    }

    public void BuyUpgrade2()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentTimer >= 30f)
        {
            GameManager.Instance.UpdateTimer(-30f);
            GameManager.Instance.timerDrainRate = 0.7f; 
            GameManager.Instance.hasBoughtUpgrade2 = true;
            CloseTraderMenu(); 
        }
        else Debug.Log("Not enough time to buy!");
    }

    public void BuyUpgrade3()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentTimer >= 40f)
        {
            GameManager.Instance.UpdateTimer(-40f);
            GameManager.Instance.hasBoughtUpgrade3 = true;
            CloseTraderMenu(); 
        }
        else Debug.Log("Not enough time to buy!");
    }
}