using UnityEditor;
using UnityEngine;

public class BasicSpawner : EditorWindow
{
    string BaseName = "";
    int ObjID = 1;
    GameObject prefabToSpawn;
    float prefabScale;
    float spawnRadius = 5f;
    Vector3 spawnArea;

    [MenuItem("Tools/SpawnPrefabs")]
    public static void ShowWindow()
    {
        GetWindow(typeof(BasicSpawner));
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn!", EditorStyles.boldLabel);

        BaseName = EditorGUILayout.TextField("Name", BaseName);
        ObjID = EditorGUILayout.IntField("Obj ID", ObjID);
        prefabScale = EditorGUILayout.Slider("Object Scale", prefabScale, 0.5f , 3f);
        spawnArea = EditorGUILayout.Vector3Field("Area to spawn", spawnArea);
        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
        prefabToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", prefabToSpawn, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Spawn"))
        {
            spawnObject();
        }
    }
    private void spawnObject()
    {
        if(prefabToSpawn == null)
        {
            Debug.Log("Missing: Missing PREFAB, please assign one.");
            return;
        }

        if (BaseName == string.Empty)
        {
            Debug.Log("Missing: Missing NAME, please assign a name.");
            return;
        }


        Vector2 spawnCircle = spawnArea + (Random.insideUnitSphere * spawnRadius);
        Vector3 spawnPos = new Vector3(spawnCircle.x, spawnCircle.y, 0f);

        GameObject newObj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        newObj.name = BaseName + ObjID;
        newObj.transform.localScale = Vector3.one * prefabScale;

        ObjID++;
     }
}
