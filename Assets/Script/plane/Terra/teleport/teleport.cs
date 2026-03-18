using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Cinemachine;  // 需要引用Cinemachine

public class PlatformTeleporter : MonoBehaviour
{
    [Header("目标平台")]
    [SerializeField] private Transform targetPlatform;
    [Header("新区域边界")]
    [SerializeField] private Collider2D newCameraBound;  // 新区域的摄像机边界
    private CinemachineConfiner cameraConfiner;  // 相机边界组件
    private CinemachineVirtualCamera virtualCamera;

    private FadeIn fadeInScript;  // 引用你的FadeIn脚本

    private void Start()
    {
        // 找到挂载FadeIn脚本的function空物体
        fadeInScript = FindObjectOfType<FadeIn>();

        // 获取相机上的边界组件
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cameraConfiner = virtualCamera.GetComponent<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportSequence(other.gameObject));
        }
    }

    private IEnumerator TeleportSequence(GameObject player)
    {
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
}