using Kamen.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.UI
{
    [RequireComponent(typeof(Button))]
    public class PopupVisiableSwitchButton : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private bool _isEnable;
        [SerializeField] private string _popupName;

        [Header("Variables")]
        private Button _button;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Click);
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }
        private void Click()
        {
            if (_isEnable) PopupManager.Instance.Show(_popupName);
            else PopupManager.Instance.Hide(_popupName);
        }

        #endregion
    }
}