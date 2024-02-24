using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Kamen 
{
    public class AppManager : SingletonComponent<AppManager>
    {
        #region Variables

        [Header("Settigs")]
        [SerializeField] private int _frameInGame;

        [Header("Settings for transitions")]
        [SerializeField] private Image _fadeWall;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private Ease _fadeEase;
        private static bool _isAfterRestart;

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            SetTargetFrameInApp();
            if (_isAfterRestart) FadeOut();
            else _fadeWall.gameObject.SetActive(false);
        }

        #endregion

        #region Control Methods

        private void SetTargetFrameInApp()
        {
            Application.targetFrameRate = _frameInGame;
        }
        public void ReloadScene()
        {
            _fadeWall.gameObject.SetActive(true);
            _fadeWall.color = new Color(_fadeWall.color.r, _fadeWall.color.g, _fadeWall.color.b, 0);
            _fadeWall.DOFade(1, _fadeDuration).SetEase(_fadeEase).OnComplete(() =>
            {
                _isAfterRestart = true;
                SceneManager.LoadScene(0);
            });
        }
        public void FadeOut()
        {
            _fadeWall.color = new Color(_fadeWall.color.r, _fadeWall.color.g, _fadeWall.color.b, 1);
            _fadeWall.DOFade(0, _fadeDuration).SetEase(_fadeEase).OnComplete(() =>
            {
                _fadeWall.gameObject.SetActive(false);
            });
        }

        #endregion
    }
}