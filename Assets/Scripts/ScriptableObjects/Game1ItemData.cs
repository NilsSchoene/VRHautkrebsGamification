using UnityEngine;

[CreateAssetMenu]
public class Game1ItemData : ScriptableObject
{
    public string itemName;
    [TextArea(2,4)]
    public string itemInfo;
    public bool itemInBag;
    public float itemScore;
}
