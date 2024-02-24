using System;
using UnityEngine;

namespace Kamen.Theme
{
    public interface IThemeObjects
    {
        #region Methods

        public void ChangeColor(Color32 newColor, float duration = 0);

        #endregion
    }
}