using UnityEditor;
using UnityEngine;
using static Kamen.Theme.ThemeObjectsInfo;

namespace Kamen.Theme
{
    [CustomPropertyDrawer(typeof(ThemeObjectsInfo))]
    public class ThemeObjectsInfoGUI : PropertyDrawer
    {
        #region Variables

        [Header("Constants")]
        private const float _lineHeight = 20;
        private const float _additionalHeight = 10;
        private const int _startLinesAmount = 3;
        private const int _linesInTheme = 2;

        [Header("Variables")]
        private SerializedProperty _objectsType;
        private SerializedProperty _objectsProperty;
        private SerializedProperty _themes;

        #endregion

        #region GUI Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _objectsType = property.FindPropertyRelative("_objectsType");
            _objectsProperty = InitializeObjectTheme(property);
            _themes = property.FindPropertyRelative("_themes");

            EditorGUI.BeginProperty(position, label, property);
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect firstRect = new Rect(position.x, position.y + 20, position.width, position.height);
            Rect secondRect = new Rect(position.x, position.y + 32 + (_objectsProperty.isExpanded ? CalculateHeightForArrays(_objectsProperty.arraySize == 0 ? 1 : _objectsProperty.arraySize) : 0), position.width, position.height);

            EditorGUI.PropertyField(position, _objectsType, true);
            EditorGUI.PropertyField(firstRect, _objectsProperty, true);
            EditorGUI.PropertyField(secondRect, _themes, true);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int additionlObjectsLines = GetObjectsAmount(property);
            int additionlThemesLines = GetThemesAmount(property);
            float height = _startLinesAmount * _lineHeight;

            if (_objectsProperty.isExpanded) height += CalculateHeightForArrays(additionlObjectsLines);
            if (_themes.isExpanded) height += CalculateHeightForArrays(additionlThemesLines);

            return height;
        }

        #endregion

        #region Calculate Methods

        private int GetObjectsAmount(SerializedProperty property)
        {
            _objectsType = property.FindPropertyRelative("_objectsType");
            _objectsProperty = InitializeObjectTheme(property);

            return _objectsProperty.arraySize == 0 ? 1 : _objectsProperty.arraySize;
        }
        private int GetThemesAmount(SerializedProperty property)
        {
            _themes = property.FindPropertyRelative("_themes");
            int lines = _themes.arraySize == 0 ? 1 : _themes.arraySize;
            for (int i = 0; i < _themes.arraySize; i++)
            {
                if (_themes.GetArrayElementAtIndex(i).isExpanded) lines += _linesInTheme;
            }
            return lines;
        }
        private SerializedProperty InitializeObjectTheme(SerializedProperty property)
        {
            return (ObjectsType)_objectsType.intValue switch
            {
                ObjectsType.Image => GetThemeObjectsProperty(property, "_imageThemeObjects", "_images"),
                ObjectsType.SVG => GetThemeObjectsProperty(property, "_svgThemeObjects", "_images"),
                ObjectsType.Text => GetThemeObjectsProperty(property, "_textThemeObjects", "_texts"),
                ObjectsType.TMProUGUI => GetThemeObjectsProperty(property, "_tmproUGUIThemeObjects", "_texts"),
                ObjectsType.TMPro => GetThemeObjectsProperty(property, "_tmproThemeObjects", "_texts"),
                _ => GetThemeObjectsProperty(property, "_imageThemeObjects", "_images")
            };
        }
        private SerializedProperty GetThemeObjectsProperty(SerializedProperty property, string name, string type) => property.FindPropertyRelative(name).FindPropertyRelative(type);
        private float CalculateHeightForArrays(int lines) => (lines + 1) * _lineHeight + _additionalHeight;

        #endregion
    }
}