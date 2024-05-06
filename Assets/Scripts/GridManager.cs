using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public int gridSize = 5; //width and height of grid (5x5, 9x9, etc)
    public float tileSize = 10.0f; //size each tile, shouldn't have a reason not to be 10

    
    public List<TileTypes> tileStates = new List<TileTypes>(); 
    public List<bool> tileBonus = new List<bool>(); 

    public List<TileData> tiles = new List<TileData>(); 
    //public Dictionary<TilePoints, int> tileBonus = new Dictionary<TilePoints, int>();

    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        tileStates.Clear();
        /*
        for (int i = 0; i < gridSize * gridSize; i++) {
            // Add default value to the list
            tileStates.Add(TileTypes.None);
        }*/

        tileBonus.Clear();

        
        for (int i = 0; i < GameManager.Instance.level.positions.Count; i++)
        {
            tiles.Add(new TileData(GameManager.Instance.level.positions[i], GameManager.Instance.level.tileTypes[i]));
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            Debug.Log(tiles[i].position);
        }

    }
    
    // Start is called before the first frame update
    private Vector3 onCalculatePos(float x, float z)
    {
        Vector3 position;
        float xPos = (x * tileSize) - (gridSize / 2 * tileSize) + (tileSize / 2);
        float zPos = (z * tileSize) - (gridSize / 2 * tileSize) + (tileSize / 2);
        return position = new Vector3(xPos, 1, zPos);
    }

    // Update is called once per frame
    public static Vector3 CalculatePos(float x, float z)
    {
        return Instance.onCalculatePos(x, z);
    }

    

    private bool OnIsTileEmpty(int index)
    {
        return tileStates[index] == TileTypes.None;
    }

    public static bool IsTileEmpty(int index)
    {
        return Instance.OnIsTileEmpty(index);
    }


    public static int GetTileIndex(Vector2 gridPosition)
    {
        return (int)(gridPosition.x * GridManager.Instance.gridSize + gridPosition.y);
    }

    public static Vector2 GetTilePosition(int index)
    {
        return new Vector2(index / GridManager.Instance.gridSize, index % GridManager.Instance.gridSize);

}
    public static void SetTileState(Vector2 tilePos, TileTypes tileType)
    {
        Instance.tileStates[GetTileIndex(tilePos)] = tileType;
    }
}