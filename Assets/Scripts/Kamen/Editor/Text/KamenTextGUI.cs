using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kamen
{
    [CustomPropertyDrawer(typeof(KamenText))]
    public class KamenTextGUI : PropertyDrawer
    {
        #region Properties

        private SerializedProperty _type;
        private SerializedProperty _legacyText;
        private SerializedProperty _tmpro3DText;
        private SerializedProperty _tmproUIText;

        #endregion

        #region GUI Methods

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            FindProperty(property);

            EditorGUI.BeginProperty(position, label, property);
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            EditorGUILayout.PropertyField(_type);
            switch (_type.enumValueIndex)
            {
                case 0:
                    EditorGUILayout.PropertyField(_legacyText);
                    break;
                case 1:
                    EditorGUILayout.PropertyField(_tmpro3DText);
                    break;
                case 2:
                    EditorGUILayout.PropertyField(_tmproUIText);
                    break;
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
        private void FindProperty(SerializedProperty property)
        {
            _type = property.FindPropertyRelative("_type");
            _legacyText = property.FindPropertyRelative("_legacyText");
            _tmpro3DText = property.FindPropertyRelative("_tmpro3DText");
            _tmproUIText = property.FindPropertyRelative("_tmproUIText");
        }

        #endregion
    }
}