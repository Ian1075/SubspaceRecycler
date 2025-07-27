// UIManager.cs
using UnityEngine;
using UnityEngine.UI; // 記得要引用 UI 命名空間

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Health playerHealth; // 我們需要引用玩家的血量腳本

    void Start()
    {
        // 初始化血條的最大值
        // 這段程式碼假設玩家一開始就存在場上
        if (playerHealth != null)
        {
            playerHealthSlider.maxValue = playerHealth.GetMaxHealth();
            playerHealthSlider.value = playerHealth.GetMaxHealth();
        }
    }

    void Update()
    {
        // 每幀更新血條的當前值
        if (playerHealth != null)
        {
            playerHealthSlider.value = playerHealth.GetCurrentHealth();
        }
    }
}