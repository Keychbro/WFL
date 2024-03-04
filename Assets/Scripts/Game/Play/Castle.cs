using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Settings;
using Kamen.DataSave;
using System.Linq;
using WOFL.Save;

namespace WOFL.Game
{
    public class Castle : MonoBehaviour, IManaOwnable, IDamagable, IDeathable
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private SpriteRenderer _castleView;
        [SerializeField] private Transform _unitsSpawnPoint;

        [Header("Variables")]
        private int _currentHealth;
        private CastleSettings _castleSettings;
        private UnitInfo[] _units;

        #endregion

        #region Properties

        public int CurrentGold { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentMana { get; private set; }
        public float ManaFillDuration { get; private set; }

        public UnitInfo[] Units { get => _units; }

        public event Action<int> OnTakedDamage;
        public event Action OnManaValueChanged;
        public event Action<float> OnManaFilled;

        #endregion

        #region Control Methods

        public void Initialize(CastleSettings castleSettings, UnitInfo[] units)
        {
            _castleSettings = castleSettings;

            _castleView.sprite = _castleSettings.CastleView;
            MaxHealth = _castleSettings.StartHealth + _castleSettings.IncreaseHealthStep * DataSaveManager.Instance.MyData.CastleHealthIncreaseLevel;
            ManaFillDuration = 100f / _castleSettings.StartManaSpeedCollectValue + _castleSettings.IncreaseManaSpeedCollectStep * DataSaveManager.Instance.MyData.CastleManaSpeedCollectLevel;

            _units = units;

            StartCoroutine(Collect());
        }
        public void CreateUnitForMana(string name)
        {
            UnitInfo createdUnitInfo = GetUnitInfoByName(name);
            if (CheckUnitForNull(createdUnitInfo)) return;

            UnitDataForSave unitData = DataSaveManager.Instance.MyData.GetUnitDataMyName(createdUnitInfo.UniqueName);
            if (!CheckUnitForLevel(createdUnitInfo, unitData)) return;
            if (!CheckUnitForMana(createdUnitInfo, unitData)) return;

            Unit createdUnit = Instantiate(createdUnitInfo.Prefab, _unitsSpawnPoint.position, transform.rotation, transform);
            CurrentMana -= createdUnitInfo.LevelsHolder.Levels[unitData.CurrentLevel].ManaPrice;
        }
        private void CreateUnitForFree(string name)
        {
            UnitInfo createdUnitInfo = GetUnitInfoByName(name);
            if (CheckUnitForNull(createdUnitInfo)) return;

            Unit createdUnit = Instantiate(createdUnitInfo.Prefab, _unitsSpawnPoint.position, transform.rotation, transform);
        }
        private UnitInfo GetUnitInfoByName(string name)
        {
            return _units.First(unit => unit.UniqueName == name);
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

        private bool CheckUnitForNull(UnitInfo unit)
        {
            if (unit != null)
            {
                Debug.LogError($"[Castle] - Unit with name <<{name}>>, doesnt exist!");
                return false;
            }
            return true;
        }
        private bool CheckUnitForLevel(UnitInfo unit, UnitDataForSave unitData)
        {
            if (unitData.CurrentLevel == 0) return false;
            return true;
        }
        private bool CheckUnitForMana(UnitInfo unit, UnitDataForSave unitData)
        {
            if (unit.LevelsHolder.Levels[unitData.CurrentLevel].ManaPrice < CurrentMana) return false;
            return true;
        }

        #endregion
    }
}

