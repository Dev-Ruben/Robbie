using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Tile Data")]
public class TileData : ScriptableObject
{
    [SerializeField] public TileStruct[] tiles;

    public GameObject getTileObject(TileType tileType){
        foreach(TileStruct tile in tiles){
            if(tile.tileType == tileType){
                return tile.tilePrefab;
            }
        }

        return null;
    }

}

[System.Serializable]
public struct TileStruct{
    public TileType tileType;
    public GameObject tilePrefab;
}
