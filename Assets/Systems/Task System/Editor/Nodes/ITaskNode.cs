using UnityEngine;

public interface ITaskNode
{
    public TaskSO CreateTask();

    public TaskSO GetTask();

    public void AssignRelativeTasks();
}
