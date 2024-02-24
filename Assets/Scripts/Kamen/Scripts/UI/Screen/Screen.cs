using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Kamen.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Screen : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]
        private CanvasGroup _canvasGroup;

        #endregion

        #region Control Methods

        public virtual void Initialize()
        {
            _canvasGroup = gameObject.GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;

            gameObject.SetActive(false);
        }
        public void HideCanvasGroup() => _canvasGroup.alpha = 0f;
        public void ShowCanvasGroup() => _canvasGroup.alpha = 1f;
        public virtual void Transit(bool isShow, bool isForth, ScreenManager.TransitionType type, float duration, Ease curve, MyCurve myCurve)
        {
            gameObject.SetActive(true);

            switch (type)
            {
                case ScreenManager.TransitionType.None:
                    break;
                case ScreenManager.TransitionType.Fade:
                    DoFade(isShow, duration, curve, myCurve);
                    break;
                case ScreenManager.TransitionType.SwipeDown:
                    if (isForth) VerticalSwipe(isShow, true, duration, curve, myCurve);
                    else VerticalSwipe(isShow, false, duration, curve, myCurve);
                    break;
                case ScreenManager.TransitionType.SwipeUp:
                    if (isForth) VerticalSwipe(isShow, false, duration, curve, myCurve);
                    else VerticalSwipe(isShow, true, duration, curve, myCurve);
                    break;
                case ScreenManager.TransitionType.SwipeRight:
                    if (isForth) HorizontalSwipe(isShow, true, duration, curve, myCurve);
                    else HorizontalSwipe(isShow, false, duration, curve, myCurve);
                    break;
                case ScreenManager.TransitionType.SwipeLeft:
                    if (isForth) HorizontalSwipe(isShow, false, duration, curve, myCurve);
                    else HorizontalSwipe(isShow, true, duration, curve, myCurve);
                    break;
            }

            if (isShow && type != ScreenManager.TransitionType.Fade) _canvasGroup.alpha = 1f;
        }

        #endregion

        #region Transition Methods

        private void DoFade(bool isShow, float duration, Ease curve, MyCurve myCurve)
        {
            float endFadeValue = isShow ? 1f : 0f;

            if (myCurve == null) _canvasGroup.DOFade(endFadeValue, duration).SetEase(curve);
            else _canvasGroup.DOFade(endFadeValue, duration).SetEase(myCurve.Value);
        }
        private void VerticalSwipe(bool isShow, bool isToDown, float duration, Ease curve, MyCurve myCurve)
        {
            int direction = isToDown ? 1 : -1;
            float screenEdge = CanvasSettings.Instance.GetCurrentScreenSize().y;

            Vector3 startPosition = isShow ? new Vector3(0f, screenEdge * direction, 0f) : Vector3.zero;
            Vector3 endPosition = isShow ? Vector3.zero : new Vector3(0f, screenEdge * -direction, 0f);

            DoSwipe(duration, curve, myCurve, startPosition, endPosition);
        }
        private void HorizontalSwipe(bool isShow, bool isToRight, float duration, Ease curve, MyCurve myCurve)
        {
            int direction = isToRight ? 1 : -1;
            float screenEdge = CanvasSettings.Instance.GetCurrentScreenSize().x;

            Vector3 startPosition = isShow ? new Vector3(screenEdge * -direction, 0f, 0f) : Vector3.zero;
            Vector3 endPosition = isShow ? Vector3.zero : new Vector3(screenEdge * direction, 0f, 0f);

            DoSwipe(duration, curve, myCurve, startPosition, endPosition);
        }
        private void DoSwipe(float duration, Ease curve, MyCurve myCurve, Vector3 startPosition, Vector3 endPosition)
        {
            transform.localPosition = startPosition;

            if (myCurve == null) transform.DOLocalMove(endPosition, duration).SetEase(curve);
            else transform.DOLocalMove(endPosition, duration).SetEase(myCurve.Value);
        }

        #endregion
    }
}