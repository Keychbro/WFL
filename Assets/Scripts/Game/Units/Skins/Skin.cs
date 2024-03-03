using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "Skin", menuName = "WOFL/Settings/Units/Skin", order = 1)]
    public class Skin : ScriptableObject
    {
        #region Variables

        [SerializeField] private string _uniqueName;
        [SerializeField] private Sprite _skinSprite;
        [SerializeField] private UnlockStatus _unlockStatus;

        #endregion

        #region Properties

        public string UniqueName { get => _uniqueName; }
        public Sprite SkinSprite { get => _skinSprite; }
        public UnlockStatus UnlockStatus { get => _unlockStatus; }

        #endregion
    }
}