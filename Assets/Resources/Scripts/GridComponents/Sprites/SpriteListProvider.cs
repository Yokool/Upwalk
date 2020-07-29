using UnityEngine;

public interface ISpriteListProvider
{
    WeightObjectTie<Sprite>[] GetSprites();
}
