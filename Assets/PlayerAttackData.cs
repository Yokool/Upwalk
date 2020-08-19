using System;
using UnityEngine;

[Serializable]
public class PlayerAttackData : PersistentFile
{
    public Turn AttackType { get; set; }

    public void IncreasetAttackType()
    {
        AttackType = (Turn)((int)++AttackType);
    }

    public PlayerAttackData()
    {

    }

    public PlayerAttackData(string filePath, Turn attackType) : base(filePath)
    {
        this.AttackType = attackType;
        Load();
        Save();
    }

    public override void PopulateFields(object deserializedObject)
    {
        PlayerAttackData data = deserializedObject as PlayerAttackData;
        this.AttackType = data.AttackType;
    }

}

public class PlayerOutfitData : PersistentFile
{

    public Turn OutfitType { get; set; }

    public PlayerOutfitData()
    {

    }

    public PlayerOutfitData(string filePath, Turn outfitType) : base(filePath)
    {
        this.OutfitType = outfitType;
        Load();
        Save();
    }

    public override void PopulateFields(object deserializedObject)
    {
        PlayerOutfitData outfitData = deserializedObject as PlayerOutfitData;
        OutfitType = outfitData.OutfitType;
    }

    public Sprite GetOutfitSprite()
    {
        switch (OutfitType)
        {
            case Turn.EASY:
                return GameSprites.Player_Easy;
            case Turn.MEDIUM:
                return GameSprites.Player_Medium;
            case Turn.HARD:
                return GameSprites.Player_Hard;
        }

        Debug.LogError("ASSERTION FAILED: This code block should not execute");
        return null;
    }


}

public class PlayerHealthData : PersistentFile
{

    public int HealthAmount { get; set; }

    public PlayerHealthData()
    {

    }

    public PlayerHealthData(string filePath, int healthAmount) : base(filePath)
    {
        this.HealthAmount = healthAmount;
        Load();
        Save();
    }

    public override void PopulateFields(object deserializedObject)
    {
        PlayerHealthData healthData = deserializedObject as PlayerHealthData;
        this.HealthAmount = healthData.HealthAmount;
    }
}