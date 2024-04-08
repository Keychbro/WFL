using UnityEngine;
using UnityEngine.UI;

namespace WOFL.UI
{
    [CreateAssetMenu(fileName = "Battle Pass Reward info", menuName = "WOFL/BattlePass/Battle Pass Reward info", order = 1)]
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