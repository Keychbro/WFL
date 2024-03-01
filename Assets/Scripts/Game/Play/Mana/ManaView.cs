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
        [SerializeField] private Castle _playerCastle;
        [Space]
        [SerializeField] private TextMeshProUGUI _currentManaValue;
        [SerializeField] private Slider _manaFiller;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        private void OnDestroy()
        {
            _playerCastle.OnManaFilled -= UpdateFillerView;
            _playerCastle.OnManaValueChanged -= UpdateManaAmountValue;
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _playerCastle.OnManaFilled += UpdateFillerView;
            _playerCastle.OnManaValueChanged += UpdateManaAmountValue;

            _manaFiller.value = 0;

            UpdateManaAmountValue();
            UpdateFillerView(0);
        }
        private void UpdateFillerView(float value) => _manaFiller.value = value;
        private void UpdateManaAmountValue() => _currentManaValue.text = BigNumberViewConverter.Instance.Convert(_playerCastle.Mana);

        #endregion
    }
}