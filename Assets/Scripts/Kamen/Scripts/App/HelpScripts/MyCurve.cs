using UnityEngine;

namespace Kamen
{
    [CreateAssetMenu(fileName = "Curve", menuName = "Kamen/Additions/Curve", order = 1)]
    public class MyCurve : ScriptableObject
    {
        #region Inspector Variables

        [SerializeField] private AnimationCurve _curve;

        #endregion

        #region Properties

        public AnimationCurve Value { get => _curve; }

        #endregion
    }
}

