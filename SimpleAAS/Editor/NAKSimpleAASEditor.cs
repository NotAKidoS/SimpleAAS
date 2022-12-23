// using System.Collections.Concurrent;
// using System.Threading.Tasks;
// using System.Text;
using static System.Math;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;
using ABI.CCK.Components;


namespace NAK.SimpleAAS 
{

    [CustomEditor(typeof(NAKSimpleAAS))]
    public class NAKSimpleAASEditor: Editor {

        SerializedProperty avatar;
        SerializedProperty baseOverrideController;
        SerializedProperty avatarAnimators;
        
        ReorderableList alist; 

        private void OnEnable() {

            //find properties
            avatar = serializedObject.FindProperty("avatar");      
            baseOverrideController = serializedObject.FindProperty("baseOverrideController");
            avatarAnimators = serializedObject.FindProperty("avatarAnimators");

            //initialize animator list
            alist = new ReorderableList(serializedObject, avatarAnimators, true, true, true, true);
            alist.drawElementCallback = DrawListItems;
            alist.drawHeaderCallback = DrawHeader;
        }

        public override void OnInspectorGUI() {
            NAKSimpleAAS script = (NAKSimpleAAS)target;

            serializedObject.UpdateIfDirtyOrScript();
            GUIStyle box = GUI.skin.GetStyle("box");

            //avatar input
            EditorGUILayout.PropertyField(avatar);

            EditorGUILayout.PropertyField(baseOverrideController);

            alist.DoLayoutList();

            // Add a button named "Compile Animators"
            if (GUILayout.Button("Compile Animators")) {
                script.AAS_CompileAnimators();
            }

            serializedObject.ApplyModifiedProperties();
        }

        
        void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {        
            SerializedProperty element = alist.serializedProperty.GetArrayElementAtIndex(index);

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element,
                GUIContent.none
            );  

        }

        void DrawHeader(Rect rect)
        {
            string name = "Avatar Animators";
            EditorGUI.LabelField(rect, name);
        }
    }
}