using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class AllAppDataBuilder
{
    private const string AutoRebuildPrefKey = "AllAppData.AutoRebuild";

    /// <summary>?????????? ????? ???????, ???????? new[] { "Assets/Data" }. null = ???? ??????.</summary>
    private static readonly string[] SearchFolders = null;

    public static bool AutoRebuild
    {
        get => EditorPrefs.GetBool(AutoRebuildPrefKey, true);
        set => EditorPrefs.SetBool(AutoRebuildPrefKey, value);
    }

    [MenuItem("Tools/App Data/Rebuild AllAppData")]
    public static void RebuildFromMenu()
    {
        var registry = FindRegistry();
        if (registry == null)
        {
            Debug.LogError("[AllAppData] ????? AllAppData ?? ?????? ? ???????");
            return;
        }

        bool changed = Rebuild(registry);
        Debug.Log($"[AllAppData] {(changed ? "?????????" : "????????? ???")}, ?????????: {registry.apps.Count}", registry);
    }

    [MenuItem("Tools/App Data/Auto Rebuild On Asset Change")]
    private static void ToggleAutoRebuild() => AutoRebuild = !AutoRebuild;

    [MenuItem("Tools/App Data/Auto Rebuild On Asset Change", true)]
    private static bool ToggleAutoRebuildValidate()
    {
        Menu.SetChecked("Tools/App Data/Auto Rebuild On Asset Change", AutoRebuild);
        return true;
    }

    public static AllAppData FindRegistry()
    {
        var guids = AssetDatabase.FindAssets("t:AllAppData");
        if (guids.Length == 0) return null;

        if (guids.Length > 1)
            Debug.LogWarning("[AllAppData] ? ??????? ????????? ????????, ???????????? ?????? ?????????");

        return AssetDatabase.LoadAssetAtPath<AllAppData>(AssetDatabase.GUIDToAssetPath(guids[0]));
    }

    /// <summary>???????????? ??????. true, ???? ?????????? ??????? ??????????.</summary>
    public static bool Rebuild(AllAppData registry)
    {
        if (registry == null) return false;

        var guids = SearchFolders == null
            ? AssetDatabase.FindAssets("t:AppData")
            : AssetDatabase.FindAssets("t:AppData", SearchFolders);

        var found = guids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Distinct()
            .Select(AssetDatabase.LoadAssetAtPath<AppData>)
            .Where(asset => asset != null)
            .OrderBy(asset => asset.name, StringComparer.Ordinal)
            .ToList();

        if (registry.apps != null && registry.apps.SequenceEqual(found)) return false;

        Undo.RecordObject(registry, "Rebuild AllAppData");
        registry.apps = found;
        EditorUtility.SetDirty(registry);
        AssetDatabase.SaveAssetIfDirty(registry);
        return true;
    }

    /// <summary>?????????????? ??? ??????????, ???????? ? ??????????? ???????.</summary>
    private class AssetWatcher : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(
            string[] imported, string[] deleted, string[] moved, string[] movedFrom)
        {
            if (!AutoRebuild) return;

            bool touched = imported.Any(IsAsset) || deleted.Any(IsAsset)
                        || moved.Any(IsAsset) || movedFrom.Any(IsAsset);
            if (!touched) return;

            // delayCall, ????? ?? ?????? ? ???? ????? ?? ????? ???????
            EditorApplication.delayCall += () =>
            {
                var registry = FindRegistry();
                if (registry != null) Rebuild(registry);
            };
        }

        private static bool IsAsset(string path) => path.EndsWith(".asset", StringComparison.OrdinalIgnoreCase);
    }
}

[CustomEditor(typeof(AllAppData))]
public class AllAppDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Rebuild", GUILayout.Height(24)))
        {
            AllAppDataBuilder.Rebuild((AllAppData)target);
            serializedObject.Update();
        }

        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}