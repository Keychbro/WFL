using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kamen.UI;
using System;

namespace WOFL.UI
{
    [RequireComponent(typeof(Button))]
    public class CityLevelUpButton : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _levelUpText;
        [SerializeField] private TextMeshProUGUI _maxLevelText;
        [Space]
        [SerializeField] private GameObject _levelToLevelHolder;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _nextLevelText;

        [Header("Settings")]
        [SerializeField] private string _popupName;

        [Header("Variables")]
        private Button _button;

        #endregion

        #region Properties

        public event Action OnLevelUpButtonClicked;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        #endregion

        #region Control Methods

        private void Click()
        {
            PopupManager.Instance.Show(_popupName);
        }
        public void UpdateButtonView(int currentLevel, bool isLast)
        {
            if (isLast)
            {
                _levelUpText.gameObject.SetActive(false);
                _maxLevelText.gameObject.SetActive(true);
                _levelToLevelHolder.SetActive(false);
            }
            else
            {
                _levelUpText.gameObject.SetActive(true);
                _maxLevelText.gameObject.SetActive(false);
                _levelToLevelHolder.SetActive(true);

                _currentLevelText.text = currentLevel.ToString();
                _nextLevelText.text = (currentLevel + 1).ToString();
            }
        }

        #endregion
    }
}

