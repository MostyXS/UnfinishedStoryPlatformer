using UnityEngine;

[System.Serializable]
public class DialogueReplica
{
    [TextArea(4, 10)] [SerializeField] string content;
    [SerializeField] Decision decisionDependency;
    [SerializeField] Decision decisionToActivate;
    [Tooltip("From zero")] [SerializeField] int[] nextSteps;
    
    public Decision GetDecisionDependency()
    {
        return decisionDependency;
    }

    public string GetContent()
    {
        return content;
    }
    public Decision GetDecisionToActivate()
    {
        return decisionToActivate;
    }

    public int[] GetNextStepReplicas()
    {
        return nextSteps;
    }

    public bool IsMoreValuableThan(DialogueReplica comparableReplica)
    {
        var thisDependency = decisionDependency;
        var otherDependency = comparableReplica.decisionDependency;
        return thisDependency.IsRelevant() && (!otherDependency.IsRelevant() || otherDependency.GetPriority() < thisDependency.GetPriority());
    }

}
