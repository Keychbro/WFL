using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kamen;
using System;
using WOFL.Control;
using WOFL.Settings;

namespace WOFL.BattlePass
{
    [RequireComponent(typeof(Button))]
    public class BattlePassTopBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _amountCompletedPoints;
        [SerializeField] private TimerViewer _timerViewer;

        [Header("Settings")]
        [SerializeField] private string _battlePassTimerName;

        [Header("Variables")]
        private BattlePassManager.SeasonInfo _currentSeasonInfo;
        private BattlePassDataSave _currentBattlePassDataSave;
        private Button _button;
        public event Action OnPassFinished;

        #endregion

        #region Control Methods

        public void Initialize(int seasonNumber, BattlePassManager.SeasonInfo seasonInfo, BattlePassDataSave battlePassDataSave)
        {
            _currentSeasonInfo = seasonInfo;
            _currentBattlePassDataSave = battlePassDataSave;

            _title.text = $"Season {seasonNumber}";

            _amountCompletedPoints.text = $"{battlePassDataSave.Score/seasonInfo.BattlePassLine.ScoreForOneLevel}/{battlePassDataSave.TotalLevels}";

            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);

            AdjustTimer(new DateTime(seasonInfo.FinishSeasonDate.Year, seasonInfo.FinishSeasonDate.Month, seasonInfo.FinishSeasonDate.Day));
        }
        private void AdjustTimer(DateTime finishTime)
        {
            TimerManager.Instance.AddNewTimerViewByName(_battlePassTimerName, _timerViewer);
            TimerManager.Instance.RecordNewTimer(_battlePassTimerName, new Timer(new KamenTime(Mathf.RoundToInt((float)(finishTime - DateTime.Now).TotalSeconds)), Timer.Mode.Always, false, 1));
            TimerManager.Instance.ActivateTimer(_battlePassTimerName);
            TimerManager.Instance.GetSubscribeOnTimeOver(_battlePassTimerName).OnTimeIsOver += CallClosePass;
        }
        private void Click()
        {

        }
        private void CallClosePass()
        {
            OnPassFinished?.Invoke();
        }

        #endregion
    }
}