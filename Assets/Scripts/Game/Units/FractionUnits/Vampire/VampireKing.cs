using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOFL.Control;
using WOFL.Save;
using WOFL.Settings;
using Random = UnityEngine.Random;

namespace WOFL.Game
{
    public class VampireKing : VampireAttackingUnit
    {
        #region Variables

        protected VampireKingUnitLevelInfo _vampireKingUnitLevelInfo;

        #endregion

        #region Properties

        public bool IsSpawningBat { get; private set; }

        #endregion

        #region Control Methods

        public override void Initialize(UnitInfo unitInfo, int levelNumber, IDamageable.GameSideName sideName)
        {
            base.Initialize(unitInfo, levelNumber, sideName);
            if (_currentUnitInfo.WeaponInfo != null)
            {
                _weapon.Initialize(_currentUnitInfo.WeaponInfo.Levels[_levelNumber < _currentUnitInfo.WeaponInfo.Levels.Length ? _levelNumber : ^1]);
            }

            SpeedIncreaseValue = 1;

            _vampireUnitLevelInfo = (VampireUnitLevelInfo)_currentUnitInfo.LevelsHolder.Levels[_levelNumber];
            _vampireKingUnitLevelInfo = (VampireKingUnitLevelInfo)_vampireUnitLevelInfo;
        }
        public override void ControlUnit()
        {
            if (!IsAlive) return;

            if (!GameManager.Instance.IsBattleStarted)
            {
                _unitAnimator.SetTrigger("Win");
            }
            else
            {
                if (_currentTargetObject == null)
                {
                    Stand();
                }
                else
                {
                    if (CalculateDistanceOnXAxis(transform.position, _currentTargetObject.transform.position) >= _currentUnitInfo.WeaponInfo.Levels[_levelNumber].AttackRange)
                    {
                        Move();
                        SpawnBat();
                    }
                    else
                    {
                        Attack();
                    }
                }
            }
        }
        public virtual async UniTask SpawnBat()
        {
            if (IsSpawningBat || !IsAlive) return;

            IsSpawningBat = true;

            await UniTask.Delay(Mathf.RoundToInt(1 * 1000));
            await _weapon.DoAttack(_currentTargetDamageable, _currentTargetObject);
            IsSpawningBat = false;
        }

        #endregion
    }
}