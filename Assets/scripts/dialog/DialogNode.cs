using System;

[Serializable]
public class DialogNode
{
    public string text;
    public DialogNode[] dialogNodes;

    public bool HasNextNode() { return dialogNodes.Length != 0; }
}
