using UnityEngine;
using DG.Tweening;

namespace Kamen.UI
{
    [CreateAssetMenu(fileName = "SwitchToggleSettings", menuName = "Kamen/Settings for components/SwitchToggleSettings", order = 1)]
    public class SwitchToggleSettings : ScriptableObject
    {
        #region Variables

        [Header("Default settings")]
        [SerializeField] private Color _backgroundOffColor;
        [SerializeField] private Color _handleOffColor;

        [Header("Active settings")]
        [SerializeField] private Color _backgroundOnColor;
        [SerializeField] private Color _handleOnColor;

        [Header("Animation settings")]
        [SerializeField] private float _duration;
        [SerializeField] private Ease _curve;
        [SerializeField] private MyCurve _myCurve;

        #endregion

        #region Properties

        public Color BackgroundOffColor { get => _backgroundOffColor; }
        public Color HandleOffColor { get => _handleOffColor; }
        public Color BackgroundOnColor { get => _backgroundOnColor; }
        public Color HandleOnColor { get => _handleOnColor; }
        public float Duration { get => _duration; }
        public Ease Curve { get => _curve; }
        public MyCurve GetMyCurve{ get  => _myCurve; }

        #endregion
    }
}
