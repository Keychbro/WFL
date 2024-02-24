using UnityEditor;
using UnityEngine;

namespace Kamen.UI 
{
    [CustomEditor(typeof(InfiniteScroll))]
    public class InfiniteScrollGUI : Editor
    {
        #region Properties

        private SerializedProperty _currentCanvas;
        private SerializedProperty _scrollRect;
        private SerializedProperty _scrollContent;

        private SerializedProperty _outOfBoundsThreshold;
        private SerializedProperty _isUseSnap;

        private SerializedProperty _snapSpeed;
        private SerializedProperty _minVelocityToDisableInertia;

        #endregion

        #region GUI Methods

        protected virtual void OnEnable()
        {
            _currentCanvas = serializedObject.FindProperty("_currentCanvas");
            _scrollRect = serializedObject.FindProperty("_scrollRect");
            _scrollContent = serializedObject.FindProperty("_scrollContent");
            _outOfBoundsThreshold = serializedObject.FindProperty("_outOfBoundsThreshold");
            _isUseSnap = serializedObject.FindProperty("_isUseSnap");
            _snapSpeed = serializedObject.FindProperty("_snapSpeed");
            _minVelocityToDisableInertia = serializedObject.FindProperty("_minVelocityToDisableInertia");
        }
        public override void OnInspectorGUI()
        {
            InfiniteScroll infiniteScroll = (InfiniteScroll)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_currentCanvas);
            EditorGUILayout.PropertyField(_scrollRect);
            EditorGUILayout.PropertyField(_scrollContent);
            EditorGUILayout.PropertyField(_outOfBoundsThreshold);
            EditorGUILayout.PropertyField(_isUseSnap);

            if (_isUseSnap.boolValue == true)
            {
                EditorGUILayout.PropertyField(_snapSpeed);
                EditorGUILayout.PropertyField(_minVelocityToDisableInertia);
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}