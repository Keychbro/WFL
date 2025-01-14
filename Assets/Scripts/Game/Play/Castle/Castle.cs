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
using Random = UnityEngine.Random;
using WOFL.Game.Components;

namespace WOFL.Game
{
    public class Castle : MonoBehaviour, IManaOwnable, IDamageable, IDeathable, IBuilding
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private GameObject _viewHolder;
        [SerializeField] private SpriteRenderer _castleView;
        [SerializeField] private Transform _unitsSpawnPoint;
        [SerializeField] private Transform _hitPoint;
        [SerializeField] private HealthBar _healthBar;

        [Header("Settings")]
        [SerializeField] private IDamageable.GameSideName _sideName;
        [SerializeField] private float _timeToDestroyUnit;
        [SerializeField] private Vector3 _spawnUnitOffset;

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
        public Transform HitPoint { get => _hitPoint; }
        public int CurrentMana { get; private set; }
        public float ManaFillDuration { get; private set; }

        public UnitInfo[] Units { get => _units; }

        public List<IDamageable> AllDamageableObject { get => _allDamageableObject; }
        public List<IAttacking> AttackingObjects { get => _attackObjects; }

        public event Action<int> OnTakedDamage;
        public event Action OnManaValueChanged;
        public event Action<float> OnManaFilled;
        private Coroutine _manaCollect;
        private Fraction.FractionName _currentFractionName;

        #endregion

        #region Unity Methods

        private void FixedUpdate()
        {
            for (int i = 0; i <  _createdUnits.Count; i++)
            {
                _createdUnits[i].ControlUnit();
            }
        }

        #endregion

        #region Control Methods

        public void CallUpdateCastleView(CastleSettings castleSettings)
        {
            _castleSettings = castleSettings;
            _castleView.sprite = _castleSettings.CastleView;
            if (_castleView.sprite == null) _healthBar.gameObject.SetActive(false);
            else _healthBar.gameObject.SetActive(true);
        }
        public void Initialize(UnitInfo[] units, int health, float manaFill, Fraction.FractionName fractionName)
        {
            _currentFractionName = fractionName;
            CurrentMana = 0;
            MaxHealth = health;
            _currentHealth = MaxHealth;
            ManaFillDuration = 1f / manaFill;
            if (_currentFractionName != Fraction.FractionName.Zombi) _allDamageableObject.Add(this);

            _units = units;
            IsAlive = true;

            _manaCollect = StartCoroutine(Collect());
            StartCoroutine(AdjustHealthBar());
        }
        private IEnumerator AdjustHealthBar()
        {
            yield return null;
            yield return null;
            yield return null;

            if (_castleView.sprite == null) _healthBar.gameObject.SetActive(false);
            else _healthBar.gameObject.SetActive(true);
        }
        public void Clear()
        {
            for (int i = 0; i < _createdUnits.Count; i++)
            {
                Destroy(_createdUnits[i].gameObject);
            }
            _createdUnits.Clear();
            _allDamageableObject.Clear();
            _attackObjects.Clear();

            StopCoroutine(_manaCollect);
        }
        public void CreateUnitForMana(UnitInfo.UniqueUnitName uniqueName)
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
        public void CreateUnitForFree(UnitInfo.UniqueUnitName uniqueName, int level)
        {
            UnitInfo createdUnitInfo = GetUnitInfoByName(uniqueName);
            if (!CheckUnitForNull(createdUnitInfo)) return;

            SpawnUnit(createdUnitInfo, level);
        }
        private void SpawnUnit(UnitInfo createdUnitInfo, int levelNumber)
        {
            Unit createdUnit = Instantiate(createdUnitInfo.Prefab);
            createdUnit.transform.position = _unitsSpawnPoint.position + new Vector3(
                Random.Range(-_spawnUnitOffset.x, _spawnUnitOffset.x),
                Random.Range(-_spawnUnitOffset.y, _spawnUnitOffset.y),
                Random.Range(-_spawnUnitOffset.z, _spawnUnitOffset.z));

            createdUnit.transform.parent = transform;
            createdUnit.Initialize(createdUnitInfo, levelNumber, SideName);
            createdUnit.OnDead += DestroyUnit;

            _createdUnits.Add(createdUnit);
            if (createdUnit.TryGetComponent(out IDamageable damageableObject)) _allDamageableObject.Add(damageableObject);
            if (createdUnit.TryGetComponent(out IAttacking attackObject)) _attackObjects.Add(attackObject);
        }
        private async void DestroyUnit(IDeathable deadObject)
        {
            Unit destroyUnit = (Unit)deadObject;
            destroyUnit.OnDead -= DestroyUnit;

            _createdUnits.Remove(destroyUnit);
            if (destroyUnit.TryGetComponent(out IDamageable damageableObject)) _allDamageableObject.Remove(damageableObject);
            if (destroyUnit.TryGetComponent(out IAttacking attackObject)) _attackObjects.Remove(attackObject);

            await Task.Delay(Mathf.RoundToInt(_timeToDestroyUnit * 1000));
            Destroy(((Unit)deadObject).gameObject);
        }
        private UnitInfo GetUnitInfoByName(UnitInfo.UniqueUnitName uniqueName)
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
        public void TakeDamageInPointWithRadius(Vector3 damagePosition,float radius, int damage)
        {
            List<IDamageable> nearbyUnits = new List<IDamageable>();
            for (int i = 0; i < AllDamageableObject.Count; i++)
            {
                if (Mathf.Abs(damagePosition.x - AllDamageableObject[i].HitPoint.transform.position.x) < radius) nearbyUnits.Add(AllDamageableObject[i]);
            }

            for (int i = 0; i < nearbyUnits.Count; i++)
            {
                nearbyUnits[i].TakeDamage(damage);
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
            if (_currentHealth <= 0) 
            {
                _currentHealth = 0;
                Death();
            }

            OnTakedDamage?.Invoke(value);
        }

        #endregion

        #region IDeathable Methods

        public void Death()
        {
            OnDead?.Invoke(this);
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

