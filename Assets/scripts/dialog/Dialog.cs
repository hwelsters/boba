using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Create new dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField] private DialogNode startingNode;
}