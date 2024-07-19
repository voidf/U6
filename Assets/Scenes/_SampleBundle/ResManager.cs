using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ResManager
{
#if UNITY_EDITOR
    public static T LoadAsset<T>(string bundleName, string fileName) where T : Object
    {
        string path = "Assets/" + bundleName + "/" + fileName;
        if (Defs.Type2Exts.TryGetValue(typeof(T), out string[] exts))
        {
            foreach (string ext in exts)
            {
                var file = AssetDatabase.LoadAssetAtPath<T>(path + ext);
                if (file != null) { return file; }
            }
        }
        Debug.LogErrorFormat("LoadAssetFromEditor({0}, {1}):Unknown type {2}, to add type to Defs.Type2Exts.", bundleName, fileName, typeof(T));
        return null;
    }
#else
    static Dictionary<string, AssetBundle> abCache = new Dictionary<string, AssetBundle>();
    public static T LoadAsset<T>(string bundleName, string fileName) where T : Object
    {
        if (!abCache.TryGetValue(bundleName, out AssetBundle assetBundle))
        {
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Defs.AssetBundlePath, bundleName.ToLower()));
            abCache.Add(bundleName, assetBundle);
        }
        return assetBundle.LoadAsset<T>(fileName);
    }
#endif

    static Dictionary<string, ScriptableObject> _keyToTable = new Dictionary<string, ScriptableObject>();
    public static T LoadTable<T>(string tableName) where T : ScriptableObject, ITable
    {
        if (_keyToTable.TryGetValue(tableName, out ScriptableObject obj))
        {
            return obj as T;
        }

        var tb = LoadAsset<ScriptableObject>(Defs.AssetBundleName.TableTotal, tableName) as T;
        tb.Ctor();
        _keyToTable.Add(tableName, tb);
        return tb;
    }
}