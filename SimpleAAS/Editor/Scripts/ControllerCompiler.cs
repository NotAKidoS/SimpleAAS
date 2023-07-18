// MIT License

// Copyright (c) 2018 King Arthur

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

//https://github.com/poiyomi/PoiyomiToonShader/

using System.Linq;
using ABI.CCK.Scripts.Editor;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using static NAK.SimpleAAS.ControllerCloner;

namespace NAK.SimpleAAS
{
    public class ControllerCompiler
    {
        [InitializeOnLoad]
        public class CompileControllersOnBuild
        {
            static CompileControllersOnBuild() => CCK_BuildUtility.PreAvatarBundleEvent.AddListener(OnPreBundleEvent);

            private static void OnPreBundleEvent(GameObject uploadedObject)
            {
                Scene targetScene = uploadedObject.scene;
                var allAASComponents = targetScene.GetRootGameObjects()
                    .SelectMany(x => x.GetComponentsInChildren<NAKSimpleAAS>(true));

                NAKSimpleAAS targetAvatar =
                    allAASComponents.FirstOrDefault(aas => aas.avatar.gameObject == uploadedObject);

                if (targetAvatar != null) CompileControllers(targetAvatar);
            }
        }

        public static void CompileControllers(NAKSimpleAAS script)
        {
            AnimatorController resultController = MergeMultipleControllers(
                script.avatarControllers,
                null,
                true,
                false,
                script.avatar.gameObject.name
            );

            if (script.baseOverrideController != null)
            {
                script.baseOverrideController.runtimeAnimatorController = resultController;
                script.avatar.overrides = script.baseOverrideController;
            }

            if (script.avatar.gameObject.TryGetComponent<Animator>(out var animator))
            {
                animator.runtimeAnimatorController = script.baseOverrideController;
            }

            EditorUtility.SetDirty(script.avatar);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}