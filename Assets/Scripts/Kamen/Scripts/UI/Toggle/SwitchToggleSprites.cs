using DG.Tweening;
using UnityEngine;

namespace Kamen.UI
{
    [CreateAssetMenu(fileName = "SwitchToggleSprites", menuName = "Kamen/Settings for components/SwitchToggleSprites", order = 1)]
    public class SwitchToggleSprites : ScriptableObject
    {
        #region Variables

        [Header("Sprites")]
        [SerializeField] private Sprite _handleOffSprite;
        [SerializeField] private Sprite _handleOnSprite;

        #endregion

        #region Properties

        public Sprite HandleOffSprite { get => _handleOffSprite; }
        public Sprite HandleOnSprite { get => _handleOnSprite; }

        #endregion
    }
}