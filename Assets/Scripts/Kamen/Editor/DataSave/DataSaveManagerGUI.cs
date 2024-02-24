using UnityEngine;
using UnityEditor;

namespace Kamen.DataSave
{
    [CustomEditor(typeof(DataSaveManager))]
    public class DataSaveManagerGUI : Editor
    {
        #region Properties

        private SerializedProperty _saveType;
        private SerializedProperty _fileName;
        private SerializedProperty _encryptionType;
        private SerializedProperty _key;
        private SerializedProperty _iv;

        #endregion

        #region GUI Methods

        protected virtual void OnEnable()
        {
            _saveType = serializedObject.FindProperty("_saveType");
            _fileName = serializedObject.FindProperty("_fileName");
            _encryptionType = serializedObject.FindProperty("_encryptionType");
            _key = serializedObject.FindProperty("_key");
            _iv = serializedObject.FindProperty("_iv");
        }
        public override void OnInspectorGUI()
        {
            DataSaveManager dataSaveManager = (DataSaveManager)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_saveType);
            EditorGUILayout.PropertyField(_fileName);
            EditorGUILayout.PropertyField(_encryptionType);

            if (dataSaveManager.MyEncryptionType != EncryptionType.None)
            {
                EditorGUILayout.PropertyField(_key);
                EditorGUILayout.PropertyField(_iv);
                if (GUILayout.Button("Generate new Key and IV")) dataSaveManager.GenerateNewKeyAndIV();
            }

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Delete Data")) dataSaveManager.DeleteData();  
        }

        #endregion
    }
}