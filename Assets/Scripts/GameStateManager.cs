// GameStateManager.cs
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // ------------------- 單例模式的核心 -------------------
    
    // 1. 一個公開的、靜態的變數，用來儲存 GameStateManager 的唯一實例
    public static GameStateManager Instance { get; private set; }

    // Awake() 是在所有物件的 Start() 之前，在物件被載入時就執行的函式
    private void Awake()
    {
        // 2. 檢查 Instance 是否已經存在
        if (Instance == null)
        {
            // 如果不存在，就把這個物件的實例賦值給它
            Instance = this;
            // 3. 告訴 Unity，當載入新場景時，不要銷毀這個掛載著本腳本的 GameObject
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 4. 如果 Instance 已經存在（例如，從前一個場景切換回來時，場景裡又多了一個 GameStateManager）
            //    就把這個重複的物件銷毀掉，從而保證永遠只有一個實例存在
            Destroy(gameObject);
        }
    }
    
    // ------------------- 遊戲狀態數據 -------------------
    
    // 在這裡，我們定義所有需要跨場景儲存的數據
    // public 讓我們能從其他腳本中輕鬆讀取和修改它們

    // 範例 1: 玩家選擇的目標星球索引
    public int TargetPlanetIndex { get; set; } = -1; // -1 代表尚未選擇

    // 範例 2: 玩家的名字
    //public string PlayerName { get; set; } = "執行官";

    // 範例 3: 玩家的貨幣
    //public int PlayerCredits { get; set; } = 100;

    // 你未來可以加入更多狀態...
    // public List<string> UnlockedSkills { get; private set; } = new List<string>();
}