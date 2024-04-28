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
        public IEnumerator Play()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;
            yield return new WaitUntil(() => GameManager.Instance.IsBattleStarted);
            if (_levelSettings == null) yield break;

            for (int i = 0; i < _levelSettings.UnitList.Length; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return new WaitForSeconds(_levelSettings.UnitList[i].SpawnDelay);;
                _controllableCastle.CreateUnitForFree(_levelSettings.UnitList[i].UniqueUnitName, _levelSettings.UnitList[i].UnitLevel);
            }
        }

        #endregion
    }
}