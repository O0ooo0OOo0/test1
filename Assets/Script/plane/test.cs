using UnityEngine;

public class ImpulseOnKeyPress : MonoBehaviour
{
    // 引用 Impulse Source 组件
    private Cinemachine.CinemachineImpulseSource impulseSource;

    // 可选：在 Inspector 中调整震动的强度
    [Header("震动参数")]
    [SerializeField] private float amplitudeMultiplier = 1f;
    [SerializeField] private float frequencyMultiplier = 1f;

    void Start()
    {
        // 获取当前物体上的 Impulse Source 组件
        impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();

        // 如果没有找到组件，给出警告
        if (impulseSource == null)
        {
            Debug.LogWarning("请先在当前物体上添加 Cinemachine Impulse Source 组件！");
        }
    }

    void Update()
    {
        // 检测是否按下 A 键
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TriggerImpulse();
        }
    }

    void TriggerImpulse()
    {
        if (impulseSource != null)
        {
            // 方法1：使用默认参数触发震动
            impulseSource.GenerateImpulse();

            // 方法2：自定义震动强度和频率（可选，注释掉的方法）
            // impulseSource.GenerateImpulseWithVelocity(Random.insideUnitSphere * amplitudeMultiplier);

            Debug.Log("触发了相机震动！");
        }
    }
}