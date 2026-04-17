using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineFollowSetter : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        SetFollowToPlayer();
    }

    void SetFollowToPlayer()
    {
        // 检查 PlayerManager 是否存在
        if (PlayerManager.pm == null)
        {
            return;
        }

        // 检查 PlayerManager.pm.gameObject 是否存在
        if (PlayerManager.pm.gameObject == null)
        {
            return;
        }

        // 设置 Follow 目标
        vcam.m_Follow = PlayerManager.pm.gameObject.transform;
    }
}