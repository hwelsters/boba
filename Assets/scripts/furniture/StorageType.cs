using UnityEngine;

[CreateAssetMenu(fileName="StorageType", menuName="Create new StorageType")]
public class StorageType : ScriptableObject
{
    [SerializeField] public Sprite[] sprites;
    
    [SerializeField] public sbyte maxCount = 3;
    [SerializeField] public Vector2 offset;
    
    [Header("Temperature")]
    [SerializeField] public float temperatureChangeRate;
    [SerializeField] public float temperatureToApproach;
}
