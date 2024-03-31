using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WOFL.UI
{
    public class ProductPackBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _title;

        #endregion

        #region Properties

        public Image Background { get => _background; }
        public TextMeshProUGUI Title { get => _title; }

        #endregion

        #region Control Methods

        public void Initialize(string title)
        {
            _title.text = title;
        }

        #endregion
    }
}