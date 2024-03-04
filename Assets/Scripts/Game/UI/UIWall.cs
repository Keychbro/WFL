using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WOFL.UI
{
    public class UIWall : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;

        [Header("Settings")]
        [SerializeField][Range(0f, 1f)] private float _activateValue;
        [SerializeField] private float _switchDuration;
        [SerializeField] private Ease _switchEase;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, _activateValue);
        }

        #endregion

        #region Control Methods

        public void Switch(bool isActive)
        {
            if (isActive) Activate();
            else Deactivate();
        }
        private void Activate()
        {
            gameObject.SetActive(true);
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 0);
            _background.DOFade(_activateValue, _switchDuration).SetEase(_switchEase);
        }
        private void Deactivate()
        {
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, _activateValue);
            _background.DOFade(0, _switchDuration).SetEase(_switchEase).OnComplete(() => 
            {
                gameObject.SetActive(false);
            });
        }

        #endregion
    }
}