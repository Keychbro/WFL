using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Kamen.UI
{
    [RequireComponent(typeof(Toggle))]
    public class SwitchToggle : MonoBehaviour
    {
        #region Enums

        private enum AnimationType
        {
            None,
            Image,
            Color,
            ImageAndColor
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private AnimationType _type;
        [SerializeField] private SwitchToggleSettings _settings;
        [SerializeField] private SwitchToggleSprites _sprites;
        [SerializeField] private bool _isMoving;
        [Space]
        [SerializeField] private Image _background;
        [SerializeField] private Image _handle;

        [Header("Variables")]
        private Toggle _toggle;
        private RectTransform _handleRectTransform;
        private Vector2 _handlePosition;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }
        private void OnDestroy() => _toggle.onValueChanged.RemoveListener(OnSwitch);
 

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _toggle = gameObject.GetComponent<Toggle>();
            _handleRectTransform = _handle.GetComponent<RectTransform>();
            _handlePosition = _handleRectTransform.anchoredPosition;

            _toggle.onValueChanged.AddListener(OnSwitch);
            CheckSettings();
        }
        private void CheckSettings()
        {
            if (_settings == null) Debug.LogError($"[Kamen - SwitchToggle] Settings for switch togle named \"{gameObject.name}\" are not assigned");
            if ((_type == AnimationType.Image || _type == AnimationType.ImageAndColor) && _sprites == null) Debug.LogError($"[Kamen - SwitchToggle] Sprites for switch togle named \"{gameObject.name}\" are not assigned");
        }

        public virtual void OnSwitch(bool isOn)
        {
            switch (_type)
            {
                case AnimationType.Image:
                    DoChangeImage(isOn);
                    break;
                case AnimationType.Color:
                    DoChangeColor(isOn); 
                    break;
                case AnimationType.ImageAndColor:
                    DoChangeColor(isOn);
                    DoChangeImage(isOn);
                    break;
            }

            if (_isMoving) DoMove(isOn);   
        }

        #endregion

        #region Animation Methods

        private void DoMove(bool isOn)
        {
            if (_settings.GetMyCurve == null) _handleRectTransform.DOAnchorPos(isOn ? _handlePosition * -1 : _handlePosition, _settings.Duration).SetEase(_settings.Curve);
            else _handleRectTransform.DOAnchorPos(isOn ? _handlePosition * -1 : _handlePosition, _settings.Duration).SetEase(_settings.GetMyCurve.Value);
        }
        private void DoChangeImage(bool isOn)
        {
            _handle.sprite = isOn ? _sprites.HandleOnSprite : _sprites.HandleOffSprite;
        }
        private void DoChangeColor(bool isOn)
        {
            _background.DOColor(isOn ? _settings.BackgroundOnColor : _settings.BackgroundOffColor, _settings.Duration);
            _handle.DOColor(isOn ? _settings.HandleOnColor : _settings.HandleOffColor, _settings.Duration);
        }

        #endregion
    }
}