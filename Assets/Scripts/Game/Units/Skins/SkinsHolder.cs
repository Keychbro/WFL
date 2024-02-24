using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "SkinsHolder", menuName = "WOFL/Settings/SkinsHolder", order = 1)]
    public class SkinsHolder : ScriptableObject
    {
        #region Variables

        [SerializeField] private Skin[] _skins;

        #endregion
    }
}