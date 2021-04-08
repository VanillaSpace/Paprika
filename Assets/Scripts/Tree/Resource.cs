using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, IInteractable
{
    public int treeLife;

    [SerializeField]
    public SpriteRenderer spriteRender;

    [SerializeField]
    private Sprite treeStump;

    public float spawnRadius = 2.5f;

    [SerializeField]
    GameObject treeArea;

    [SerializeField]
    GameObject logPrefab;

    public int stopAfterSpawns = 5;

    public int currSpawns;



    // Start is called before the first frame update
    void Awake()
    {
       
    }
    void Start()
    {
        treeLife = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (treeLife == 0)
        {
            spriteRender.sprite = treeStump;

            Invoke("SpawnLogs", 1f);

            SpawnLogs();
        }
    }

    public void Interact()
    {
        if (GatherLoot.MyInstance.canChop)
        {
            BasicMovement.MyInstance.Chop(projectileBook.MyInstance.GetProjectile("Chop"));
            treeLife--;
        }
    }

    public void StopInteract()
    {

    }

    public void SpawnLogs()
    {
        if (currSpawns >= stopAfterSpawns)
        {
            CancelInvoke("SpawnLogs");
            return;
        }

            Vector2 spawnCircle = treeArea.transform.position + (UnityEngine.Random.insideUnitSphere * spawnRadius);
            Vector3 spawnPos = new Vector3(spawnCircle.x, spawnCircle.y, 0f);

            GameObject newObj = Instantiate(logPrefab, spawnPos, Quaternion.identity);
            currSpawns++;

    }

}
