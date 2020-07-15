using UnityEngine;

public class GroundTilesSpriteListProvider : MonoBehaviour, ISpriteListProvider
{
    public SpriteChance[] GetSprites()
    {
        return new SpriteChance[]
        {
            new SpriteChance()
            {
                sprite = GridSprites.Ground_Black,
                weight = 20
            },
            new SpriteChance()
            {
                sprite = GridSprites.Ground_Flowers,
                weight = 1
            },
            new SpriteChance()
            {
                sprite = GridSprites.Ground_WhiteStripes,
                weight = 1
            }
        };
    }
}
