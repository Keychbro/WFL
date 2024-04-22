using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WOFL.UI
{
    public class NotificationPoint : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _textAmount;

        #endregion

        #region Control Methods

        public void UpdateTextAmount(int value)
        {
            _textAmount.text = value < 100 ? value.ToString() : "99+";
            CheckActive(value);
        }
        private void CheckActive(int value)
        {
            if (value > 0) gameObject.SetActive(true);
            else gameObject.SetActive(false);
        } 

        #endregion
    }
}