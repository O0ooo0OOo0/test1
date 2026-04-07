using UnityEngine;
using UnityEngine.EventSystems;

public class InputBlocker : MonoBehaviour
{
    public static InputBlocker ib;

    private EventSystem cachedEventSystem;   // 缓存引用，避免频繁访问 EventSystem.current

    private void Awake()
    {
        if (ib == null)
        {
            ib = this;
            DontDestroyOnLoad(gameObject);
            
            cachedEventSystem = EventSystem.current;   // 初始化时获取 EventSystem
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // 如果缓存的 EventSystem 失效，尝试重新获取
        if (cachedEventSystem == null)
        {
            cachedEventSystem = EventSystem.current;
        }
    }

    // 禁用所有 UI 和键盘输入
    public void DisableAllInput()
    {
        EnsureEventSystem();
        if (cachedEventSystem != null)
        {
            cachedEventSystem.enabled = false;
        }
    }

    // 恢复输入
    public void EnableAllInput()
    {
        EnsureEventSystem();
        if (cachedEventSystem != null)
        {
            cachedEventSystem.enabled = true;
        }
    }

    // 确保 EventSystem 存在
    private void EnsureEventSystem()
    {
        if (cachedEventSystem == null)
        {
            cachedEventSystem = EventSystem.current;

            // 如果仍然没有，尝试创建一个
            if (cachedEventSystem == null)
            {
                GameObject esObj = new GameObject("EventSystem");
                cachedEventSystem = esObj.AddComponent<EventSystem>();
                esObj.AddComponent<StandaloneInputModule>();
            }
        }
    }
}