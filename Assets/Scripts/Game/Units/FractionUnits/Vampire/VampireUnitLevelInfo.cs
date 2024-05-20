using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Vampire Unit Level Info", menuName = "WOFL/Settings/Units/Vampire Unit Level Info", order = 1)]
    public class VampireUnitLevelInfo : UnitLevelInfo
    {
        #region Variables

        [Header("Vampire Settings")]
        [SerializeField][Range(0f, 1f)] private float _lifestealValue;

        #endregion

        #region Propeties

        public float LifestealValue { get => _lifestealValue; }

        #endregion
    }
}