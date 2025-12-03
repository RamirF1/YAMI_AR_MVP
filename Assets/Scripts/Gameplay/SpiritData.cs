using UnityEngine;

[CreateAssetMenu(fileName = "NewSpiritData", menuName = "Yami/Spirit Data")]
public class SpiritData : ScriptableObject
{
    public string spiritName;
    [TextArea(4, 10)]
    public string loreDescription;
    public int rewardPoints = 20;
}
