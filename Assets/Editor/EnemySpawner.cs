using UnityEditor;
using UnityEngine;
using UnityEditor.AnimatedValues;

public class EnemySpawner : EditorWindow
{
    //SetUp
    string enemyName = "";
    int enemyID = 1;
    GameObject enemyPrefab;
    float enemyScale = 1f;
    float spawnRadius = 5f;
    Vector3 spawnArea;
    AnimBool customizeVal;

    //Enemy Attributes
    float baseHP = 100;
    float baseSpeed = 1f;
    string currEnemyType;
    float aggroRange = 1f;

    [MenuItem("Tools/Enemy Prefabs Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EnemySpawner));
    }

    private void OnEnable()
    {
        customizeVal = new AnimBool(false);
        customizeVal.valueChanged.AddListener(Repaint);
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawner Enemies", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        enemyName = EditorGUILayout.TextField("Name", enemyName);
        enemyID = EditorGUILayout.IntField("Obj ID", enemyID);
        spawnArea = EditorGUILayout.Vector3Field("Area to spawn", spawnArea);
        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
        enemyPrefab = EditorGUILayout.ObjectField("Prefab to Spawn", enemyPrefab, typeof(GameObject), false) as GameObject;

        customizeVal.target = EditorGUILayout.ToggleLeft("Customize Enemy Stats", customizeVal.target);


      
        //Opens if pressed, Radio box
        if (EditorGUILayout.BeginFadeGroup(customizeVal.faded))
        {
            EditorGUI.indentLevel++;

            enemyScale = EditorGUILayout.Slider("Enemy Scale", enemyScale, 1f, 10f);
            baseHP = EditorGUILayout.Slider("Base Health", baseHP, 10f, 100f);
            baseSpeed = EditorGUILayout.Slider("Base Speed", baseSpeed, 0.5f, 5f);
            currEnemyType = EditorGUILayout.TextField("Enemy Type", currEnemyType);
            aggroRange = EditorGUILayout.Slider("Aggro Range", aggroRange, 1f, 5f);

            EditorGUI.indentLevel--;
        }
        else
        {
            // When toggle gets unchecked, revert to default vaules.
            baseHP = 20;
            enemyScale = 1f;
            baseSpeed = 0.8f;
            currEnemyType = "Slimes";
            aggroRange = 1.5f;
        }
        EditorGUILayout.EndFadeGroup();
        
        //if prefab is empty cannot
        EditorGUI.BeginDisabledGroup(enemyPrefab == null);

        if (GUILayout.Button("Spawn Enemy"))
        {
            SpawnEnemy();
        }

        EditorGUI.EndDisabledGroup();
    }
    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.Log("Missing: Missing your enemy PREFAB.");
            return;
        }

        if (enemyName == string.Empty)
        {
            Debug.Log("Missing: Missing NAME, please assign a X & Y.");
            return;
        }

        if (spawnArea == Vector3.zero)
        {
            Debug.Log("Missing: Position, please assign a name.");
            return;
        }

      
        Vector2 spawnCircle = spawnArea + (Random.insideUnitSphere * spawnRadius);
        Vector3 spawnPos = new Vector3(spawnCircle.x, spawnCircle.y, 0f);

        GameObject newObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        newObj.name = enemyName + enemyID;
        newObj.transform.localScale = Vector3.one * enemyScale;

        //this will set the inputs for the CHARACTER
        Enemy currentEnemy = newObj.GetComponentInParent<Enemy>();
        currentEnemy.EnemyHealth = baseHP;
        currentEnemy.Speed = baseSpeed;
        currentEnemy.MyType = currEnemyType;

        //this will set the inputs for the CHARACTER
        Enemy currEnemy = newObj.GetComponent<Enemy>();
        currEnemy.MyAggroRange = aggroRange;

        enemyID++;

    }
}
