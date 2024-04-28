using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        protected CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Unity Methods

        private void OnApplicationQuit()
        {
            _cancellationTokenSource?.Cancel();
        }

        #endregion

        #region Control Methods

        public void UpdateLevelSettings(AIEnemySettings levelSettings)
        {
            _levelSettings = levelSettings;
        }
        public async void ControlGame()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;
            await UniTask.WaitUntil(() => GameManager.Instance.IsBattleStarted);
            if (_levelSettings == null) return;

            for (int i = 0; i < _levelSettings.UnitList.Length; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(Mathf.RoundToInt(_levelSettings.UnitList[i].SpawnDelay * 1000), cancellationToken);
                _controllableCastle.CreateUnitForFree(_levelSettings.UnitList[i].UniqueUnitName, _levelSettings.UnitList[i].UnitLevel);
            }
        }

        #endregion
    }
}