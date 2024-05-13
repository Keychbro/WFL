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
    public class HumanBarrack : Unit, IBuilding
    {
        #region Variables

        protected Castle _ownerCastle;
        protected BarracksInfo _currentBarrackInfo;

        #endregion

        #region Proepeties

        public bool IsSpawning { get; protected set; }

        #endregion

        #region Control Methods

        public override void Initialize(UnitInfo unitInfo, int levelNumber, IDamageable.GameSideName sideName)
        {
            base.Initialize(unitInfo, levelNumber, sideName);
            _currentBarrackInfo = (BarracksInfo)unitInfo;
            _ownerCastle = transform.GetComponentInParent<Castle>();
        }
        public override void ControlUnit()
        {
            if (!IsAlive || _currentBarrackInfo == null) return;

            Spawn();
        }
        public async UniTask Spawn()
        {
            if (IsSpawning || !IsAlive) return;

            IsSpawning = true;
            await UniTask.Delay(Mathf.RoundToInt(1000 * _currentBarrackInfo.DelayBetweenSpawnUnit));

            _ownerCastle.CreateUnitForFree(_currentBarrackInfo.SpawnUnitName, _levelNumber);
            IsSpawning = false;
        }

        #endregion
    }
}