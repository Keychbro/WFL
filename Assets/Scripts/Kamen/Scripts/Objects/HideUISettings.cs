using System;
using DG.Tweening;
using UnityEngine;

namespace Kamen.Settings
{
    [CreateAssetMenu(fileName = "HideUI Settings", menuName = "Kamen/Settings/HideUI Settings", order = 1)]
    public class HideUISettings : ScriptableObject
    {
        #region Variables

        [SerializeField][Range(0, 1)] private float _value;
        [SerializeField] private float _duration;
        [SerializeField] private float _delay;
        [SerializeField] private Ease _ease;
        [SerializeField] private bool _isDeactivate;

        #endregion

        #region Properties

        public float Value { get => _value; }
        public float Duration { get => _duration; }
        public float Delay { get => _delay; }
        public Ease Ease { get => _ease; }
        public bool IsDeactivate { get => _isDeactivate; }

        #endregion
    }
}