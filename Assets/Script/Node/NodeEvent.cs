using UnityEngine.Events;

static class NodeEvent
{
    public static UnityEvent<int> OnInteractNoNode = new();
    public static UnityEvent<int> OnInteractYesNode = new();

}