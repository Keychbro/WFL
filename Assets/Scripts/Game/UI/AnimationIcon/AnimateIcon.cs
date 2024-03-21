using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.UI
{
    [RequireComponent(typeof(Image))]
    public class AnimateIcon : MonoBehaviour
    {
        #region Variables

        [Header("Appear Settings")]
        [SerializeField] protected bool _isUseSmoothAppear;
        [Space]
        [SerializeField] protected float _smoothAppearDuration;
        [SerializeField] protected Ease _smoothAppearEase;

        [Header("Dissapear Settings")]
        [SerializeField] protected bool _isUseSmoothDissapear;
        [Space]
        [SerializeField] protected float _smoothDissapearDuration;
        [SerializeField] protected Ease _smoothDissapearEase;

        [Header("Variables")]
        protected Image _iconView;
        private bool _isAppearing;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _iconView = GetComponent<Image>();
        }

        #endregion

        #region Control Methods

        public virtual void CallAppear()
        {
            if (_iconView == null) _iconView = GetComponent<Image>();

            _isAppearing = true;
            _iconView.DOKill();
            _iconView.gameObject.SetActive(true);
            if (_isUseSmoothAppear)
            {
                _iconView.transform.localScale = Vector3.zero;
                _iconView.transform.DOScale(Vector3.one, _smoothAppearDuration).SetEase(_smoothAppearEase);
            }
        }
        public virtual void CallDissapear()
        {
            _isAppearing = false;
            _iconView.DOKill();
            if (_isUseSmoothDissapear)
            {
                _iconView.transform.localScale = Vector3.one;
                _iconView.transform.DOScale(Vector3.zero, _smoothAppearDuration).SetEase(_smoothAppearEase).OnComplete(() =>
                {
                    if (!_isAppearing) _iconView.gameObject.SetActive(false);
                });
            }
            else _iconView.gameObject.SetActive(false);
        }

        #endregion
    }
}