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
        private UnitInfo[] _units;
        private List<UnitCardInGame> _unitCards = new List<UnitCardInGame>();

        #endregion

        #region Control Methods

        public void Initialize(Castle castle, UnitInfo[] units)
        {
            _currentCastle = castle;
            _units = units;

            for (int i = 0; i < _units.Length; i++)
            {
                CreateUnitCard(_units[i]);
            }
            _currentCastle.OnManaValueChanged += UpdateCards;

            if (_cardsHolder.sizeDelta.x < _cardScroll.GetComponent<RectTransform>().sizeDelta.x) _cardScroll.horizontal = false;
        }
        private void CreateUnitCard(UnitInfo unitInfo)
        {
            UnitCardInGame unitCard = Instantiate(_unitCardPrefab, _cardsHolder.transform);
            unitCard.Adjust(unitInfo);
            string uniqueName = unitInfo.UniqueName;
            unitCard.OnClick.AddListener(() => CallSpawn(uniqueName));
            unitCard.UpdateCardView(false);

            _unitCards.Add(unitCard);
        }
        private void CallSpawn(string uniqueName)
        {
            _currentCastle.CreateUnitForMana(uniqueName);
        }
        private void UpdateCards()
        {
            for (int i = 0; i < _unitCards.Count; i++)
            {
                _unitCards[i].UpdateCardView(_currentCastle.CurrentMana >= _unitCards[i].CardManaPrice);
            }
        }

        #endregion
    }
}