using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Defs
{
    public static readonly string AssetBundlePath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");

    public static class AssetBundleName
    {
        public const string Windows = "Prefab/Windows";
        public const string Widgets = "Prefab/Widgets";
        public const string TableTotal = "Table/Total";
        public const string TableBullet = "Table/Bullet";
        public const string TableWeapon = "Table/Weapon";
        public const string TablePickup = "Table/Pickup";
        public const string TableScene = "Table/Scene";
        public const string Item = "Prefab/item";
        public const string Character = "Prefab/Character";
        public const string Heroes = "Prefab/Heroes";
        public const string SpriteModel = "Prefab/SpriteModel";
        public const string Box = "Prefab/Box";
        public const string TextureConst = "Texture/Const";
    }

    public static Dictionary<Type, string[]> Type2Exts = new Dictionary<Type, string[]>(){
        { typeof(GameObject), new string[]{ ".prefab" } },
        { typeof(ScriptableObject), new string[]{ ".asset" } },
        { typeof(Sprite), new string[]{ ".png" } },
    };
}
