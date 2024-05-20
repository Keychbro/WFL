using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Game;
using System.Linq;
using Kamen.DataSave;
using WOFL.UI;
using System.Threading.Tasks;
using Kamen.UI;

namespace WOFL.Control
{
    public class GameManager : SingletonComponent<GameManager>
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Castle _alliedCastle;
        [SerializeField] private AIEnemy _aiEnemy;
        [SerializeField] private Castle _enemyCastle;
        [Space]
        [SerializeField] private ManaView _manaView;
        [SerializeField] private GameCardsPanel _gameCardsPanel;

        [Header("Settings")]
        [SerializeField] private float _delayBetweenUpdateBattleControl;

        [Header("Variables")]
        private Coroutine _aiEnemyPlayCoroutine;
        private Coroutine _battleControlCoroutine;

        #endregion

        #region Properties

        public bool IsBattleStarted { get; private set; }

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _alliedCastle.OnDead -= CallLose;
            _enemyCastle.OnDead -= CallWin;
        }

        #endregion

        #region Control Methods

        public void CallUpdateLevel(AIEnemySettings enemySettings)
        {
            _aiEnemy.UpdateLevelSettings(enemySettings);
        }
        public void StartBattle()
        {
            Fraction playerFraction = FractionManager.Instance.CurrentFraction;
            _alliedCastle.Initialize(playerFraction.CastleSettings, playerFraction.Units);
            _alliedCastle.OnDead += CallLose;
            _manaView.Initialize(_alliedCastle);
            _gameCardsPanel.Initialize(_alliedCastle, playerFraction.Units);

            Fraction enemyFraction = FractionManager.Instance.GetFractionByName(_aiEnemy.EnemyFraction);

            _aiEnemyPlayCoroutine = StartCoroutine(_aiEnemy.Play());
            _enemyCastle.Initialize(enemyFraction.CastleSettings, enemyFraction.Units);
            _enemyCastle.OnDead += CallWin;

            if (DataSaveManager.Instance.MyData.UnitsDatas[0].CurrentLevel != 1)
            {
                DataSaveManager.Instance.MyData.UnitsDatas[0].IncreaseLevel();
                DataSaveManager.Instance.SaveData();
            }

            IsBattleStarted = true;
            _battleControlCoroutine = StartCoroutine(BattleControl());
        }
        public void CallTakeDamageInPointWithRadius(IDamageable.GameSideName targetSideName,Vector3 damagePosition, float radius, int damage)
        {
            switch (targetSideName)
            {
                case IDamageable.GameSideName.Allied:
                    _alliedCastle.TakeDamageInPointWithRadius(damagePosition, radius, damage);
                    break;
                case IDamageable.GameSideName.Enemy:
                    _enemyCastle.TakeDamageInPointWithRadius(damagePosition, radius, damage);
                    break;
            }
        }
        private IEnumerator BattleControl()
        {
            while (IsBattleStarted)
            {
                yield return new WaitForSeconds(_delayBetweenUpdateBattleControl);
                _alliedCastle.UpdateTargets(_enemyCastle);
                _enemyCastle.UpdateTargets(_alliedCastle);
            }
        }
        private void CallWin(IDeathable deathableObject)
        {
            IsBattleStarted = false;
            PopupManager.Instance.Show("WinScreenPopup");
        }
        private void CallLose(IDeathable deathableObject)
        {
            IsBattleStarted = false;
            PopupManager.Instance.Show("LoseScreenPopup");
        }

        #endregion
    }
}