using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.UI
{
    public class RequestResultView : MonoBehaviour
    {
        #region Enums

        public enum ResultType
        {
            None,
            Loading,
            Success,
            Failure,
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private LoadingIcon _loadingIcon;
        [SerializeField] private AnimateIcon _successIcon;
        [SerializeField] private AnimateIcon _failureIcon;

        [Header("Variables")]
        private AnimateIcon _currentIcon;
        private ResultType _currentType = ResultType.None;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _loadingIcon.gameObject.SetActive(false);
            _successIcon.gameObject.SetActive(false);
            _failureIcon.gameObject.SetActive(false);
        }

        #endregion

        #region Control Methods

        public void CallResult(ResultType newResult)
        {
            if (_currentIcon != null && newResult != _currentType) _currentIcon.CallDissapear();
            _currentType = newResult;

            switch (newResult)
            {
                case ResultType.Loading:
                    CallIconAppear(_loadingIcon);
                    break;
                case ResultType.Success:
                    CallIconAppear(_successIcon);
                    break;
                case ResultType.Failure:
                    CallIconAppear(_failureIcon);
                    break;
                case ResultType.None:
                    _currentIcon = null;
                    break;
            }
        }
        private void CallIconAppear(AnimateIcon icon)
        {
            icon.CallAppear();
            _currentIcon = icon;
        }

        #endregion
    }
}