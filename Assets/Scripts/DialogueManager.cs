// DialogueManager.cs --- 一個專門負責對話的管理器
using UnityEngine;
using TMPro; // 記得引用 TextMeshPro

public class DialogueManager : MonoBehaviour
{
    [Header("對話 UI 元件")]
    [SerializeField] private GameObject dialoguePanel; // 整個對話框的父物件
    [SerializeField] private TextMeshProUGUI dialogueText; // 顯示文字的元件

    private PlayerMovement playerMovement; // 儲存玩家移動腳本的引用

    void Start()
    {
        // 確保對話框一開始是關閉的
        dialoguePanel.SetActive(false);
        // 尋找玩家的移動腳本，為將來的「暫停移動」做準備
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
    }

    // 這是給其他腳本 (例如 Interactable.cs) 呼叫的公開函式
    public void ShowDialogue(string message)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = message;

        // (可選，但推薦) 顯示對話時，禁止玩家移動
        if (playerMovement != null)
        {
            playerMovement.DisableMovement();
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);

        // (可選，但推薦) 關閉對話時，恢復玩家移動
        if (playerMovement != null)
        {
            playerMovement.EnableMovement();
        }
    }
}