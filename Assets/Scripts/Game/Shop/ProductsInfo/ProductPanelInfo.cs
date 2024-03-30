using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WOFL.Settings
{
    public class ProductPanelInfo : ScriptableObject
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _name;
        [Space]
        [SerializeField] private Sprite _icon;
        [SerializeField] private Vector2 _iconSize;
        [SerializeField] private Vector3 _iconPosition;


        #endregion
    }
}