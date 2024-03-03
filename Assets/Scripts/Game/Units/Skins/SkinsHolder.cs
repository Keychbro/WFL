using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Skins Holder", menuName = "WOFL/Settings/Units/Skins Holder", order = 1)]
    public class SkinsHolder : ScriptableObject
    {
        #region Variables

        [SerializeField] private Skin[] _skins;

        #endregion

        #region Properties

        public Skin[] Skins { get => _skins; }

        #endregion
    }
}