using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using static NAK.SimpleAAS.ControllerCompiler;

namespace NAK.SimpleAAS
{
    [CustomEditor(typeof(NAKSimpleAAS))]
    public class NAKSimpleAASEditor : Editor
    {
        private SerializedProperty _avatar;
        private SerializedProperty _baseOverrideController;
        private SerializedProperty _avatarControllers;
        private ReorderableList _controllerReorderableList;

        #region GUI Methods

        private void OnEnable()
        {
            _avatar = serializedObject.FindProperty("avatar");
            _baseOverrideController = serializedObject.FindProperty("baseOverrideController");
            _avatarControllers = serializedObject.FindProperty("avatarControllers");

            _controllerReorderableList = new ReorderableList(serializedObject, _avatarControllers, true, true, true, true)
            {
                drawHeaderCallback = DrawControllerListHeader,
                drawElementCallback = DrawControllerListItems
            };
        }

        public override void OnInspectorGUI()
        {
            var script = (NAKSimpleAAS)target;
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(_avatar);
            EditorGUILayout.PropertyField(_baseOverrideController);
            EditorGUILayout.Space();

            int parameterUsage = script.GetParameterSyncUsage();
            EditorGUI.ProgressBar(
                EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight),
                parameterUsage / 3200f,
                parameterUsage + " of 3200 Synced Bits used."
            );
            
            _controllerReorderableList.DoLayoutList();

            if (GUILayout.Button("Compile Controllers"))
                CompileControllers(script);

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Controller Reorderable List Drawing

        private void DrawControllerListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Avatar Controllers");
        }

        private void DrawControllerListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = _controllerReorderableList.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element,
                GUIContent.none
            );
        }

        #endregion
    }
}