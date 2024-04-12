using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WOFL.BattlePass
{
    public class BattlePassLevelPoint : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _valueText;

        [Header("Settings")]
        [SerializeField] private Color32 _offColor;
        [SerializeField] private Color32 _onColor;

        #endregion

        #region Control Methods

        public void Initialize(int value, bool isOn)
        {
            _valueText.text = value.ToString();
            SwitchPointColor(isOn);
        }
        public void SwitchPointColor(bool isOn) => _background.color = isOn ? _onColor : _offColor;

        #endregion
    }
}