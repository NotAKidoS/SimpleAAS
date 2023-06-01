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
        public AnimatorController[] avatarControllers;
    }
}

#endif