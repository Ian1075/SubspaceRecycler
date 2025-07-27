// DialogueManager.cs --- 全新升級版
using UnityEngine;
using TMPro;
using System.Collections.Generic; // 引用這個命名空間來使用 Queue (隊列)

public class DialogueManager : MonoBehaviour
{
    [Header("對話 UI 元件")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Queue<string> sentences; // 一個用來存放所有待顯示句子的「隊列」
    private bool isDialogueActive = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
    }

    // --- 新的對話啟動函式 ---
    public void StartDialogue(string[] dialogueLines)
    {
        // 如果正在對話中，就不要啟動新的對話
        if (isDialogueActive) return;

        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        if (playerMovement != null) playerMovement.DisableMovement();

        sentences.Clear(); // 清空之前的對話

        // 將所有對話句子依序放入隊列中
        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        // 開始顯示第一句話
        DisplayNextSentence();
    }

    // --- 顯示下一句話的核心邏輯 ---
    public void DisplayNextSentence()
    {
        // 如果隊列中已經沒有句子了，就結束對話
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // 從隊列中取出第一句話
        string sentenceToDisplay = sentences.Dequeue();
        // 顯示在畫面上
        dialogueText.text = sentenceToDisplay;
    }

    // --- 結束對話的函式 ---
    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        if (playerMovement != null) playerMovement.EnableMovement();
        Debug.Log("對話結束。");
    }

    // 供外部查詢狀態的函式
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}