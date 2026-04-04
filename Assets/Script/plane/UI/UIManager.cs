using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("面板引用")]
    public GameObject cardSelectionPanel;  // 拖入CardSelectionPanel对象

    private CardSelectionPanel cardPanelScript;
    private bool isCardPanelOpen = false;

    void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // 获取脚本引用
        if (cardSelectionPanel != null)
        {
            cardPanelScript = cardSelectionPanel.GetComponent<CardSelectionPanel>();
            cardSelectionPanel.SetActive(false);  // 初始关闭
        }
    }

    void Update()
    {
        // 按Q打开/关闭选卡面板
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isCardPanelOpen)
                CloseCardPanel();
            else
                OpenCardPanel();
        }

        // 按ESC关闭面板（如果开着）
        if (Input.GetKeyDown(KeyCode.Escape) && isCardPanelOpen)
        {
            CloseCardPanel();
        }
    }

    public void OpenCardPanel()
    {
        if (cardSelectionPanel == null) return;

        isCardPanelOpen = true;
        cardSelectionPanel.SetActive(true);

        // 如果面板有OpenPanel方法，调用它
        if (cardPanelScript != null)
        {
            // 使用反射或添加公开方法
            cardPanelScript.OpenPanel();
        }

        Time.timeScale = 0f;  // 暂停游戏
        Debug.Log("选卡面板已打开");
    }

    public void CloseCardPanel()
    {
        if (cardSelectionPanel == null) return;

        isCardPanelOpen = false;

        // 如果面板有ClosePanel方法，调用它
        if (cardPanelScript != null)
        {
            cardPanelScript.ClosePanel();
        }

        cardSelectionPanel.SetActive(false);
        Time.timeScale = 1f;  // 恢复游戏
        Debug.Log("选卡面板已关闭");
    }

    public bool IsCardPanelOpen()
    {
        return isCardPanelOpen;
    }
}