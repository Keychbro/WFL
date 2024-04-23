using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using DG.Tweening;
using Kamen;
using Kamen.DataSave;
using WOFL.Game;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using WOFL.Control;

namespace WOFL.UI
{
    public class FightScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Button _playButton;
        [Space]
        [SerializeField] private MainTopBar _mainTopBar;
        [SerializeField] private QuickBar _quickBar;
        [SerializeField] private MiniProfileIcon _miniProfileIcon;
        [SerializeField] private GameObject _gameBottomBar;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _playButton.onClick.AddListener(Play);
        }

        #endregion

        #region Control Methods

        public override async void Initialize()
        {
            base.Initialize();

            await UniTask.WaitUntil(() => DataSaveManager.Instance.IsDataLoaded);
            await UniTask.WaitUntil(() => DataSaveManager.Instance.MyData.ChoosenFraction != Fraction.FractionName.None);
            await Task.Delay(100);

            _playButton.onClick.AddListener(Play);
        }
        public override void Transit(bool isShow, bool isForth, ScreenManager.TransitionType type, float duration, Ease curve, MyCurve myCurve)
        {
            base.Transit(isShow, isForth, type, duration, curve, myCurve);

            if (DataSaveManager.Instance.MyData.ChoosenFraction == Fraction.FractionName.None)
            {
                PopupManager.Instance.Show("ChooseFractionPopup");
            }
        }
        private void Play()
        {
            _mainTopBar.SwitchVisible(false, true);
            _quickBar.SwitchVisible(false, true);
            _miniProfileIcon.gameObject.SetActive(false);
            _playButton.gameObject.SetActive(false);

            _gameBottomBar.SetActive(true);

            GameManager.Instance.StartBattle();
        }

        #endregion
    }
}