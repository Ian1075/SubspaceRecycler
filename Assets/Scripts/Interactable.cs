// Interactable.cs
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // [TextArea] 讓文字欄位在 Inspector 中變大，方便輸入多行文字
    [SerializeField, TextArea(3, 5)] 
    private string[] dialogueLines; 
    private DialogueManager dialogueManager;
    private bool isInRange = false; // 玩家是否在互動範圍內
    private bool isDialogueActive = false;

    void Start()
    {
        // 尋找場景中的 DialogueManager
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        // 確保 dialogueManager 存在，並且當前沒有正在進行的對話
        if (dialogueManager != null && isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueManager.IsDialogueActive())
            {
                dialogueManager.StartDialogue(dialogueLines);
            }
            else
            {
                dialogueManager.DisplayNextSentence();
            }
        }
    }

    // 當有物件進入觸發器時
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("玩家進入 " + gameObject.name + " 的互動範圍");
            // 在這裡可以顯示一個 "按 E 互動" 的提示圖示
        }
    }

    // 當有物件離開觸發器時
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("玩家離開 " + gameObject.name + " 的互動範圍");
            // 在這裡可以隱藏互動提示圖示
        }
    }
}