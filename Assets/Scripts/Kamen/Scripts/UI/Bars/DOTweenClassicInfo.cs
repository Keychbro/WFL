using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.UI
{
    [Serializable] public class DOTweenClassicInfo
    {
        #region Variables

        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        #endregion

        #region Properties

        public float Duration { get => _duration; }
        public Ease Ease { get => _ease; }

        #endregion
    }
}