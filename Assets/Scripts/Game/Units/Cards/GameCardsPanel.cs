using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WOFL.Game;
using WOFL.Settings;

namespace WOFL.UI
{
    public class GameCardsPanel : MonoBehaviour
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private UnitCardInGame _unitCardPrefab;
        [SerializeField] private GameObject _cardsHolder;

        [Header("Variables")]
        private Castle _currentCastle;
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
        }
        private void CreateUnitCard(UnitInfo unitInfo)
        {
            UnitCardInGame unitCard = Instantiate(_unitCardPrefab, _cardsHolder.transform);
            unitCard.Adjust(unitInfo);
            unitCard.OnClick.AddListener(() => CallSpawn(unitInfo.UniqueName));

            _unitCards.Add(unitCard);
        }
        private void CallSpawn(string name)
        {
            _currentCastle.CreateUnitForMana(name);
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