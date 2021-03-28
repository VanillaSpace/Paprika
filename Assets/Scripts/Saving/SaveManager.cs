using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Item[] items;

    private Chest[] chests;
    // Start is called before the first frame update
     void Awake()
    {
        chests = FindObjectsOfType<Chest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Save();
            Debug.Log("Saved");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Load();
            Debug.Log("Loaded");
        }
    }

    private void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "Savetest.dat", FileMode.OpenOrCreate);

            SaveData data = new SaveData();

            SavePlayer(data);

            SaveChests(data);

            bf.Serialize(file,data);

            file.Close();

        }
        catch (System.Exception)
        {
            //this is for handling errors
        }
    }
    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(Player.MyInstance.MyLevel, 
            Player.MyInstance.MyXP.myCurrentValue, 
            Player.MyInstance.MyXP.MyMaxValue,
            Player.MyInstance.MyHealth.myCurrentValue,
            Player.MyInstance.MyHealth.MyMaxValue,
            Player.MyInstance.MyStamina.myCurrentValue,
            Player.MyInstance.MyStamina.MyMaxValue);
    }

    private void SaveChests(SaveData data)
    {
        for (int i = 0; i < chests.Length; i++)
        {
            data.MyChestData.Add(new ChestData(chests[i].name));

            foreach (Item item in chests[i].MyItems)
            {
                if (chests[i].MyItems.Count > 0)
                {
                    data.MyChestData[i].MyItems.Add(new ItemData(item.MyTitle, item.MySlot.MyItems.Count, item.MySlot.MyIndex));
                }
            }
        }
    }


    private void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "Savetest.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadPlayer(data);

            LoadChests(data);

        }
        catch (System.Exception)
        {
            //this is for handling errors
        }
    }

    private void LoadPlayer(SaveData data)
    {
        Player.MyInstance.MyLevel = data.MyPlayerData.MyLevel;
        Player.MyInstance.UpdateLevel();
        Player.MyInstance.MyHealth.Initialize(data.MyPlayerData.MyHealth, data.MyPlayerData.MyMaxHealth);
        Player.MyInstance.MyStamina.Initialize(data.MyPlayerData.MyStamina, data.MyPlayerData.MyMaxStamina);
        Player.MyInstance.MyXP.Initialize(data.MyPlayerData.MyXp, data.MyPlayerData.MyMaxXP);
    }

    private void LoadChests(SaveData data)
    {
        foreach (ChestData chest in data.MyChestData)
        {
            Chest c = Array.Find(chests, x => x.name == chest.MyName);

            foreach (ItemData itemData in chest.MyItems)
            {
                Item item = Instantiate(Array.Find(items, x => x.MyTitle == itemData.MyTitle));
                item.MySlot = c.MyBag.MySlots.Find(x => x.MyIndex == itemData.MySlotIndex);
                c.MyItems.Add(item);
            }
        }

    }

}

