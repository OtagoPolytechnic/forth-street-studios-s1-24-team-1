using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;
    private int playerX;
    private int playerZ; 
    public static Vector2 gridPosition;

    public static bool isHovering = false; 

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if(Time.timeScale == 1) //Checks if the game is unpaused. Otherwise the player can still place down buildings while the pause menu is active.
        {
            CheckMouseHover();
        }
    }

    void CheckMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hoveredTile = hit.collider.gameObject;
            //All grid planes are given the "Tile" tag, checks if the player is hovering over one
            if (hoveredTile.CompareTag("Tile"))
            {
                isHovering = true;
                //The tile gameobject names are automatically set   
                //grabs only the x and z position from the name
                string[] tileNameParts = hoveredTile.name.Split('_');
                playerX = int.Parse(tileNameParts[1]);
                playerZ = int.Parse(tileNameParts[2]);

                
                gridPosition = new Vector2(playerX, playerZ);    
                SetTileAtPlayerPosition();

            }else{
                isHovering = false;
                foreach (GameObject tile in GridCreator.tiles)
                {
                    tile.GetComponent<Renderer>().material = GridCreator.Instance.defaultMaterial;
                }
            }
        }
    }

    void SetTileAtPlayerPosition()
    {
        //sets all tiles to unselected
        foreach (GameObject tile in GridCreator.tiles)
        {
            tile.GetComponent<Renderer>().material = GridCreator.Instance.defaultMaterial;
        }

        //Checks if player's x,y grid position is within bounds
        if (gridPosition.x >= 0 && gridPosition.x < GridManager.Instance.gridSize && gridPosition.y >= 0 && gridPosition.y < GridManager.Instance.gridSize)
        {
            int index = GridManager.GetTileIndex(gridPosition);

            //Change tile material to highlighted
            GridCreator.tiles[index].GetComponent<Renderer>().material = GridCreator.Instance.highlightedMaterial;
        }
    }
}
