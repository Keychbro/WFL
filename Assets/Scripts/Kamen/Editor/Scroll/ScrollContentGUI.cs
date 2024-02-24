using UnityEditor;
using UnityEngine;
using static Kamen.UI.ScrollContent;

namespace Kamen.UI
{
    [CustomEditor(typeof(ScrollContent))]
    public class ScrollContentGUI : Editor
    {
        #region Properties

        private SerializedProperty _currentCanvas;
        private SerializedProperty _rectContent;
        private SerializedProperty _rectChildren;
        private SerializedProperty _itemSpacing;
        private SerializedProperty _type;
        private SerializedProperty _spawnDirection;
        private SerializedProperty _horizontalMargin;
        private SerializedProperty _verticalMargin;
        private SerializedProperty _isManualConfiguration;
        private SerializedProperty _isUseMiddleForStartPosition;

        #endregion

        #region GUI Methods

        protected virtual void OnEnable()
        {
            _currentCanvas = serializedObject.FindProperty("_currentCanvas");
            _rectContent = serializedObject.FindProperty("_rectContent");
            _rectChildren = serializedObject.FindProperty("_rectChildren");
            _itemSpacing = serializedObject.FindProperty("_itemSpacing");
            _type = serializedObject.FindProperty("_type");
            _spawnDirection = serializedObject.FindProperty("_spawnDirection");
            _horizontalMargin = serializedObject.FindProperty("_horizontalMargin");
            _verticalMargin = serializedObject.FindProperty("_verticalMargin");
            _isManualConfiguration = serializedObject.FindProperty("_isManualConfiguration");
            _isUseMiddleForStartPosition = serializedObject.FindProperty("_isUseMiddleForStartPosition");
        }
        public override void OnInspectorGUI()
        {
            ScrollContent scrollContent = (ScrollContent)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_currentCanvas);
            EditorGUILayout.PropertyField(_rectContent);
            EditorGUILayout.PropertyField(_rectChildren);
            EditorGUILayout.PropertyField(_itemSpacing);
            EditorGUILayout.PropertyField(_type);
            EditorGUILayout.PropertyField(_spawnDirection);
            EditorGUILayout.PropertyField(_isUseMiddleForStartPosition);

            switch (scrollContent.Type)
            {
                case ScrollContent.ScrollType.Horizontal:
                    EditorGUILayout.PropertyField(_horizontalMargin);
                    break;
                case ScrollContent.ScrollType.Vertical:
                    EditorGUILayout.PropertyField(_verticalMargin);
                    break;
            }

            EditorGUILayout.PropertyField(_isManualConfiguration);
            serializedObject.ApplyModifiedProperties();
            if (_isManualConfiguration.boolValue)
            {
                if (GUILayout.Button("Set Children Positions")) scrollContent.ManualInitialize();
            }
        }

        #endregion
    }
}