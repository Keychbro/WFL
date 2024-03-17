using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Player Profile Settings", menuName = "WOFL/Settings/Player/Player Profile Settings", order = 1)]
    public class PlayerProfileSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private Sprite[] _iconsList;

        #endregion

        #region Properties

        public Sprite[] IconsList { get => _iconsList; }

        #endregion
    }
}