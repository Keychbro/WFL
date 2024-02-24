using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Kamen.Theme
{
    [Serializable] public class ImageThemeObjects : IThemeObjects
    {
        #region Variables

        [SerializeField] private Image[] _images;

        #endregion

        #region Methods
        public void ChangeColor(Color32 newColor, float duration = 0)
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].DOColor(newColor, duration);
            }
        }

        #endregion
    }
}