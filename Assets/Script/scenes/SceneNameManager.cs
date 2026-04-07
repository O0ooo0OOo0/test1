using GameFramework.Samples.Localization;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static SceneNameManager;

public class SceneNameManager : MonoBehaviour
{
    public static SceneNameManager snm;

    [System.Serializable]
    public class SceneNames 
    {
        public string nameCN, nameEN, nameMap;
        public Sprite image;
        public List<int> sceneIndexs;
    }

    public List<SceneNames> sceneNames;
    private Dictionary<int, (string, Sprite)> sceneIndexToName;   // 建立对应关系字典（场景序列号→场景地图名称+场景图片）
    public string sceneName;
    public Sprite sceneImage;

    private void Awake()
    {
        if (snm == null)
        {
            snm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sceneName = null;
        sceneImage = null;
        sceneIndexToName = new Dictionary<int, (string, Sprite)>();   // 初始化字典
        CurrentLanType(LanguageType.lt.lanType);
    }

    // 当前语言环境
    public void CurrentLanType(int lan)
    {
        if (lan == 0)   // 中文
        {
            foreach (var scene in sceneNames)
            {
                scene.nameMap = scene.nameCN;
            }
        }
        else if (lan == 1)   // 英文
        {
            foreach (var scene in sceneNames)
            {
                scene.nameMap = scene.nameEN;
            }
        }

        // 根据语言环境刷新字典信息
        sceneIndexToName.Clear();
        BuildCache();
    }

    // 建立对应关系
    public void BuildCache()
    {
        foreach (var scene in sceneNames)
        {
            foreach (var index in scene.sceneIndexs)
            {
                sceneIndexToName[index] = (scene.nameMap, scene.image);
            }
        }
    }

    // 根据场景序列号得出对应场景名称（地图）
    public void GetSceneMapName(int index)
    {
        (sceneName, sceneImage) = sceneIndexToName[index];
    }
}
