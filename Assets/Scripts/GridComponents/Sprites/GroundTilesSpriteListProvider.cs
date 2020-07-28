using UnityEngine;

public class GroundTilesSpriteListProvider : MonoBehaviour, ISpriteListProvider
{
    public WeightObjectTie<Sprite>[] GetSprites()
    {
        return new WeightObjectTie<Sprite>[]
        {
            new WeightObjectTie<Sprite>()
            {
                weightedObject = GameSprites.Ground_Black,
                weight = 20
            },
            new WeightObjectTie<Sprite>()
            {
                weightedObject = GameSprites.Ground_Flowers,
                weight = 1
            },
            new WeightObjectTie<Sprite>()
            {
                weightedObject = GameSprites.Ground_WhiteStripes,
                weight = 1
            }
        };
    }
}
