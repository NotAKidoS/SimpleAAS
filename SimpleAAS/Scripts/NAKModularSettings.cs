﻿using System;
using System.Collections.Generic;
using UnityEngine;
using ABI.CCK.Scripts;

[CreateAssetMenu(fileName = "SimpleAAS_Parameters", menuName = "NAKSimpleAAS/SimpleAAS_Parameters")]
public class NAKModularSettings : ScriptableObject
{
    public List<CVRAdvancedSettingsEntry> settings = new List<CVRAdvancedSettingsEntry>();

    public static CVRAdvancesAvatarSettingBase.ParameterType GetDefaultType(CVRAdvancedSettingsEntry.SettingsType settingType)
    {
        switch (settingType)
        {
            // Toggles default to Bool
            case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                return CVRAdvancesAvatarSettingBase.ParameterType.GenerateBool;
            // Dropdowns default to Int
            case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:
                return CVRAdvancesAvatarSettingBase.ParameterType.GenerateInt;
            // Everything else is Float
            default:
                return CVRAdvancesAvatarSettingBase.ParameterType.GenerateFloat;
        }
    }

    public static List<CVRAdvancesAvatarSettingBase.ParameterType> GetSupportedTypes(CVRAdvancedSettingsEntry.SettingsType settingType)
    {
        List<CVRAdvancesAvatarSettingBase.ParameterType> supportedTypes = new List<CVRAdvancesAvatarSettingBase.ParameterType>();

        switch (settingType)
        {
            // Toggles can use all
            case CVRAdvancedSettingsEntry.SettingsType.GameObjectToggle:
                supportedTypes.Add(CVRAdvancesAvatarSettingBase.ParameterType.GenerateBool);
                supportedTypes.Add(CVRAdvancesAvatarSettingBase.ParameterType.GenerateInt);
                supportedTypes.Add(CVRAdvancesAvatarSettingBase.ParameterType.GenerateFloat);
                break;
            // Dropdowns can only use float, int
            case CVRAdvancedSettingsEntry.SettingsType.GameObjectDropdown:
                supportedTypes.Add(CVRAdvancesAvatarSettingBase.ParameterType.GenerateInt);
                supportedTypes.Add(CVRAdvancesAvatarSettingBase.ParameterType.GenerateFloat);
                break;
            // Everything else is Float
            default:
                supportedTypes.Add(CVRAdvancesAvatarSettingBase.ParameterType.GenerateFloat);
                break;
        }

        return supportedTypes;
    }

    public enum SimpleAAS_SettingsType
    {
        Toggle,
        Dropdown,
        Color,
        Slider,
        Joystick2D,
        Joystick3D,
        InputSingle,
        InputVector2,
        InputVector3
    }

    //maybe we parse this on upload?
    public enum SimpleAAS_ParameterType
    {
        Float = 1,
        Int = 2,
        Bool = 3
    }
}