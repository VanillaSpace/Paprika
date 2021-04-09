using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    Player character;
    Rigidbody2D rgbd2d;
    ActionButton actionButtonController;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markermanager;
    [SerializeField] TileMapReadController tileMapeReadcontroller;
    [SerializeField] float maxDistance = 1.5f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTile;
    [SerializeField] TileData Non_plowableTile;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        character = GetComponent<Player>();
        rgbd2d = GetComponent<Rigidbody2D>();
        actionButtonController = GetComponent<ActionButton>();
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UseWorldTool() == true)
            {
                return;
            }
            UseToolGrid();
        }
    }
    private void SelectTile()
    {
        selectedTilePosition = tileMapeReadcontroller.GetGridPostion(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 characterPos = transform.position;
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPos, cameraPos) < maxDistance;
        markermanager.Show(selectable);
    }

    private void Marker()
    {
        markermanager.markedCellPosition = selectedTilePosition;
    }

    private bool UseWorldTool()
    {
        Tools_Scriptable tool = ActionButton.MyInstance.GetTool;
        Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;
        
        if(tool == null) { return false; }
        
        if(tool.OnAction == null) { return false; }

        bool complete = tool.OnAction.OnApply(position);

        return complete;
    }

    private void UseToolGrid()
    {
        if (selectable == true)
        {
            TileBase tileBase = tileMapeReadcontroller.GetTileBase(selectedTilePosition);
            TileData tileData = tileMapeReadcontroller.GetTileData(tileBase);

            if (tileData != plowableTile)
            {
                return;
            }

            if (cropsManager.Check(selectedTilePosition))
            {
                cropsManager.Seed(selectedTilePosition);
            }
            else
            {
                cropsManager.Plow(selectedTilePosition);
            }
        }
    }
}
