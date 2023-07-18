#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using ABI.CCK.Components;
using ABI.CCK.Scripts;
using UnityEditor.Animations;
using UnityEngine;

namespace NAK.SimpleAAS
{
    public class NAKSimpleAAS : MonoBehaviour
    {
        public CVRAvatar avatar;

        public AnimatorOverrideController baseOverrideController;
        public AnimatorController[] avatarControllers;

        #region Unity Methods

        private void Reset()
        {
            if (avatar == null)
                avatar = GetComponent<CVRAvatar>();
        }

        #endregion

        #region Public Methods

        public int GetParameterSyncUsage()
        {
            var syncedValues = 0;
            var syncedBooleans = 0;

            if (avatar?.avatarSettings == null)
                return 0;

            var animatorParameters = new HashSet<string>();

            foreach (AnimatorController controller in avatarControllers)
                if (controller != null)
                    foreach (AnimatorControllerParameter parameter in controller.parameters)
                        if (IsParameterSyncable(parameter) && !animatorParameters.Contains(parameter.name))
                        {
                            CountParameterTypes(parameter, ref syncedValues, ref syncedBooleans);
                            animatorParameters.Add(parameter.name);
                        }

            // Stuff in AAS Entries- counts whats not added to controller yet
            // If not added to controller, these values don't matter at all...
            foreach (CVRAdvancedSettingsEntry entry in avatar.avatarSettings.settings)
                CountEntryTypes(entry, animatorParameters, ref syncedValues, ref syncedBooleans);

            return syncedValues * 32 + Mathf.CeilToInt(syncedBooleans / 8f) * 8;
        }

        #endregion

        #region Private Methods

        private bool IsParameterSyncable(AnimatorControllerParameter parameter)
        {
            return parameter.name.Length > 0 && !coreParameters.Contains(parameter.name) &&
                   !parameter.name.StartsWith("#");
        }

        private void CountParameterTypes(AnimatorControllerParameter parameter, ref int syncedValues,
            ref int syncedBooleans)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                syncedBooleans += 1;
            else if (parameter.type == AnimatorControllerParameterType.Int ||
                     parameter.type == AnimatorControllerParameterType.Float)
                syncedValues += 1;
        }

        private void CountEntryTypes(CVRAdvancedSettingsEntry entry, HashSet<string> animatorParameters,
            ref int syncedValues, ref int syncedBooleans)
        {
            if (entry?.name == null || entry.name.Length == 0 || entry.name.StartsWith("#"))
                return;

            switch (entry.type)
            {
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                    if (animatorParameters.Contains(entry.machineName)) break;
                    if (entry.setting.usedType == CVRAdvancesAvatarSettingBase.ParameterType.GenerateBool)
                        syncedBooleans += 1;
                    else
                        syncedValues += 1;
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:
                    if (!animatorParameters.Contains(entry.machineName))
                        syncedValues += 1;
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.MaterialColor:
                    IncrementSyncValuesForEntry(entry, animatorParameters, ref syncedValues, "-r", "-g", "-b");
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Joystick2D:
                case CVRAdvancedSettingsEntry.SettingsType.InputVector2:
                    IncrementSyncValuesForEntry(entry, animatorParameters, ref syncedValues, "-x", "-y");
                    break;
                case CVRAdvancedSettingsEntry.SettingsType.Joystick3D:
                case CVRAdvancedSettingsEntry.SettingsType.InputVector3:
                    IncrementSyncValuesForEntry(entry, animatorParameters, ref syncedValues, "-x", "-y", "-z");
                    break;
                default:
                    if (!animatorParameters.Contains(entry.machineName)) syncedValues += 1;
                    break;
            }
        }

        private void IncrementSyncValuesForEntry(CVRAdvancedSettingsEntry entry, HashSet<string> animatorParameters,
            ref int syncedValues, params string[] suffixes)
        {
            if (suffixes.Any(suffix => animatorParameters.Contains(entry.machineName + suffix)))
                return;

            syncedValues += suffixes.Length;
        }

        #endregion

        #region Core Parameters

        private static readonly HashSet<string> coreParameters = new HashSet<string>
        {
            "MovementX",
            "MovementY",
            "Grounded",
            "Emote",
            "GestureLeft",
            "GestureRight",
            "Toggle",
            "Sitting",
            "Crouching",
            "CancelEmote",
            "Prone",
            "Flying"
        };

        #endregion
    }
}
#endif