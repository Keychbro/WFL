using System;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Kamen.Theme
{
    [Serializable] public class TMProThemeObjects : IThemeObjects
    {
        #region Variables

        [SerializeField] private TextMeshPro[] _texts;

        #endregion

        #region Methods

        public void ChangeColor(Color32 newColor, float duration = 0)
        {
            for (int i = 0; i < _texts.Length; i++)
            {
                _texts[i].DOColor(newColor, duration);
            }
        }

        #endregion
    }
}