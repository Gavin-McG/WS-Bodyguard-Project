using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, TaskGraph.AssetExtension)]
internal class TaskGraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var graph = GraphDatabase.LoadGraphForImporter<TaskGraph>(ctx.assetPath);
        if (graph == null)
        {
            Debug.LogError($"Failed to load Task Graph Object: {ctx.assetPath}");
            return;
        }

        //Get texture for asset
        //var assetTexture = Resources.Load<Texture2D>("DialogueGraphTexture");

        
        //get list of all ITaskNodes nodes
        var taskNodes = graph.GetNodes().OfType<ITaskNode>().ToList();
        
        //Create objects for each node
        int nonMainAssetCount = 0;
        foreach (var taskNode in taskNodes)
        {
            taskNode.CreateTask();
            var task = taskNode.GetTask();
            
            if (taskNode is BeginTaskNode node)
            {
                ctx.AddObjectToAsset("Main", task); // TODO Add texture
                ctx.SetMainObject(task);
            }
            else 
            {
                nonMainAssetCount++;
                ctx.AddObjectToAsset(nonMainAssetCount.ToString(), task); // TODO Add texture
            }
        }
        
        //assign next task references between nodes
        foreach (var taskNode in taskNodes)
        {
            taskNode.AssignNextTasks();
        }
    }
}