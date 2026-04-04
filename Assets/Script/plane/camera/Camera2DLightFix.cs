using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class Camera2DLightFix : MonoBehaviour
{
    private Camera _camera;
    private UniversalAdditionalCameraData _cameraData;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraData = _camera.GetUniversalAdditionalCameraData();

        // 核心：告诉URP，这个相机需要自己的光照纹理
        // 这会让渲染器为这个Overlay相机单独生成一张光照纹理，而不是尝试去复用主相机的
        if (_cameraData != null)
        {
            _cameraData.requiresDepthTexture = true;
            _cameraData.requiresColorTexture = true;
  
        }
    }

    // 可选：如果Start里不生效，可以尝试在OnEnable里强制请求
    void OnEnable()
    {
        if (_cameraData != null)
        {
            _cameraData.requiresDepthTexture = true;
            _cameraData.requiresColorTexture = true;
        }
    }
}