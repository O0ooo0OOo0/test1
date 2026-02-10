using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameFramework.Samples.Localization
{
    public class LanguageType : MonoBehaviour
    {
        [SerializeField]
        private List<string> languageTypes = new List<string> {"CN", "EN"};
        [SerializeField]
        private List<string> languageDisplays = new List<string>() {"简体中文", "English"};
        [SerializeField]
        private TMP_Dropdown languageDp;

        private void Start()
        {
            languageDp.AddOptions(languageDisplays);
            languageDp.onValueChanged.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage(int index)
        {
            LocalizationManager.Instance.ChangeLanguage(languageTypes[index]);
        }
    }
}