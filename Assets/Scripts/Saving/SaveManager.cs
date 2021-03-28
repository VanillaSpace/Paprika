using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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


    private void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "Savetest.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();

            LoadPlayer(data);

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

}

