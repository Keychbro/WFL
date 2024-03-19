using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Kamen.UI
{
    public class ScreenManager : SingletonComponent<ScreenManager>
    {
        #region Enums

        public enum TransitionType
        {
            None,
            Fade,
            SwipeDown,
            SwipeUp,
            SwipeRight,
            SwipeLeft
        }

        private enum State
        {
            Standing,
            Transition
        }

        #endregion

        #region Classes

        [Serializable] private class TransitionInfo
        {
            #region TransitionInfo Variables

            [SerializeField] private TransitionType _type;
            [SerializeField] private Ease _curve;
            [SerializeField] private MyCurve _myCurve;
            [SerializeField] private float _duration;

            #endregion

            #region TransitionInfo Properties

            public TransitionType Type { get => _type; }
            public Ease Curve { get => _curve; }
            public MyCurve GetMyCurve { get => _myCurve; }
            public float Duration { get => _duration; }

            #endregion
        }

        [Serializable] private class ScreenInfo
        {
            #region ScreenInfo Variables

            [SerializeField][Tooltip("Should be unique")] private string _id;
            [SerializeField][Tooltip("Should be unique")] private int _number;
            [SerializeField] private Screen _screen;

            #endregion

            #region ScreenInfo Properties

            public string ID { get => _id; }
            public int Number { get => _number; }
            public Screen ThisScreen { get => _screen; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private TransitionInfo _transitionInfo;
        [Space]
        [SerializeField] private ScreenInfo[] _screenInfos;
        [Space]
        [SerializeField] private string _startScreen;
        [SerializeField] private string _registrationScreenName;

        [Header("Variables")]
        private ScreenInfo _currentScreen;
        private State _state;

        #endregion

        #region Properties

        public float TransitionDuration { get => _transitionInfo.Duration; }
        public string StartScreen { get => _startScreen; }
        public string RegistrationScreenName { get => _registrationScreenName; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < _screenInfos.Length; i++)
            {
                _screenInfos[i].ThisScreen.Initialize();
            }
            _state = State.Standing;
            FastTransitionTo(_startScreen);
        }

        #endregion

        #region Control Methods

        public void TransitionTo(string id) => TransitionTo(id, false);
        public void TransitionTo(string id, bool isFast)
        {
            if (_state != State.Standing || _currentScreen?.ID == id) return;

            ScreenInfo screenInfo = GetScreenInfoByID(id);
            Screen screen = screenInfo.ThisScreen;

            if (screen != null)
            {
                _state = State.Transition;
                StartCoroutine(WaitToTransitionEnd(_currentScreen?.ThisScreen));

                bool isForth = _currentScreen?.Number < screenInfo.Number;

                screen.Transit(true, isForth, _transitionInfo.Type, isFast ? 0 : _transitionInfo.Duration, _transitionInfo.Curve, _transitionInfo.GetMyCurve);
                _currentScreen?.ThisScreen.Transit(false, isForth, _transitionInfo.Type, isFast ? 0 : _transitionInfo.Duration, _transitionInfo.Curve, _transitionInfo.GetMyCurve);
                _currentScreen = screenInfo;
            }
            else Debug.LogError($"[Kamen - ScreenManager] Screen with id \"{id}\" does not exist in the scene!");
        }
        public void TransitionWithOwnDuration(string id, float duration, Ease ease = Ease.Unset)
        {
            if (_state != State.Standing || _currentScreen?.ID == id) return;

            ScreenInfo screenInfo = GetScreenInfoByID(id);
            Screen screen = screenInfo.ThisScreen;

            if (screen != null)
            {
                _state = State.Transition;
                StartCoroutine(WaitToTransitionEnd(_currentScreen?.ThisScreen));

                bool isForth = _currentScreen?.Number < screenInfo.Number;

                screen.Transit(true, isForth, _transitionInfo.Type, duration, ease, _transitionInfo.GetMyCurve);
                _currentScreen?.ThisScreen.Transit(false, isForth, _transitionInfo.Type, duration, ease, _transitionInfo.GetMyCurve);
                _currentScreen = screenInfo;
            }
            else Debug.LogError($"[Kamen - ScreenManager] Screen with id \"{id}\" does not exist in the scene!");
        }
        public void FastTransitionTo(string id)
        {
            ScreenInfo screenInfo = GetScreenInfoByID(id);
            Screen screen = screenInfo.ThisScreen;

            if (screenInfo != null)
            {
                _currentScreen?.ThisScreen?.HideCanvasGroup();
                _currentScreen?.ThisScreen?.gameObject.SetActive(false);

                screen.ShowCanvasGroup();
                screen.gameObject.SetActive(true);

                _currentScreen = screenInfo;
            }
            else Debug.LogError($"[Kamen - ScreenManager] Screen with id \"{id}\" does not exist in the scene!");
        }
        private IEnumerator WaitToTransitionEnd(Screen oldScreen)
        {
            yield return new WaitForSeconds(_transitionInfo.Duration);
            _state = State.Standing;
            oldScreen?.HideCanvasGroup();
            oldScreen?.gameObject.SetActive(false);
        }

        #endregion

        #region Calculate Methods

        private ScreenInfo GetScreenInfoByID(string id)
        {
            for (int i = 0; i < _screenInfos.Length; i++)
            {
                if (id == _screenInfos[i].ID) return _screenInfos[i];
            }
            return null;
        }

        #endregion
    }
}