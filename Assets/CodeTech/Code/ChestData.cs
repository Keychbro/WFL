using UnityEngine;

[CreateAssetMenu(fileName = "NewChestData", menuName = "CodeTech/Data/NewChestData")]
public class ChestData : ScriptableObject
{
    [Space(20)]

    public Vector2 ToolsRange;
    public Vector2 DiamondsRange;
    public Vector2 GoldRange;
    public Vector2 MarkRange;

    [Space(50)]

    [Min(0)] public int ToolsChance;
    [Min(0)] public int DiamondsChance;
    [Min(0)] public int GoldChance;
    [Min(0)] public int MarkChance;

    [Space(50)]
    public Sprite ChestSprite;
}
