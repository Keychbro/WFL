using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kamen;

namespace WOFL.BattlePass
{
    public class BattlePassTopBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _amountCompletedPoints;
        [SerializeField] private TimerViewer _timerViewer;

        #endregion
    }
}