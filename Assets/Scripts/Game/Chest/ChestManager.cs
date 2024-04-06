using Kamen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Control
{
    public class ChestManager : SingletonComponent<ChestManager>
    {
        #region Enums

        public enum ChestType
        {
            Classic1,
            Classic2,
            VIP1,
            VIP2,
            VIP3
        }

        #endregion

        #region Classes

        [Serializable] private class ChestInfo
        {
            #region ChestInfo Variables

            [SerializeField] private ChestType _type;

            #endregion

            #region ChestInfo Properties

            public ChestType Type { get => _type; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private ChestInfo[] _chestInfos;

        #endregion

        #region Control Methods

        

        #endregion
    }
}