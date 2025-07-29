// StarMapManager.cs
using UnityEngine;
using UnityEngine.UI; // 為了使用 Button

public class StarMapManager : MonoBehaviour
{
    [SerializeField] private GameObject starMapPanel; // 星際地圖的 Panel
    [SerializeField] private Transform playerShipIcon; // 飛船圖示的 Transform
    [SerializeField] private Button[] planetButtons; // 所有的星球按鈕

    private Transform currentPlanet; // 飛船目前所在的星球按鈕的 Transform

    void Start()
    {
        starMapPanel.SetActive(false); // 確保地圖一開始是關閉的

        // 找到初始星球 (這裡我們先手動指定第一個按鈕為初始位置)
        if (planetButtons.Length > 0)
        {
            currentPlanet = planetButtons[0].transform;
            UpdateShipPosition();
        }

        // 為每一個按鈕添加點擊事件監聽
        for (int i = 0; i < planetButtons.Length; i++)
        {
            int planetIndex = i; // 關鍵：建立一個臨時變數來儲存索引
            planetButtons[i].onClick.AddListener(() => GoToPlanet(planetIndex));
        }
    }

    // --- 公開的函式，給 Interactable 的事件呼叫 ---
    public void OpenStarMap()
    {
        starMapPanel.SetActive(true);
        // 在這裡可以暫停玩家移動
    }

    public void CloseStarMap()
    {
        starMapPanel.SetActive(false);
        // 在這裡可以恢復玩家移動
    }

    // --- 按鈕點擊後執行的函式 ---
    void GoToPlanet(int planetIndex)
    {
        Debug.Log("準備前往星球 " + planetIndex);
        
        Transform targetPlanet = planetButtons[planetIndex].transform;
        currentPlanet = targetPlanet;
        UpdateShipPosition();

        // --- 核心修改在這裡 ---
        // 透過單例模式的靜態實例，直接存取 GameStateManager 並設定目標星球索引
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.TargetPlanetIndex = planetIndex;
            Debug.Log("遊戲狀態已更新：目標星球索引為 " + GameStateManager.Instance.TargetPlanetIndex);
        }
            // 關閉地圖，返回飛船場景
            CloseStarMap();
        }

    void UpdateShipPosition()
    {
        if (playerShipIcon != null && currentPlanet != null)
        {
            // 讓飛船圖示的位置等於當前星球按鈕的位置
            playerShipIcon.position = currentPlanet.position;
        }
    }
}