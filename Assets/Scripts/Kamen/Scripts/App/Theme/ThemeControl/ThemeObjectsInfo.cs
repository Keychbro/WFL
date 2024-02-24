using System;
using UnityEngine;

namespace Kamen.Theme
{
    [Serializable] public class ThemeObjectsInfo
    {
        #region Enums

        public enum ObjectsType
        {
            Image,
            SVG,
            Text,
            TMProUGUI,
            TMPro
        }

        #endregion

        #region Classes 

        [Serializable] private struct Theme
        {
            #region Theme Variables

            [SerializeField] private string _id;
            [SerializeField] private Color32 _color;

            #endregion

            #region Theme Properties

            public string ID { get => _id; }
            public Color32 ThemeColor { get => _color; }

            #endregion
        }

        #endregion

        #region Variables
        //[Space(-10)]
        //[Header("Settings")]
        [SerializeField] private ObjectsType _objectsType;
        [SerializeField] private ImageThemeObjects _imageThemeObjects;
        [SerializeField] private SVGThemeObjects _svgThemeObjects;
        [SerializeField] private TextThemeObjects _textThemeObjects;
        [SerializeField] private TMProUGUIThemeObjects _tmproUGUIThemeObjects;
        [SerializeField] private TMProThemeObjects _tmproThemeObjects;
        [Space]
        [SerializeField] private Theme[] _themes;

        [Header("Variables")]
        private IThemeObjects _currentObjects;

        #endregion

        #region Control Methods

        public void Initialize()
        {
            _currentObjects = _objectsType switch
            {
                ObjectsType.Image => _imageThemeObjects,
                ObjectsType.SVG => _svgThemeObjects,
                ObjectsType.Text => _textThemeObjects,
                ObjectsType.TMProUGUI => _tmproUGUIThemeObjects,
                ObjectsType.TMPro => _tmproThemeObjects,
                _ => null
            };
        }
        public void ChangeTheme(string id, float duration)
        {
            Theme newTheme = GetThemeByID(id);
            _currentObjects.ChangeColor(newTheme.ThemeColor, duration);
        }

        #endregion

        #region Calculate Methods

        private Theme GetThemeByID(string id)
        {
            for (int i = 0; i < _themes.Length; i++)
            {
                if (id == _themes[i].ID) return _themes[i];
            }
        
            Debug.LogError($"[Kamen - ThemeObject] Theme with id \"{id}\" does not exist for this object!");
        
            return new Theme();
        }

        #endregion
    }
}