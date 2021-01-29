using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateScriptableObjects : EditorWindow
{
    string foodName = "";
    Sprite foodIcon;
    int StackSize = 1;

    int stamGain;
    int stamLose;
    int defenseP;
    int attackP;
    int HPGain;
    int HPLose;

    static Food Foodinfo;
 

    [MenuItem("Tools/Create Food")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CreateScriptableObjects));
    }

     void OnEnable()
    {
        InitData();
    }

    public static void InitData()
    {
        Foodinfo = (Food)ScriptableObject.CreateInstance(typeof(Food));
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Food", EditorStyles.boldLabel);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.Label("Food Info", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        foodName = EditorGUILayout.TextField("Name/Food Title", foodName);
        StackSize = EditorGUILayout.IntSlider("Stack Size", StackSize, 1, 20);
        foodIcon = EditorGUILayout.ObjectField("Food Icon", foodIcon, typeof(Sprite), false) as Sprite;
        EditorGUILayout.EndVertical();

        GUILayout.Label("Food Attributes", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        GUILayout.Label("Power");
        attackP = EditorGUILayout.IntSlider("ATK Power", attackP, 0, 50);
        defenseP = EditorGUILayout.IntSlider("DEF Power", defenseP, 0, 50);
        EditorGUILayout.Space();

        GUILayout.Label("Positive Effects");
        HPGain = EditorGUILayout.IntSlider("HP Gain", HPGain, 0, 50);
        stamGain = EditorGUILayout.IntSlider("Stamina Gain", stamGain, 0, 50);
        EditorGUILayout.Space();

        GUILayout.Label("Negative Effects");
        HPLose = EditorGUILayout.IntSlider("HP Lost", HPLose, 0, 50);
        stamLose = EditorGUILayout.IntSlider("Stamina Lost", stamLose, 0, 50);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Type of Food");
        Foodinfo.MyFoodType = (FoodType)EditorGUILayout.EnumPopup(Foodinfo.MyFoodType);
        EditorGUILayout.Space();

        GUILayout.Label("Rarity of Food");
        Foodinfo.MyQuality = (Quality)EditorGUILayout.EnumPopup(Foodinfo.MyQuality);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Food!"))
        {
            createFood();
        }

    }


    private void createFood()
    {
        Food currFood = ScriptableObject.CreateInstance<Food>();

        currFood.MyAttackPower = attackP;
        currFood.MyDefensePower = defenseP;
        currFood.MyFoodType = Foodinfo.MyFoodType;
        currFood.MyGainHP = HPGain;
        currFood.MyGainStamina = stamGain;
        currFood.MyIcon = foodIcon;
        currFood.MyLoseHP = HPLose;
        currFood.MyLoseStamina = stamLose;
        currFood.MyQuality = Foodinfo.MyQuality;
        currFood.MyStackSize = StackSize;
        currFood.MyTitle = foodName;

        AssetDatabase.CreateAsset(currFood, "Assets/Prefabs/Items/" + foodName + ".asset");

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = currFood;

    }
}