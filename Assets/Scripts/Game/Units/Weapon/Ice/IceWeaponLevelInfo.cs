using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Ice Weapon Level Info", menuName = "WOFL/Settings/Units/Ice Weapon Level Info", order = 1)]
    public class IceWeaponLevelInfo : WeaponLevelInfo
    {
        #region Variables

        [SerializeField][Range(0f, 1f)] private float _slowdownIncreaseValue;
        [SerializeField] private float _slowdownIncreaseDuration;

        #endregion

        #region Properties

        public float SlowdownIncreaseValue { get => _slowdownIncreaseValue; }
        public float SlowdownIncreaseDuration { get => _slowdownIncreaseDuration; }

        #endregion 
    }
}