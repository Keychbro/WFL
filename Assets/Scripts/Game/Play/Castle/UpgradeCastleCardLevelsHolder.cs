using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Upgrade Castle Card Levels Holder", menuName = "WOFL/Castle/Upgrade Castle Card Levels Holder", order = 1)]
    public class UpgradeCastleCardLevelsHolder : ScriptableObject
    {
        #region Enums

        public enum UpgradeCastleCardType
        {
            CastleHealth,
            ExtraGold,
            ManaRegeneration
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private string _name;
        [SerializeField] private UpgradeCastleCardType _type;
        [SerializeField] private UpgradeCastleCardLevel[] _cardLevels;

        #endregion

        #region Properties

        public string Name { get => _name; }
        public UpgradeCastleCardType Type { get => _type; }
        public UpgradeCastleCardLevel[] CardLeveles { get => _cardLevels; }

        #endregion
    }
}