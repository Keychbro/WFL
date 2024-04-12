using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WOFL.BattlePass
{
    public class BattlePassBottomBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _title;

        [Header("Settings")]
        [SerializeField] private Color32 _emptyColor;
        [SerializeField] private Color32 _fillColor;

        #endregion

        #region Properties

        public void Fill()
        {
            _background.color = _fillColor;
        }

        #endregion
    }
}