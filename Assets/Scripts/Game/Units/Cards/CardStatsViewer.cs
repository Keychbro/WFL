using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using WOFL.Settings;
using WOFL.Save;
using System;

namespace WOFL.UI
{
    public class CardStatsViewer : MonoBehaviour
    {
        #region Enums

        public enum ActiveView
        {
            Image,
            Stats,
            Transition
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private RectTransform _unitImageView;
        [SerializeField] private RectTransform _unitStatsView;
        [Space]
        [SerializeField] private UnitStats _currentUnitStats;
        [SerializeField] private TextMeshProUGUI _moreButtonText;

        [Header("Settings")]
        [SerializeField] private float _swapDuration;
        [SerializeField] private Ease _swapEase;

        [Header("Variables")]
        private ActiveView _activeView = ActiveView.Image;
        private UnitInfo _unitInfo;
        private UnitDataForSave _unitData;
        private float _distanceBetweenViews;

        #endregion

        #region Control Methods

        public void Initialize(UnitInfo unitInfo, UnitDataForSave unitData)
        {
            _unitInfo = unitInfo;
            _unitData = unitData;

            CallUpdateStats();
            _distanceBetweenViews = Vector2.Distance(_unitImageView.anchoredPosition, _unitStatsView.anchoredPosition);
        }
        public void CallUpdateStats()
        {
            _currentUnitStats.UpdateStats(
                _unitInfo.LevelsHolder == null ? null : _unitInfo.LevelsHolder.Levels[_unitData.CurrentLevel < _unitInfo.LevelsHolder.Levels.Length ? _unitData.CurrentLevel : ^1],
                _unitInfo.WeaponInfo == null ? null : _unitInfo.WeaponInfo.Levels[_unitData.CurrentLevel < _unitInfo.WeaponInfo.Levels.Length ? _unitData.CurrentLevel : ^1]);
        }
        public void SwapViews()
        {
            if (_activeView == ActiveView.Transition) return;
            ActiveView oldView = _activeView;

            int sign = oldView == ActiveView.Image ? -1 : 1;
            _activeView = ActiveView.Transition;

            MoveView(_unitImageView, sign);
            MoveView(_unitStatsView, sign);
            _moreButtonText.text = oldView == ActiveView.Image ? "BACK" : "MORE";

            StartCoroutine(WaitToFinishTransition(oldView));
        }
        private IEnumerator WaitToFinishTransition(ActiveView oldView)
        {
            yield return new WaitForSeconds(_swapDuration);

            _activeView = oldView == ActiveView.Image ? ActiveView.Stats : ActiveView.Image;
        }
        private void MoveView(RectTransform view, int sign)
        {
            view.DOAnchorPos(view.anchoredPosition + sign * new Vector2(_distanceBetweenViews, 0), _swapDuration).SetEase(_swapEase);
        }


        #endregion
    }
}

