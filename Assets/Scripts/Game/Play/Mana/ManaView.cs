using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Game;
using WOFL.Control;

namespace WOFL.UI
{
    public class ManaView : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _currentManaValue;
        [SerializeField] private Slider _manaFiller;
        private Castle _castle;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _castle.OnManaFilled -= UpdateFillerView;
            _castle.OnManaValueChanged -= UpdateManaAmountValue;
        }

        #endregion

        #region Control Methods

        public void Initialize(Castle castle)
        {
            _castle = castle;

            _castle.OnManaFilled += UpdateFillerView;
            _castle.OnManaValueChanged += UpdateManaAmountValue;

            _manaFiller.value = 0;

            UpdateManaAmountValue();
            UpdateFillerView(0);
        }
        private void UpdateFillerView(float value) => _manaFiller.value = value;
        private void UpdateManaAmountValue() => _currentManaValue.text = BigNumberViewConverter.Instance.Convert(_castle.CurrentMana);

        #endregion
    }
}