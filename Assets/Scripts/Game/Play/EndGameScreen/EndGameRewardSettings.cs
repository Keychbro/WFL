using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    [CreateAssetMenu(fileName = "End Game Reward Settings", menuName = "WOFL/Game/Settings/End Game Reward Settings", order = 1)]
    public class EndGameRewardSettings : ScriptableObject
    {
        #region Enums

        public enum EndGameRewardType
        {
            Gold,
            Diamonds,
            DiamondsWithPass,
            Tools,
            ChestClassic1,
            ChestClassic2
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private EndGameRewardType _rewardType;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector2 _iconSize;
        [SerializeField] private Vector3 _iconPosition;
        [SerializeField] private bool _isDiamondPass;
        [Space]
        [SerializeField] private Color32 _textColor;

        #endregion

        #region Properties

        public EndGameRewardType RewardType { get => _rewardType; }
        public Sprite Icon { get => _icon; }
        public Vector2 IconSize { get => _iconSize; }
        public Vector3 IconPosition { get => _iconPosition; }
        public bool IsDiamondPass { get => _isDiamondPass; }
        public Color32 TextColor { get => _textColor; }

        #endregion
    }
}
