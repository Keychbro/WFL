using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using WOFL.Settings;
using System;
using System.Linq;

namespace WOFL.Game
{
    public class GameSideManager : SingletonComponent<GameSideManager>
    {
        #region Classes

        [Serializable] private class HealthBarSettingsInfo
        {
            #region Variables

            [SerializeField] private IDamageable.GameSideName _sideName;
            [SerializeField] private HealthBarSettings _healthBarSettings;

            #endregion

            #region Properties

            public IDamageable.GameSideName SideName { get => _sideName; }
            public HealthBarSettings HealthBarSettings { get =>  _healthBarSettings; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("HealthBar Settings")]
        [SerializeField] private HealthBarSettingsInfo[] _healthBarSettingsInfo;

        #endregion

        #region Control Methods

        public HealthBarSettings GetHealthBarSettingsInfoByName(IDamageable.GameSideName sideName) => _healthBarSettingsInfo.First(healthBarSettingsInfo => healthBarSettingsInfo.SideName == sideName).HealthBarSettings;

        #endregion
    }
}

