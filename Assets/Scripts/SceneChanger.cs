// SceneChanger.cs --- 升級版
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // 定義一個列舉 (Enum) 來表示不同的切換模式
    public enum ChangeMode
    {
        DirectToScene,      // 直接跳轉到指定的場景
        GoToSelectedPlanet  // 根據 GameState 前往選擇的星球
    }

    [Header("切換設定")]
    [SerializeField] private ChangeMode mode = ChangeMode.DirectToScene;

    [Header("直接跳轉模式設定")]
    [Tooltip("在 DirectToScene 模式下，要載入的場景名稱")]
    [SerializeField] private string sceneToLoadByName;

    [Header("前往星球模式設定")]
    [Tooltip("請按照 Build Settings 中的順序，填入每個星球登陸區的場景索引")]
    [SerializeField] private int[] planetSceneIndexes;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (mode == ChangeMode.DirectToScene)
            {
                // 執行舊的、直接的跳轉邏輯
                LoadSceneByName(sceneToLoadByName);
            }
            else if (mode == ChangeMode.GoToSelectedPlanet)
            {
                // 執行新的、根據 GameState 載入場景的邏輯
                LoadSelectedPlanetScene();
            }
        }
    }

    private void LoadSceneByName(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void LoadSelectedPlanetScene()
    {
        if (GameStateManager.Instance == null)
        {
            Debug.LogError("GameStateManager 不存在！無法確定目標星球。");
            return;
        }

        int targetIndex = GameStateManager.Instance.TargetPlanetIndex;

        // 檢查索引是否有效
        if (targetIndex >= 0 && targetIndex < planetSceneIndexes.Length)
        {
            int sceneBuildIndex = planetSceneIndexes[targetIndex];
            Debug.Log("準備根據 GameState 前往星球，載入場景索引: " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex);
        }
        else
        {
            Debug.LogWarning("目標星球索引無效或尚未選擇！目標索引為: " + targetIndex);
            // 在這裡可以選擇載入一個預設的「太空」場景，或者不執行任何操作
        }
    }
}