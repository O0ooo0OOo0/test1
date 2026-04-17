using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    [Header("UI组件")]
    [SerializeField] private GameObject dialogPanel;      // 对话面板
    [SerializeField] private TextMeshProUGUI npcNameText; // NPC名字文本
    [SerializeField] private TextMeshProUGUI dialogText;  // 对话内容文本

    [SerializeField] private GameObject choicePanel;      // 选项面板
    [SerializeField] private Button choiceButtonPrefab;   // 选项按钮预制体

    [Header("对话设置")]
    [SerializeField] private TextAsset dialogJsonFile;    // JSON文件
    [SerializeField] private float typingSpeed = 0.05f;   // 打字速度
    [SerializeField] private float dialogCooldownTime = 1f; // 对话结束后冷却时间
    private bool canStartNewDialog = true;


    [Header("玩家禁用")]
    [SerializeField] private GameObject player;


    // 存储所有对话的字典
    private Dictionary<string, DialogData> dialogDictionary;

    // 当前对话状态
    private bool isDialogActive = false;
    private DialogData currentDialog;
    private int currentSentenceIndex;
    private Coroutine typingCoroutine;


    // 公开属性，让其他脚本可以访问对话状态
    public bool IsDialogActive => isDialogActive;

    private void Awake()
    {
        // 单例模式实现
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切换场景时不销毁
            LoadDialogs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 初始状态：启用玩家
        SetPlayerControl(true);

        // 初始时隐藏所有面板
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
        if (choicePanel != null)
            choicePanel.SetActive(false);
    }

    // 加载JSON对话文件
    private void LoadDialogs()
    {
        if (dialogJsonFile != null)
        {
            try
            {
                // 解析JSON
                var dialogList = JsonConvert.DeserializeObject<DialogList>(dialogJsonFile.text);
                dialogDictionary = new Dictionary<string, DialogData>();

                foreach (var dialog in dialogList.dialogs)
                {
                    dialogDictionary[dialog.id] = dialog;

                    // 打印每个加载的对话ID和内容预览
                    Debug.Log($"加载对话: {dialog.id} - {dialog.npcName} - 句子数: {dialog.sentences?.Count ?? 0}");

                    if (dialog.sentences != null && dialog.sentences.Count > 0)
                    {
                        Debug.Log($"  第一句: {dialog.sentences[0].text}");
                    }
                }

                Debug.Log($"成功加载 {dialogDictionary.Count} 个对话");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"加载对话失败: {e.Message}");
            }
        }
        else
        {
            Debug.LogError("未指定对话JSON文件");
        }
    }

    // 开始对话
    public void StartDialog(string dialogId)
    {
        if (!canStartNewDialog) return;

        if (dialogDictionary.ContainsKey(dialogId))
        {
            // 对话开始时禁用玩家控制
            SetPlayerControl(false);
            currentDialog = dialogDictionary[dialogId];
            currentSentenceIndex = 0;
            isDialogActive = true;

 


            Debug.Log($"开始对话: {dialogId}");
            Debug.Log($"dialogPanel 是否为null: {dialogPanel == null}");
            if (dialogPanel != null)
            {
                Debug.Log($"dialogPanel 当前状态: {dialogPanel.activeSelf}");
                dialogPanel.SetActive(true);
                Debug.Log($"dialogPanel 设置后状态: {dialogPanel.activeSelf}");
            }
            else
            {
                Debug.LogError("dialogPanel 为 null！请在Inspector中赋值");
            }
            Debug.Log($"npcNameText 是否为null: {npcNameText == null}");
            if (npcNameText != null)
            {
                npcNameText.text = currentDialog.npcName;
                Debug.Log($"NPC名字设置为: {currentDialog.npcName}");
            }
            // 显示对话面板
            npcNameText.text = currentDialog.npcName;

            // 显示第一句
            ShowCurrentSentence();
        }
        else
        {
            Debug.LogError($"找不到对话ID: {dialogId}");
        }
    }

    // 显示当前句子
    private void ShowCurrentSentence()
    {
        // 如果是选项对话
        if (currentDialog.isChoice)
        {
            Debug.Log("是选项对话，调用 ShowChoicePanel");
            ShowChoicePanel();
            return;
        }

        // 普通对话
        if (currentSentenceIndex < currentDialog.sentences.Count)
        {
            var sentence = currentDialog.sentences[currentSentenceIndex];

            Debug.Log($"显示第 {currentSentenceIndex} 句: '{sentence.text}'");

            // 打字效果
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeText(sentence.text));

            // 自动下一句
            if (sentence.autoNext)
            {
                StartCoroutine(AutoNextSentence());
            }
        }
        else
        {
            Debug.Log("currentSentenceIndex 超出范围，结束对话");
            EndDialog();
        }
    }

    // 打字效果协程
    private IEnumerator TypeText(string text)
    {
        dialogText.text = "";
        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // 显示选项面板
    private void ShowChoicePanel()
    {
        choicePanel.SetActive(true);

        // 清除旧的选项按钮
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        // 获取或添加VerticalLayoutGroup
        VerticalLayoutGroup layoutGroup = choicePanel.GetComponent<VerticalLayoutGroup>();
        // 关键：设置反向排列（从下往上）
        layoutGroup.reverseArrangement = true;  // 实现了从下往上

        // 创建新的选项按钮
        for (int i = 0; i < currentDialog.choices.Count; i++)
        {
            int index = i; // 闭包问题修复
            var button = Instantiate(choiceButtonPrefab, choicePanel.transform);
            // 设置按钮文本
            //TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = currentDialog.choices[i].text;

            // 添加点击事件
            button.onClick.AddListener(() =>
            {
                MakeChoice(index);
            });
        }
    }

    // 处理选项选择
    private void MakeChoice(int choiceIndex)
    {
        var choice = currentDialog.choices[choiceIndex];
        string nextDialogId = choice.nextId;

        // 隐藏选项面板
        choicePanel.SetActive(false);

        if (!string.IsNullOrEmpty(nextDialogId))
        {
            StartDialog(nextDialogId);
        }
        else
        {
            EndDialog();
        }
    }

    // 自动下一句
    private IEnumerator AutoNextSentence()
    {
        yield return new WaitForSeconds(2f);
        NextSentence();
    }

    // 下一句
    public void NextSentence()
    {
        // 如果是选项对话，直接返回
        if (currentDialog.isChoice) return;

        // 如果当前句子正在打字，不执行下一句 
        if (dialogText.text != currentDialog.sentences[currentSentenceIndex].text) return;

        if (!isDialogActive || 
            currentDialog == null || 
            currentDialog.sentences == null ||
            currentDialog.sentences.Count == 0) 
            return;

        // 获取当前句子
        var currentSentence = currentDialog.sentences[currentSentenceIndex];

        // 检查是否是最后一句
        bool isLastSentence = currentSentenceIndex >= currentDialog.sentences.Count - 1;

        // 1. 优先检查是否有 nextId 跳转 且 是最后一句
        if (isLastSentence)
        {
            if (!string.IsNullOrEmpty(currentSentence.nextId))
            {
                // 直接跳转到指定的对话ID
                StartDialog(currentSentence.nextId);
            }
            else
            {
                // 是最后一句，结束对话
                EndDialog();
            }
            return;
        }

        // 3. 正常显示下一句
        currentSentenceIndex++;
        ShowCurrentSentence();

    }

    // 结束对话
    private void EndDialog()
    {
        isDialogActive = false;
        dialogPanel.SetActive(false);
        choicePanel.SetActive(false);

        // 对话结束时启用玩家控制
        SetPlayerControl(true);

        Debug.Log("对话结束");

        // 冷却：1秒后才能重新对话
        canStartNewDialog = false;
        StartCoroutine(CooldownCoroutine());
    }
    // 冷却：1秒后才能重新对话
    private System.Collections.IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(dialogCooldownTime);
        canStartNewDialog = true;
    }

    private void Update()
    {
        // 按F键继续对话
        if (isDialogActive && (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)))
        {
            NextSentence();
        }
    }

    // 控制玩家脚本的启用/禁用
    private void SetPlayerControl(bool enable)
    {
        if (player != null)
        {
            // 禁用所有脚本
            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                if (script is DialogManager || script is NPCInteraction)
                    continue;
                script.enabled = enable;
            }

            // 设置动画为Idle
            Animator anim = player.GetComponent<Animator>();
            if (anim != null && !enable)
            {
                anim.SetFloat("Speed", 0);
                anim.Play("Idle"); // 强制播放空闲动画
            }

            // 停止物理移动
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = !enable; // 对话时设置为kinematic，防止物理影响
            }
        }


    }
}


    // ---------- 数据类 ----------
    [System.Serializable]
    public class DialogList
    {
        public List<DialogData> dialogs;
    }

    [System.Serializable]
    public class DialogData
    {
        public string id;
        public string npcName;
        public List<Sentence> sentences;
        public bool isChoice;
        public List<Choice> choices;
    }

    [System.Serializable]
    public class Sentence
    {
        public string text;
        public bool autoNext;
        public string nextId;
    }

    [System.Serializable]
    public class Choice
    {
        public string text;
        public string nextId;
    }
