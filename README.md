# SimpleAAS
Simple utility script to replace the normal workflow for building Avatars in ChilloutVR.

**This is not a tool meant to be used for merging VRC controllers for ports.** While you *can* use the tool for this, it is not what it is intended for. 
Beware that the tool will run at build time if left in the scene.

# What is SimpleAAS?

This tool provides a replacement for the normal Avatar building workflow where things are manual and modular.

You assign as many individual controllers and menu items as you want and at build time they are merged and assigned to the CVRAvatar script.

This allows for modularizing Avatars and easily sharing prefabs across them without needing to maintain a copy of a system per-avatar.

For example, you could use this to build a locomotion prefab utilizing custom controllers and menu items, and easily assign them to the tool to be included on your avatar at build.

### Note:
This tool is fundimentally at conflict with the CCKs built-in Auto Generation tools to build layers and toggles for you. 
It cannot be used in tandum with that existing system nor will have support in the future.

# How To Use

To use SimpleAAS you will you need to add the NAK Simple AAS or NAK Simple AAS Parameters components to an empty object somewhere in your scene (or both components if you want to use both mergers).

For organization purposes it is recommened to make this object as a child of the Avatar with the GameObject tagged as Editor Only:
<img width="944" height="862" alt="image" src="https://github.com/user-attachments/assets/991f883e-9114-4d01-858a-b436c8c9a2a1" />

You can create the NAK Modular Settings asset by right-clicking somewhere in a folder and navigating the Create context menu:

<img width="1043" height="372" alt="image" src="https://github.com/user-attachments/assets/dd8b2ee8-d338-45e1-9c6d-0293b35e4f24" />

From there you can assign as many Animation Controllers and NAK Modular Settings assets as you want.

### Porting existing Avatar Advanced Settings to NAK Modular Settings asset:

You can easily extract the settings you have already in your CVRAvatar to the modular settings asset by using the CCKs built-in utility menu found on most reorderable lists. 

**You should do this before attempting a build with the NAKSimpleAASParameters component in scene, as it will likely override your menu items in the CVRAvatar script.**

To move the parameters back into the CVRAvatar script you can use the Sync to Avatar button or copy them back with the same method as above.

![Unity_5XEg10qEjO](https://github.com/user-attachments/assets/9f6fd553-d0b6-471d-891a-9b9de2aff8f0)

------

# Credit:

[Avatars-3.0-Manager](https://github.com/VRLabs/Avatars-3.0-Manager) - [MIT License](https://github.com/VRLabs/Avatars-3.0-Manager/blob/main/LICENSE)

Modified the incredibly-well-built Animator Cloner script to merge controllers together with everything accounted for.

[PoiyomiToonShader](https://github.com/poiyomi/PoiyomiToonShader) - [MIT License](https://github.com/poiyomi/PoiyomiToonShader/blob/master/LICENSE)

Nicked the ChilloutVR CCK Upload hook code to automatically compile controllers on upload.

---

Here is the block of text where I tell you it's not my fault if you're bad at Unity.

> Use of this Unity Script is done so at the user's own risk and the creator cannot be held responsible for any issues arising from its use.
