using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;
    public int playerX;
    public int playerZ; 
    public static int gridPosition;

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

                
                gridPosition = playerX * GridManager.Instance.gridSize + playerZ;    
                SetTileAtPlayerPosition();
                //Debug.Log(GridManager.IsTileEmpty(MouseManager.gridPosition));

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
        if (playerX >= 0 && playerX < GridManager.Instance.gridSize && playerZ >= 0 && playerZ < GridManager.Instance.gridSize)
        {
            int index = playerX * GridManager.Instance.gridSize + playerZ;

            //Change tile material to highlighted
            GridCreator.tiles[index].GetComponent<Renderer>().material = GridCreator.Instance.highlightedMaterial;
        }
    }
}
