using System;
using UnityEditor;
using UnityEngine;
using Unity.GraphToolkit.Editor;

[Graph(AssetExtension)]
[Serializable]
public class TaskGraph : Graph
{
    public const string AssetExtension = "taskGraph";
    
    [MenuItem("Assets/Create/Task System/Task Graph", false)]
    static void CreateAssetFile()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<TaskGraph>();
    }
}
