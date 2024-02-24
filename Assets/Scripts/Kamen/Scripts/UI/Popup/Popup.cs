using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Kamen.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Popup : MonoBehaviour
    {
        #region Enums

        protected enum AnimationType
        {
            Fade,
            Zoom,
            SwipeDown,
            SwipeUp,
            SwipeRight,
            SwipeLeft
        }
        private enum State
        {
            Shown,
            Hidden,
            Showing,
            Hidding
        }

        #endregion

        #region Classes

        [Serializable] protected class AnimationInfo
        {
            #region AnimationInfo Variables

            [SerializeField] private AnimationType _type;
            [SerializeField] private Ease _curve;
            [SerializeField] private MyCurve _myCurve;
            [SerializeField] private float _duration;

            #endregion

            #region AnimationInfo Properties

            public AnimationType Type { get => _type; }
            public Ease Curve { get => _curve; }
            public MyCurve GetMyCurve { get => _myCurve; }
            public float Duration { get => _duration; }

            #endregion
        }
        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] protected AnimationInfo _showAnimation;
        [SerializeField] protected AnimationInfo _hideAnimation;
        [SerializeField] protected bool _isOnlyBackgroundFade;
        [Space]
        [SerializeField] protected RectTransform _container;
        [SerializeField] protected Image _background;

        [Header("Variables")]
        private State _state;
        private CanvasGroup _canvasGroup;
        private Vector3 _rightPosition;
        private float _backgroundAlpha;

        #endregion

        #region Control Methods

        public virtual void Initialize()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            _state = State.Hidden;
            _rightPosition = transform.localPosition;
            _backgroundAlpha = _background.color.a;

            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            if (_state != State.Hidden) return;

            _state = State.Showing;
            gameObject.SetActive(true);

            StartCoroutine(ShowingCoroutine());
        }

        private IEnumerator ShowingCoroutine()
        {
            bool isFadeWithCurve = false;
            switch (_showAnimation.Type)
            {
                case AnimationType.Fade:
                    isFadeWithCurve = true;
                    break;
                case AnimationType.Zoom:
                    DoZoom(_showAnimation, true);
                    break;
                case AnimationType.SwipeDown:
                    VerticalSwipe(_showAnimation, true, true);
                    break;
                case AnimationType.SwipeUp:
                    VerticalSwipe(_showAnimation, true, false);
                    break;
                case AnimationType.SwipeRight:
                    HorizontalSwipe(_showAnimation, true, true);
                    break;
                case AnimationType.SwipeLeft:
                    HorizontalSwipe(_showAnimation, true, false);
                    break;
            }
            if (!_isOnlyBackgroundFade) DoFade(_showAnimation, true, isFadeWithCurve);
            else BackgroundFade(_showAnimation, true);
            
            yield return new WaitForSeconds(_showAnimation.Duration);
            _state = State.Shown;
        }

        public virtual void Hide()
        {
            if (_state != State.Shown) return;
            _state = State.Hidding;

            StartCoroutine(HiddingCoroutine());
        }
        private IEnumerator HiddingCoroutine()
        {
            bool isFadeWithCurve = false;
            switch (_hideAnimation.Type)
            {
                case AnimationType.Fade:
                    isFadeWithCurve = true;
                    break;
                case AnimationType.Zoom:
                    DoZoom(_hideAnimation, false);
                    break;
                case AnimationType.SwipeDown:
                    VerticalSwipe(_hideAnimation, false, true);
                    break;
                case AnimationType.SwipeUp:
                    VerticalSwipe(_hideAnimation, false, false);
                    break;
                case AnimationType.SwipeRight:
                    HorizontalSwipe(_hideAnimation, false, true);
                    break;
                case AnimationType.SwipeLeft:
                    HorizontalSwipe(_hideAnimation, false, false);
                    break;
            }
            if (!_isOnlyBackgroundFade) DoFade(_hideAnimation, false, isFadeWithCurve);
            else BackgroundFade(_hideAnimation, false);

            yield return new WaitForSeconds(_showAnimation.Duration);
            if (_isOnlyBackgroundFade) _canvasGroup.alpha = 0f;
            _state = State.Hidden;
            gameObject.SetActive(false);
        }

        #endregion

        #region Animation Methods

        private void DoFade(AnimationInfo info, bool isShow, bool isWithCurve)
        {
            float endFadeValue = isShow ? 1f : 0f;

            if (isWithCurve)
            {
                _container.transform.localPosition = _rightPosition;
                if (info.GetMyCurve == null) _canvasGroup.DOFade(endFadeValue, info.Duration).SetEase(info.Curve);
                else _canvasGroup.DOFade(endFadeValue, info.Duration).SetEase(info.GetMyCurve.Value);
            }
            else _canvasGroup.DOFade(endFadeValue, info.Duration);
        }

        private void DoZoom(AnimationInfo info, bool isShow) 
        {
            float startZoomValue = isShow ? 0f : 1f;
            float endZoomValue = isShow ? 1f : 0f;

            _container.transform.localPosition = _rightPosition;
            _container.transform.localScale = new Vector3(startZoomValue, startZoomValue, startZoomValue);

            if (info.GetMyCurve == null) _container.DOScale(endZoomValue, info.Duration).SetEase(info.Curve);
            else _container.DOScale(endZoomValue, info.Duration).SetEase(info.GetMyCurve.Value);
        }

        private void VerticalSwipe(AnimationInfo info, bool isShow, bool isToDown) 
        {
            int direction = isToDown ? 1 : -1;
            float screenEdge = CanvasSettings.Instance.GetCurrentReferenceResolution().y / 2f + _container.sizeDelta.y / 2f;

            Vector3 startPosition = isShow ? new Vector3(0f, screenEdge * direction, 0f) : _rightPosition;
            Vector3 endPosition = isShow ? _rightPosition : new Vector3(0f, screenEdge * -direction, 0f);

            DoSwipe(info, startPosition, endPosition);
        }
        private void HorizontalSwipe(AnimationInfo info, bool isShow, bool isToRight) 
        {
            int direction = isToRight ? 1 : -1;
            float screenEdge = CanvasSettings.Instance.GetCurrentReferenceResolution().x / 2f + _container.sizeDelta.x / 2f;

            Vector3 startPosition = isShow ? new Vector3(screenEdge * -direction, 0f, 0f) : _rightPosition;
            Vector3 endPosition = isShow ? _rightPosition : new Vector3(screenEdge * direction, 0f, 0f);

            DoSwipe(info, startPosition, endPosition);
        }
        private void DoSwipe(AnimationInfo info, Vector3 startPosition, Vector3 endPosition)
        {
            _container.transform.localPosition = startPosition;

            if (info.GetMyCurve == null) _container.DOLocalMove(endPosition, info.Duration).SetEase(info.Curve);
            else _container.DOLocalMove(endPosition, info.Duration).SetEase(info.GetMyCurve.Value);
        }

        private void BackgroundFade(AnimationInfo info, bool isShow)
        {
            _canvasGroup.alpha = 1f;
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, isShow ? 0f : _backgroundAlpha);
            _background.DOFade(isShow ? _backgroundAlpha : 0f, info.Duration);
        }

        #endregion
    }
}