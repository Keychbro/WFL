using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WOFL.Control;

namespace WOFL.UI
{
    public class ProductPanel : MonoBehaviour
    {
        #region Variables

        [Header("Product Objects")]
        [SerializeField] protected Image _background;
        [SerializeField] protected TextMeshProUGUI _title;
        [SerializeField] protected Image _productIcon;
        [Space]
        [SerializeField] protected KamenButton _button;
        [SerializeField] protected LoadingIcon _loadingIcon;
        [SerializeField] protected Image _priceIcon;
        [SerializeField] protected TextMeshProUGUI _priceText;

        //[Header("Variables")]

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _button.OnClick().RemoveListener(ClickOnButton);
        }

        #endregion

        #region Control Methods

        public void Initialize()
        {
            _button.OnClick().AddListener(ClickOnButton);
        }
        private void ClickOnButton()
        {

        }

        #endregion
    }
}