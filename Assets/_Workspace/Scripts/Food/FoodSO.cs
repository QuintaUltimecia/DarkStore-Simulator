using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Food")]
public class FoodSO : ScriptableObject
{
    [field: SerializeField]
    public Mesh Mesh { get; private set; }

    [field: SerializeField]
    public Sprite Sprite { get; private set; }

    [field: SerializeField]
    public FoodVariant Variant { get; private set; }
}

public enum FoodVariant
{
    Empty, Apple, Cucumber
}
