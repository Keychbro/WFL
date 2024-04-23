using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Health Bar Settings", menuName = "WOFL/Settings/Units/Components/Health Bar Settings", order = 1)]
    public class HealthBarSettings : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private Sprite _instantFillSprite;
        [SerializeField] private Sprite _animationFillSprite;
        [Space]
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector2 _iconSize;
        [SerializeField] private Vector3 _iconPosition;
        [SerializeField] private Color32 _mainColor;

        #endregion

        #region Properties

        public Sprite InstantFillSprite { get => _instantFillSprite; }
        public Sprite AnimationFillSprite { get => _animationFillSprite; }

        public Sprite Icon { get => _icon; }
        public Vector2 IconSize { get => _iconSize; }
        public Vector3 IconPosition { get => _iconPosition; }
        public Color32 MainColor { get => _mainColor; }

        #endregion
    }
}