using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;
using Kamen.DataSave;
using System.Linq;
using WOFL.Save;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace WOFL.Game
{
    public class Castle : MonoBehaviour, IManaOwnable, IDamageable, IDeathable
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private SpriteRenderer _castleView;
        [SerializeField] private Transform _unitsSpawnPoint;

        [Header("Settings")]
        [SerializeField] private IDamageable.GameSideName _sideName;
        [SerializeField] private float _timeToDestroyUnit;

        [Header("Variables")]
        private int _currentHealth;
        private CastleSettings _castleSettings;
        private UnitInfo[] _units;
        private List<Unit> _createdUnits = new List<Unit>();
        private List<IDamageable> _allDamageableObject = new List<IDamageable>();
        private List<IAttacking> _attackObjects = new List<IAttacking>();

        public event Action<IDeathable> OnDead;
        public bool IsAlive { get; private set; }

        #endregion

        #region Properties

        public IDamageable.GameSideName SideName { get => _sideName; }
        public int CurrentGold { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentMana { get; private set; }
        public float ManaFillDuration { get; private set; }

        public UnitInfo[] Units { get => _units; }

        public List<IDamageable> AllDamageableObject { get => _allDamageableObject; }
        public List<IAttacking> AttackingObjects { get => _attackObjects; }

        public event Action<int> OnTakedDamage;
        public event Action OnManaValueChanged;
        public event Action<float> OnManaFilled;

        #endregion

        #region Unity Methods

        private void Update()
        {
            for (int i = 0; i <  _createdUnits.Count; i++)
            {
                _createdUnits[i].ControlUnit();
            }
        }

        #endregion

        #region Control Methods

        public async void Initialize(CastleSettings castleSettings, UnitInfo[] units)
        {
            _castleSettings = castleSettings;

            _castleView.sprite = _castleSettings.CastleView;
            MaxHealth = _castleSettings.StartHealth + _castleSettings.IncreaseHealthStep * DataSaveManager.Instance.MyData.CastleHealthIncreaseLevel;
            ManaFillDuration = 100f / _castleSettings.StartManaSpeedCollectValue + _castleSettings.IncreaseManaSpeedCollectStep * DataSaveManager.Instance.MyData.CastleManaSpeedCollectLevel;
            _allDamageableObject.Add(this);

            _units = units;
            IsAlive = true;

            StartCoroutine(Collect());
        }
        public void CreateUnitForMana(string uniqueName)
        {
            UnitInfo createdUnitInfo = GetUnitInfoByName(uniqueName);
            if (!CheckUnitForNull(createdUnitInfo)) return;

            UnitDataForSave unitData = DataSaveManager.Instance.MyData.GetUnitDataMyName(createdUnitInfo.UniqueName);
            if (!CheckUnitForLevel(unitData)) return;
            if (!CheckUnitForMana(createdUnitInfo, unitData)) return;

            SpawnUnit(createdUnitInfo, unitData.CurrentLevel);

            CurrentMana -= createdUnitInfo.LevelsHolder.Levels[unitData.CurrentLevel].ManaPrice;
            OnManaValueChanged?.Invoke();
        }
        private void CreateUnitForFree(string uniqueName, int level)
        {
            UnitInfo createdUnitInfo = GetUnitInfoByName(uniqueName);
            if (CheckUnitForNull(createdUnitInfo)) return;

            SpawnUnit(createdUnitInfo, level);
        }
        private void SpawnUnit(UnitInfo createdUnitInfo, int levelNumber)
        {
            Unit createdUnit = Instantiate(createdUnitInfo.Prefab);
            createdUnit.transform.position = _unitsSpawnPoint.position;
            createdUnit.transform.parent = transform;
            createdUnit.Initialize(createdUnitInfo.LevelsHolder.Levels[levelNumber], levelNumber, SideName);
            createdUnit.OnDead += DestroyUnit;

            _createdUnits.Add(createdUnit);
            if (createdUnit.TryGetComponent(out IDamageable damageableObject)) _allDamageableObject.Add(damageableObject);
            if (createdUnit.TryGetComponent(out IAttacking attackObject)) _attackObjects.Add(attackObject);
        }
        private async void DestroyUnit(IDeathable deadObject)
        {
            await Task.Delay(Mathf.RoundToInt(_timeToDestroyUnit * 1000));
            Unit destroyUnit = (Unit)deadObject;
            destroyUnit.OnDead -= DestroyUnit;

            _createdUnits.Remove(destroyUnit);
            if (destroyUnit.TryGetComponent(out IDamageable damageableObject)) _allDamageableObject.Remove(damageableObject);
            if (destroyUnit.TryGetComponent(out IAttacking attackObject)) _attackObjects.Remove(attackObject);
            Destroy(((Unit)deadObject).gameObject);
        }
        private UnitInfo GetUnitInfoByName(string uniqueName)
        {
            return _units.First(unit => unit.UniqueName == uniqueName);
        }

        #endregion

        #region Units Control

        public void UpdateTargets(Castle otherCastle)
        {
            for (int i = 0; i < _attackObjects.Count; i++)
            {
                _attackObjects[i].FindClosestTarget(otherCastle.AllDamageableObject);
            }
        }

        #endregion

        #region IManaOwnable Methods

        public IEnumerator Collect()
        {
            while (true)
            {
                DOVirtual.Float(0f, 1f, ManaFillDuration, CallOnManaFiller).SetEase(Ease.Linear);
                yield return new WaitForSeconds(ManaFillDuration);
                CurrentMana++;
                OnManaValueChanged?.Invoke();
            }
        }
        private void CallOnManaFiller(float value) => OnManaFilled?.Invoke(value);

        #endregion

        #region IDamagable Methods

        public void TakeDamage(int value)
        {
            if (value < 0) return;

            _currentHealth -= value;
            OnTakedDamage?.Invoke(value);

            if (_currentHealth < 0) Death();
        }

        #endregion

        #region IDeathable Methods

        public void Death()
        {
           
        }

        #endregion

        #region Spawn Unit Checks Methods

        private bool CheckUnitForNull(UnitInfo unitInfo)
        {
            if (unitInfo == null)
            {
                Debug.LogError($"[Castle] - Unit with uniqueName ''{unitInfo.UniqueName}'', doesnt exist!");
                return false;
            }
            return true;
        }
        private bool CheckUnitForLevel(UnitDataForSave unitData)
        {
            if (unitData.CurrentLevel == 0) return false;
            return true;
        }
        private bool CheckUnitForMana(UnitInfo unit, UnitDataForSave unitData)
        {
            if (CurrentMana < unit.LevelsHolder.Levels[unitData.CurrentLevel].ManaPrice) return false;
            return true;
        }

        #endregion
    }
}

