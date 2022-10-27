using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScrolling : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(0,0);
    [SerializeField] Vector2Int playerTilePosition;
    Vector2Int onTileGridPlayerPosition; 
    [SerializeField] float tileSize = 20f;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;
    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;
    
    private void Awake() {
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }

    private void Update() {
        playerTilePosition.x = (int) (playerTransform.position.x / tileSize);
        playerTilePosition.y = (int) (playerTransform.position.y / tileSize);
        if(currentTilePosition != playerTilePosition) { 
            currentTilePosition = playerTilePosition;  
            
            onTileGridPlayerPosition.x = CalculatePositionOnAxis(onTileGridPlayerPosition.x, true);
            onTileGridPlayerPosition.y = CalculatePositionOnAxis(onTileGridPlayerPosition.y, false);
            UpdateTileOnScren();
        }
    }

    private void UpdateTileOnScren() {
        for(int pov_x = 0; pov_x < fieldOfVisionWidth; pov_x ++) { 
            for (int pov_y = 0; pov_y < fieldOfVisionHeight; pov_y++) {
                int tileToUpdate_x = CalculatePositionOnAxis(playerTilePosition.x + pov_x, true);
                int tileToUpdate_y = CalculatePositionOnAxis(playerTilePosition.y + pov_y, true);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];
                tile.transform.position = CalculateTilePosition(playerTilePosition.x + pov_x, playerTilePosition.y + pov_y);        
            } 
        }            
    }

    private Vector3 CalculateTilePosition(int x, int y) {
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }

    private int CalculatePositionOnAxis(float currentValue, bool horizontal) {

        if(horizontal) {
            if(currentValue > 0) {
                currentValue = currentValue % terrainTileHorizontalCount;
            }else {
                 currentValue = terrainTileHorizontalCount -1 + currentValue % terrainTileHorizontalCount;

            }
        } else { 
            if (currentValue > 0)  {
            currentValue = currentValue % terrainTileVerticalCount;
            }else {
                currentValue = terrainTileVerticalCount -1 + currentValue % terrainTileVerticalCount;
            }
        }
        return (int) currentValue;
    }

     public void Add(GameObject tileGameObject, Vector2Int tilePosition) {
        terrainTiles [tilePosition.x, tilePosition.y] = tileGameObject;
    }
}

