#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using ABI.CCK.Components;

namespace NAK.SimpleAAS
{
    public class NAKSimpleAAS: MonoBehaviour 
    {

        public CVRAvatar avatar;

        public AnimatorOverrideController baseOverrideController;
        public AnimatorController[] avatarAnimators;

        public void AAS_CompileAnimators()
        {
            var resultController = NAK.SimpleAAS.AnimatorCloner.MergeMultipleControllers(avatarAnimators, null, true, false, avatar.gameObject.name);
            baseOverrideController.runtimeAnimatorController = resultController;
            avatar.overrides = baseOverrideController;
            Animator animator = avatar.gameObject.GetComponent<Animator>();
            if (animator != null){
                animator.runtimeAnimatorController = baseOverrideController;
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

#endif