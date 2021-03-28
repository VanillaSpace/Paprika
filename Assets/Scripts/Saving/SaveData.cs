using System;
using System.Collections.Generic;

[Serializable]
public class SaveData 
{ 
    public PlayerData MyPlayerData { get; set; }

    public List<ChestData> MyChestData { get; set; }
    public SaveData()
    {
        MyChestData = new List<ChestData>();
    }
}

[Serializable]
public class PlayerData
{
    public int  MyLevel { get; set; }

    public float MyXp { get; set; }

    public float MyMaxXP { get; set; }

    public float MyHealth { get; set; }

    public float MyMaxHealth { get; set; }

    public float MyStamina { get; set; }

    public float MyMaxStamina { get; set; }

    public PlayerData(int level, float xp, float maxXp, float  health, float maxHealth, float stamina, float maxStamina)
    {
        this.MyLevel = level;
        this.MyXp = xp;
        this.MyMaxXP = maxXp;
        this.MyHealth = health;
        this.MyMaxHealth = maxHealth;
        this.MyStamina = stamina;
        this.MyMaxStamina = maxStamina;
    }

}

[Serializable]
public class ItemData
{
    public string MyTitle { get; set; }

    public int MyStackCount { get; set; }

    public int MySlotIndex { get; set; }

    public ItemData(string title, int stackCount = 0, int slotIndex = 0)
    {
        MyTitle = title;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;

    }
}

[Serializable]
public class ChestData
{
    public string MyName { get; set; }

    public List<ItemData> MyItems { get; set; }

    public ChestData(string name)
    {
        MyName = name;

        MyItems = new List<ItemData>();
    }
}
