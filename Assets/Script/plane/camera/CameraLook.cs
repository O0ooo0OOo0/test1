using UnityEngine;
using Cinemachine;

public class CameraLook : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float pos = 0.4f;
    private CinemachineFramingTransposer transposer;
    private float originalScreenY;

    void Start()
    {
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (transposer != null)
            originalScreenY = transposer.m_ScreenY;
        else
            Debug.LogError("未找到 FramingTransposer 组件，请检查Virtual Camera的Body类型");
    }

    void Update()
    {
        if (transposer == null) return;

        if (Input.GetKey(KeyCode.DownArrow))
            transposer.m_ScreenY = pos;  // 往下看
        else
            transposer.m_ScreenY = originalScreenY;  // 恢复正常
    }
}