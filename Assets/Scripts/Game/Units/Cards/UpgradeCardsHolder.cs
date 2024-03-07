using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Settings;
using WOFL.Game;

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

        #region Control Methods

        public void Initialize(UnitInfo[] units)
        {
            _unitsInfo = units;

            for (int i = 0; i < _unitsInfo.Length; i++)
            {
                CreateUnitCard(_unitsInfo[i]);
            }

            if (_cardsHolder.sizeDelta.x < _cardScroll.GetComponent<RectTransform>().sizeDelta.x) _cardScroll.vertical = false;
        }
        private void CreateUnitCard(UnitInfo unitInfo)
        {
            UnitCardForUpgrade unitCard = Instantiate(_unitCardPrefab, _cardsHolder.transform);
            unitCard.Initialize(unitInfo);

            _unitCards.Add(unitCard);
        }

        #endregion
    }
}