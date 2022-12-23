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

#if CVR_CCK_EXISTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABI.CCK.Scripts.Editor;
using ABI.CCK.Components;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;
using System.IO;

namespace NAK.SimpleAAS
{
    public class AbiAutoLock
    {
        [InitializeOnLoad]
        public class AAS_CompileAnimators
        {
            static AAS_CompileAnimators()
            {
                CCK_BuildUtility.PreAvatarBundleEvent.AddListener(OnPreBundleEvent);
                CCK_BuildUtility.PrePropBundleEvent.AddListener(OnPreBundleEvent);
            }

            private static NAKSimpleAAS SimpAASInfo;
            private static AnimatorController resultController;

            static void OnPreBundleEvent(GameObject uploadedObject)
            {
                var simpaas = Object.FindObjectsOfType<NAKSimpleAAS>();

                //find our avatar
                foreach(var x in simpaas) {
                    if (x.avatar.gameObject == uploadedObject) {
                        Debug.Log("Avatar Found!");
                        SimpAASInfo = x;
                    }
                }
                if (!SimpAASInfo) return;
                SimpAASInfo.AAS_CompileAnimators();
            }
        }
    }
}
#endif