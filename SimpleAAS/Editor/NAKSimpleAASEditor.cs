using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using static NAK.SimpleAAS.ControllerCompiler;

namespace NAK.SimpleAAS
{
    [CustomEditor(typeof(NAKSimpleAAS))]
    public class NAKSimpleAASEditor : Editor
    {
        private SerializedProperty avatar;
        private SerializedProperty baseOverrideController;
        private SerializedProperty avatarControllers;
        private ReorderableList alist;

        private void OnEnable()
        {
            FindProperties();
            InitializeAnimatorList();
        }

        public override void OnInspectorGUI()
        {
            var script = (NAKSimpleAAS)target;

            serializedObject.UpdateIfRequiredOrScript();
            DrawAvatarInput();
            DrawBaseOverrideControllerInput();
            alist.DoLayoutList();
            DrawCompileControllersButton(script);
            serializedObject.ApplyModifiedProperties();
        }

        private void FindProperties()
        {
            avatar = serializedObject.FindProperty("avatar");
            baseOverrideController = serializedObject.FindProperty("baseOverrideController");
            avatarControllers = serializedObject.FindProperty("avatarControllers");
        }

        private void InitializeAnimatorList()
        {
            alist = new ReorderableList(serializedObject, avatarControllers, true, true, true, true)
            {
                drawElementCallback = DrawListItems,
                drawHeaderCallback = DrawHeader
            };
        }

        private void DrawAvatarInput()
        {
            EditorGUILayout.PropertyField(avatar);
        }

        private void DrawBaseOverrideControllerInput()
        {
            EditorGUILayout.PropertyField(baseOverrideController);
        }

        private void DrawCompileControllersButton(NAKSimpleAAS script)
        {
            if (GUILayout.Button("Compile Controllers"))
            {
                CompileControllers(script);
            }
        }

        private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = alist.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element,
                GUIContent.none
            );
        }

        private void DrawHeader(Rect rect)
        {
            string name = "Avatar Controllers";
            EditorGUI.LabelField(rect, name);
        }
    }
}
