using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Cinemachine;  // 需要引用Cinemachine

public class PlatformTeleporterF : MonoBehaviour
{
    [Header("目标平台")]
    [SerializeField] private Transform targetPlatform;
    [Header("新区域边界")]
    [SerializeField] private Collider2D newCameraBound;  // 新区域的摄像机边界
    private CinemachineConfiner cameraConfiner;  // 相机边界组件
    private CinemachineVirtualCamera virtualCamera;

    private FadeIn fadeInScript;  // 引用你的FadeIn脚本

    // 新增变量
    private bool playerInRange = false;  // 玩家是否在触发范围内
    private GameObject player;  // 引用玩家对象

    private void Start()
    {
        // 找到挂载FadeIn脚本的function空物体
        fadeInScript = FindObjectOfType<FadeIn>();

        // 获取相机上的边界组件
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cameraConfiner = virtualCamera.GetComponent<CinemachineConfiner>();
    }

    private void Update()
    {
        // 检测玩家是否在范围内并且按下F键
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(TeleportSequence());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject;  // 保存玩家引用
            // 可选：显示提示UI
            ShowInteractPrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;  // 清除玩家引用
            // 可选：隐藏提示UI
            ShowInteractPrompt(false);
        }
    }

    private IEnumerator TeleportSequence()
    {
        // 防止在传送过程中再次触发
        playerInRange = false;

        // 手动实现淡出效果
        GameObject fadeUI = fadeInScript.fadeIn;
        Image fadeImage = fadeUI.GetComponent<Image>();
        float fadeSpeed = fadeInScript.fadeInSpeed;

        // 淡出（透明 -> 黑）
        fadeUI.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 传送
        player.transform.position = targetPlatform.position;

        // ===== 更新相机边界 =====
        if (cameraConfiner != null && newCameraBound != null)
        {
            cameraConfiner.m_BoundingShape2D = newCameraBound;
            cameraConfiner.InvalidatePathCache(); // 刷新边界缓存
        }

        // 淡入（黑 -> 透明）
        alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeUI.SetActive(false);
    }

    // 可选：显示/隐藏交互提示的方法
    private void ShowInteractPrompt(bool show)
    {
        // 这里可以添加显示"按F传送"的UI提示
        // 例如：promptUI.SetActive(show);
        Debug.Log(show ? "显示提示：按F传送" : "隐藏提示");
    }
}