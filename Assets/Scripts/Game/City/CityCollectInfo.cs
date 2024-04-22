using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WOFL.UI
{
    [RequireComponent(typeof(Button))]
    public class CityCollectInfo : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private CityScreen _cityScreen;
        private Button _button;

        [Header("Variables")]
        private bool _isActive;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _cityScreen.OnTimeToCollectUpdate += UpdateInfoText;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }
        private void UpdateInfoText(int value)
        {
            _infoText.text = $"Automatic collection after {value} seconds";
        }
        private void Click()
        {
            _isActive = !_isActive;
            _infoText.gameObject.SetActive(_isActive);
        }

        #endregion
    }
}