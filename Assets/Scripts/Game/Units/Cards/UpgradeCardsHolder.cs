using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Settings;
using WOFL.Game;
using WOFL.Save;
using System;

namespace WOFL.UI
{
    public class UpgradeCardsHolder : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private UnitCardForUpgrade _unitCardPrefab;

        [Header("Objects")]
        [SerializeField] private RectTransform _cardsHolder;
        [SerializeField] private ScrollRect _cardScroll;

        [Header("Variables")]
        private UnitInfo[] _unitsInfo;
        private List<UnitCardForUpgrade> _unitCards = new List<UnitCardForUpgrade>();

        #endregion

        #region Properties

        public bool IsInitialized { get; private set; }

        #endregion

        #region Control Methods

        public void Initialize(UnitInfo[] units)
        {
            _unitsInfo = units;

            for (int i = 0; i < _unitsInfo.Length; i++)
            {
                CreateUnitCard(_unitsInfo[i]);
            }

            if (_cardsHolder.sizeDelta.x < _cardScroll.GetComponent<RectTransform>().sizeDelta.x) _cardScroll.vertical = false;

            IsInitialized = true;
        }
        private void CreateUnitCard(UnitInfo unitInfo)
        {
            UnitCardForUpgrade unitCard = Instantiate(_unitCardPrefab, _cardsHolder.transform);
            unitCard.Initialize(unitInfo);

            _unitCards.Add(unitCard);
        }
        public void SubscribeOnCardsMoreButton(Action<UnitDataForSave, UnitLevelsHolder, Skin> callback)
        {
            for (int i = 0; i < _unitCards.Count; i++)
            {
                _unitCards[i].OnUpgradeButtonClicked += callback;
            }
        }

        #endregion
    }
}