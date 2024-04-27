using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WOFL.Control;

namespace WOFL.Game
{
    public class AIEnemy : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Castle _controllableCastle;

        [Header("Variables")]
        private AIEnemySettings _levelSettings;

        #endregion

        #region Unity Methods

        private void Start()
        {
            ControlGame();
        }

        #endregion

        #region Control Methods

        public void UpdateLevelSettings(AIEnemySettings levelSettings)
        {
            _levelSettings = levelSettings;
        }
        private async void ControlGame()
        {
            await UniTask.WaitUntil(() => GameManager.Instance.IsBattleStarted);
            if (_levelSettings = null) return;

            for (int i = 0; i < _levelSettings.UnitList.Length; i++)
            {
                await Task.Delay(Mathf.RoundToInt(_levelSettings.UnitList[i].SpawnDelay * 1000));
                _controllableCastle.CreateUnitForFree(_levelSettings.UnitList[i].UniqueUnitName, _levelSettings.UnitList[i].UnitLevel);
            }
        }

        #endregion
    }
}