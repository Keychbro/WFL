using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Quick Bar Button Info", menuName = "WOFL/Settings/UI/Quick Bar Button Info", order = 1)]
    public class QuickBarButtonInfo : ScriptableObject
    {
        #region Enums

        public enum ButtonState
        {
            Disabling,
            Enabling,
            Selecting,
        }

        #endregion

        #region Classes 

        [Serializable] public struct ButtonViewInfo
        {
            #region ButtonView Variables

            [SerializeField] private Color32 _mainColor;
            [SerializeField] private Color32 _backgroundColor;
            [SerializeField] private float _animationDuration;
            [SerializeField] private Ease _animationEase;
            [SerializeField] private IndicatorInfo _indicator;

            #endregion

            #region ButtonView Properties

            public Color32 MainColor { get => _mainColor; }
            public Color32 Background { get => _backgroundColor; }
            public float AnimationDuration { get => _animationDuration; }
            public Ease AnimationEase { get => _animationEase; }
            public IndicatorInfo IndicatorObject { get => _indicator; }

            #endregion
        }
        [Serializable] public struct IndicatorInfo
        {
            #region IndicatorInfo Variables

            [SerializeField] private Vector3 _objectScale;
            [SerializeField] private float _animationDuration;
            [SerializeField] private Ease _animationEase;

            #endregion

            #region IndicatorInfo Properties

            public Vector3 ObjectScale { get => _objectScale; }
            public float AnimationDuration { get => _animationDuration; }
            public Ease AnimationEase { get => _animationEase; }

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private ButtonState _state;
        [SerializeField] private ButtonViewInfo _viewInfo;

        #endregion

        #region Properties

        public ButtonState State { get => _state; }
        public ButtonViewInfo ViewInfo { get => _viewInfo; }

        #endregion
    }
}

