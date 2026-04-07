using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework.Samples.Localization
{
    public class LanguageType : MonoBehaviour
    {
        public static LanguageType lt;

        [SerializeField]
        private List<string> languageTypes;     // 语言标识符列表
        [SerializeField]
        private List<string> languageDisplays;     // 下拉菜单显示文本

        public TMP_Dropdown languageDp;     // 下拉菜单组件引用
        public int lanType;  // 记录当前语言类型

        // 调用其他脚本
        public ForgingDialogManager fdm;
        public List<InputMaterial> inputMaterials;
        public List<ForgingProducts> forgingProducts;
        public SceneNameManager sceneNameManager;

        private void Awake()
        {
            if (lt == null)
            {   
                lt = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else if (lt != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (lt == this)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }

        // 当场景加载时启用
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            UpdateDropdownState(scene.name);
        }

        private void Start()
        {
            lanType = 0;
            languageDp.AddOptions(languageDisplays);     // 将显示文本添加到下拉菜单选项
            languageDp.onValueChanged.AddListener(ChangeLanguage);      // 监听下拉菜单值变化事件，切换语言时调用 ChangeLanguage

            // 初始化时根据当前场景设置状态
            UpdateDropdownState(SceneManager.GetActiveScene().name);
        }

        // 根据场景名更新下拉菜单状态
        private void UpdateDropdownState(string sceneName)
        {
            bool isMainScene = sceneName == "main";
            languageDp.enabled = isMainScene;   // 直接启用或禁用整个下拉菜单组件
            languageDp.interactable = isMainScene;   // 同时控制交互，防止已禁用时还能点击
        }

        // 根据选中索引获取对应语言代码，通知本地化管理器切换
        private void ChangeLanguage(int index)
        {
            LocalizationManager.Instance.ChangeLanguage(languageTypes[index]);
            lanType = index;
            SpecialTextChange();
        }

        // 特殊文本切换
        public void SpecialTextChange()
        {
            if (fdm != null)
            {
                fdm.DefineTextContent(lanType);
            }

            if (inputMaterials != null)
            {
                foreach (var inputMat in inputMaterials)
                {
                    inputMat.MatName(lanType);
                    inputMat.CurrentMat();
                }
            }

            if (forgingProducts != null)
            {
                foreach (var forgingPdt in forgingProducts)
                {
                    forgingPdt.PdtName(lanType);
                }
            }

            if (sceneNameManager != null)
            {
                sceneNameManager.CurrentLanType(lanType);
            }
        }
    }
}