using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Kamen.Theme
{
    [Serializable] public class TextThemeObjects : IThemeObjects
    {
        #region Variables

        [SerializeField] private Text[] _texts;

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