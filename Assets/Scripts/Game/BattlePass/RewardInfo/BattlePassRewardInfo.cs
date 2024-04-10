using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    public abstract class BattlePassRewardInfo : ScriptableObject
    {
        #region Variables
        
        [Header("Settings")]
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected int _value;
        
        #endregion
        
        #region Properties
        
        public Sprite Icon { get => _icon; }
        public int Value { get => _value; }
        
        #endregion
    }
}