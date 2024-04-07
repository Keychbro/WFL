using System;
using UnityEngine;
using System.Collections.Generic;
using WOFL.Save;
using WOFL.Settings;
using System.Linq;
using WOFL.Game;
using WOFL.DataSave;
using WOFL.Stats;
using WOFL.Control;

namespace Kamen.DataSave
{
    [Serializable] public class Data
    {
        #region Variables

        [SerializeField] private List<TimerInfo> _timersInfo = new List<TimerInfo>();
        [SerializeField] private DateTime _quitTime;

        [Header("Player Data")]
        [SerializeField] private string _username;
        [SerializeField] private int _iconNumber;
        [Space]
        [SerializeField] private List<UserStatsData> _userStatsDatas = new List<UserStatsData>();

        [Header("Currency")]
        [SerializeField] private int _gold = 100000000; //Change to 0
        public event Action OnGoldAmountChanged;
        [SerializeField] private int _diamonds = 100000000; //Change to 0
        public event Action OnDiamondsAmountChanged;
        [SerializeField] private int _tools = 100000000; //Change to 0
        public event Action OnToolsAmountChanged;

        [Header("City")]
        [SerializeField] private int _cityLevel;
        public event Action OnCityLevelChanged;

        [Header("Castle")]
        [SerializeField] private int _castleManaSpeedCollectLevel;
        [SerializeField] private int _castleHealthIncreaseLevel;

        [Header("Units")]
        [SerializeField] private List<UnitDataForSave> _unitsDatas = new List<UnitDataForSave>();

        [Header("Fraction")]
        [SerializeField] private Fraction.FractionName _choosenFraction = Fraction.FractionName.None; //Change to none

        [Header("Shop")]
        [SerializeField] private List<ChestManager.ChestType> _chests = new List<ChestManager.ChestType>();
        public event Action OnChestsAmountChanged;

        [Header("DailyBonus")]
        [SerializeField] private int _dailyBonusFactor = 1;

        #endregion

        #region PlayerData

        public string Username
        {
            get => _username;
            set
            {
                if (value == null || value.Length == 0)
                {
                    Debug.LogError("[Data] - Attempting to assign an incorrect name!");
                    return;
                }

                _username = value;
            }
        }
        public int IconNumber
        {
            get => _iconNumber;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_iconNumber'' minus value");
                    return;
                }

                _iconNumber = value;
            }
        }
        public List<UserStatsData> UserStatsDatas { get => _userStatsDatas; }

        #endregion

        #region PlayerData Methods

        public void AdjustUserStatsDatas(UserStatsInfo[] userStatsInfos)
        {
            for (int i = 0; i < userStatsInfos.Length; i++)
            {
                if (!_userStatsDatas.Any(userStatsData => userStatsData.StatsName == userStatsInfos[i].StatsName))
                {
                    _userStatsDatas.Add(new UserStatsData(userStatsInfos[i].StatsName, userStatsInfos[i].StartValue));
                }
            }
        }
        public UserStatsData GetUserStatsDataMyName(string name)
        {
            return _userStatsDatas.First(userStatsData => userStatsData.StatsName == name);
        }

        #endregion

        #region Properties

        public List<TimerInfo> TimersInfo { get => _timersInfo; }
        public DateTime QuitTime 
        { 
            get => _quitTime;
            set 
            {
                if (value != null) _quitTime = value;
            }
        }

        #endregion

        #region Currency Properties
        public int Gold
        {
            get => _gold;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_gold'' minus value");
                    return;
                }

                _gold = value;
                OnGoldAmountChanged?.Invoke();
            }
        }
        public int Diamonds
        {
            get => _diamonds;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Dats] - Attempy to assign variable ''_diamonds'' minus value ");
                    return;
                }

                _diamonds = value;
                OnDiamondsAmountChanged?.Invoke();
            }
        }
        public int Tools
        {
            get => _tools;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Dats] - Attempy to assign variable ''_tools'' minus value ");
                    return;
                }

                _tools = value;
                OnToolsAmountChanged?.Invoke();
            }
        }

        #endregion

        #region City Properties

        public int CityLevel
        {
            get => _cityLevel;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_cityLevel'' minus value");
                    return;
                }

                _cityLevel = value;
                OnCityLevelChanged?.Invoke();
            }
        }

        #endregion

        #region Castle Properties

        public int CastleManaSpeedCollectLevel 
        { 
            get => _castleManaSpeedCollectLevel; 
            set
            {
                if (value < 0) return;
                _castleManaSpeedCollectLevel = value;
            }
        }
        public int CastleHealthIncreaseLevel 
        { 
            get => _castleHealthIncreaseLevel;
            set
            {
                if (value < 0) return;
                _castleHealthIncreaseLevel = value;
            }
        }

        #endregion

        #region Unit Properties

        public List<UnitDataForSave> UnitsDatas { get => _unitsDatas; }

        #endregion

        #region Unit Methods

        public void AdjustUnitsDatas(UnitInfo[] unitsInfos)
        {
            for (int i = 0; i < unitsInfos.Length; i++)
            {
                if (!_unitsDatas.Any(unitData => unitData.UniqueName == unitsInfos[i].UniqueName))
                {
                     List<string> obtainedUnitList = unitsInfos[i].SkinsHolder.Skins
                        .Where(skin => skin.UnlockStatus != UnlockStatus.Locked)
                        .Select(skin => skin.UniqueName)
                        .ToList();

                    _unitsDatas.Add(new UnitDataForSave(unitsInfos[i].UniqueName, obtainedUnitList, obtainedUnitList[0]));
                }
            }
        }
        public UnitDataForSave GetUnitDataMyName(string name)
        {
            return _unitsDatas.First(unitData => unitData.UniqueName == name);
        }

        #endregion

        #region Fraction Methods

        public Fraction.FractionName ChoosenFraction 
        { 
            get => _choosenFraction; 
            set
            {
                if (value == Fraction.FractionName.None)
                {
                    Debug.LogError("[Data] - Attempt to assign variable ''_choosenFraction'' None type");
                    return;
                }

                _choosenFraction = value;
            }
        }

        #endregion

        #region Shop Methods

        public void AddChest(ChestManager.ChestType chestType)
        {
            _chests.Add(chestType);
            OnChestsAmountChanged?.Invoke();
        }
        public void RemoveChest(ChestManager.ChestType chestType)
        {
            _chests.Remove(chestType);
            OnChestsAmountChanged?.Invoke();
        }
        public void RemoveAtChest(int index)
        {
            _chests.RemoveAt(index);
            OnChestsAmountChanged?.Invoke();
        }

        #endregion

        #region Daily Factor Properties

        public int DailyBonusFactor
        {
            get => _dailyBonusFactor;
            set
            {
                if (value < _dailyBonusFactor)
                {
                    Debug.LogError("An attempt was made to assign a value less than the current one");
                    return;
                }
                _dailyBonusFactor = value;
            }
        }

        #endregion
    }
}