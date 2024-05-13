using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;

namespace WOFL.Save
{
    [Serializable] public class UnitDataForSave
    {
        #region Variables

        [Header("Main Settings")]
        [SerializeField] private UnitInfo.UniqueUnitName _uniqueName;

        [Header("Level Settings")]
        [SerializeField] private int _currentLevel;
        [SerializeField] private int _amountCards;
        public event Action OnCurrentLevelChanged;
        public event Action OnAmountCardsChanged;

        [Header("Skin Settings")]
        [SerializeField] private List<string> _obtainedSkinsNames;
        [SerializeField] private string _currentSkinName;


        #endregion

        #region Properties

        public UnitInfo.UniqueUnitName UniqueName { get => _uniqueName; }

        public int CurrentLevel { get => _currentLevel; }
        public int AmountCards 
        { 
            get => _amountCards;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[UnitDataForSave] - Attempt to assign variable ''_amountCards'' minus value");
                    return;
                }
                _amountCards = value;
                OnAmountCardsChanged?.Invoke();
            }
        }

        public List<string> ObtainedSkinsNames { get => _obtainedSkinsNames; }
        public string CurrentSkinName 
        { 
            get => _currentSkinName;
            set
            {
                if (!ObtainedSkinsNames.Contains(value))
                {
                    Debug.LogError($"[UnitDataForSave] - Skin named ''{value}'' has not yet been obtained!");
                    return;
                }
                _currentSkinName = value;
            }
        }

        #endregion

        #region Constructors

        public UnitDataForSave(UnitInfo.UniqueUnitName uniqueName, List<string> obtainedSkinsNames, string currentSkinName)
        {
            _uniqueName = uniqueName;
            _currentLevel = 0;
            _amountCards = 0;
            _obtainedSkinsNames = obtainedSkinsNames;
            _currentSkinName = currentSkinName;
        }

        #endregion

        #region Control Methods

        public void IncreaseLevel()
        {
            _currentLevel++;
            OnCurrentLevelChanged?.Invoke();
        }
        public override bool Equals(object? obj)
        {
            if (obj is UnitDataForSave unitData) return UniqueName == unitData.UniqueName;
            return false;
        }

        #endregion
    }
}

