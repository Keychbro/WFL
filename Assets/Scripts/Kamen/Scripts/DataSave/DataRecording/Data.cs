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
using WOFL.BattlePass;
using WOFL.DiamondPass;
using Unity.VisualScripting;

namespace Kamen.DataSave
{
    [Serializable] public class Data
    {
        #region Variables

        [SerializeField] private List<TimerInfo> _timersInfo = new List<TimerInfo>();
        [SerializeField] private DateTime _quitTime;

        [Header("Player Data")]
        [SerializeField] private string _username;
        public event Action OnUsernameChanged;
        [SerializeField] private int _iconNumber;
        public event Action OnIconNumberChanged;
        [Space]
        [SerializeField] private List<UserStatsData> _userStatsDatas = new List<UserStatsData>();

        [Header("Currency")]
        [SerializeField] private int _gold = 1000; //Change to 0
        public event Action OnGoldAmountChanged;
        [SerializeField] private int _diamonds = 0; //Change to 0
        public event Action OnDiamondsAmountChanged;
        [SerializeField] private int _tools = 0; //Change to 0
        public event Action OnToolsAmountChanged;

        [Header("Main Game")]
        [SerializeField] private int _gameLevel;
        public event Action OnGameLevelChanged;

        [SerializeField] private int _zombieLevel;
        public event Action OnZombieLevelChanged;

        [Header("City")]
        [SerializeField] private int _cityLevel;
        public event Action OnCityLevelChanged;

        [Header("Castle")]
        [SerializeField] private int _castleManaSpeedCollectLevel;
        [SerializeField] private int _castleHealthIncreaseLevel;
        [SerializeField] private List<UpgradeCastleCardData> _upgradeCastleCardDatas = new List<UpgradeCastleCardData>();

        [Header("Units")]
        [SerializeField] private List<UnitDataForSave> _unitsDatas = new List<UnitDataForSave>();

        [Header("Fraction")]
        [SerializeField] private Fraction.FractionName _choosenFraction = Fraction.FractionName.None; //Change to none

        [Header("Shop")]
        [SerializeField] private List<ChestManager.ChestType> _chests = new List<ChestManager.ChestType>();
        public event Action OnChestsAmountChanged;

        [Header("DailyBonus")]
        [SerializeField] private int _dailyBonusFactor = 1;

        [Header("Ads")]
        [SerializeField] private bool _isAdsRemoved;
        public event Action OnAdsRemoved;

        [Header("BattlePass")]
        [SerializeField] private List<BattlePassDataSave> _battlePassesDataSave = new List<BattlePassDataSave>();

        [Header("Diamond Pass")]
        [SerializeField] private DiamondPassDataSave _diamondPassDataSave = new DiamondPassDataSave();

        #endregion

        #region PlayerData Properties

        public string Username
        {
            get => _username;
            set
            {
                if (value == null || value.Length == 0)
                {
                    Debug.LogError("[Data] - Attempting to assign an incorrect seasonName!");
                    return;
                }

                _username = value;
                OnUsernameChanged?.Invoke();
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
                OnIconNumberChanged?.Invoke();
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
        public UserStatsData GetUserStatsDataByName(string name)
        {
            return _userStatsDatas.First(userStatsData => userStatsData.StatsName == name);
        }

        #endregion

        #region Timer Properties

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

        #region MainGame Properties

        public int GameLevel
        {
            get => _gameLevel;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_gameLevel'' minus value");
                    return;
                }

                _gameLevel = value;
                OnGameLevelChanged?.Invoke();
            }
        }
        public int ZombieLevel
        {
            get => _zombieLevel;
            set
            {
                if (value < 0)
                {
                    Debug.LogError($"[Data] - Attempt to assign variable ''_zombieLevel'' minus value");
                    return;
                }

                _zombieLevel = value;
                OnZombieLevelChanged?.Invoke();
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
        public List<UpgradeCastleCardData> UpgradeCastleCardDatas { get => _upgradeCastleCardDatas; }

        #endregion

        #region Castle Methods

        public void AdjustUpgradeCastleCardDatas(UpgradeCastleCardLevelsHolder[] upgradeCastleCardLevelsHolders)
        {
            for (int i = 0; i < upgradeCastleCardLevelsHolders.Length; i++)
            {
                if (!_upgradeCastleCardDatas.Any(holder => holder.Type == upgradeCastleCardLevelsHolders[i].Type))
                {
                    _upgradeCastleCardDatas.Add(new UpgradeCastleCardData(upgradeCastleCardLevelsHolders[i].Type));
                }
            }
        }
        public UpgradeCastleCardData GetUpgradeCastleCardDataByType(UpgradeCastleCardLevelsHolder.UpgradeCastleCardType type)
        {
            return _upgradeCastleCardDatas.First(upgradeCastleCardDatas => upgradeCastleCardDatas.Type == type);
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
        public UnitDataForSave GetUnitDataMyName(UnitInfo.UniqueUnitName uniqueName)
        {
            return _unitsDatas.First(unitData => unitData.UniqueName == uniqueName);
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

        #region Ads Properties

        public bool IsAdsRemoved
        {
            get => _isAdsRemoved;
            set
            {
                if (!value)
                {
                    Debug.LogError("Trying to assign a negative meaning");
                    return;
                }
                _isAdsRemoved = value;
                OnAdsRemoved?.Invoke();
            }
        }

        #endregion

        #region BattlePass Methods

        public void AdjustBattlePassDatas(BattlePassManager.SeasonInfo[] seasonInfos)
        {
            for (int i = 0; i < seasonInfos.Length; i++)
            {
                if (!_battlePassesDataSave.Any(battlePassDataSave => battlePassDataSave.SeasonName == seasonInfos[i].BattlePassLine.SeasonName))
                {
                    _battlePassesDataSave.Add(new BattlePassDataSave(seasonInfos[i].BattlePassLine));
                }
            }

            for (int i = 0; i < _battlePassesDataSave.Count; i++)
            {
                _battlePassesDataSave[i].UpdateAllRewardStates();
            } 
        }
        public BattlePassDataSave GetBattlePassDataByName(string seasonName)
        {
            return _battlePassesDataSave.First(battlePassDataSave => battlePassDataSave.SeasonName == seasonName);
        }

        #endregion

        #region DiamondPass Methods

        public DiamondPassDataSave DiamondPassDataSave { get => _diamondPassDataSave; }

        #endregion
    }
}