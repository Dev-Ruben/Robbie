using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    private BiomeType biomeType;
    private Boss boss;
    private TileData tileData;

    public BiomeType GetBiomeType(){
        return biomeType;
    } 

    public void setBiomeType(BiomeType newBiomeType){
        biomeType = newBiomeType;
    }

    public void setRandomBiomeType(){
        biomeType = (BiomeType)Random.Range(0, System.Enum.GetValues(typeof(BiomeType)).Length);
    }

    public TileData getTileData(){
        return tileData;
    } 

    public void setTileData(){
        switch(this.biomeType){
            case BiomeType.Hills:
            tileData = Resources.Load<TileData>("BiomeData/HillsBiomeTileData");
            break;
            case BiomeType.Rock:
            tileData = Resources.Load<TileData>("BiomeData/RockBiomeTileData");
            break;
            case BiomeType.Cave:
            tileData = Resources.Load<TileData>("BiomeData/RockBiomeTileData");
            break;
            case BiomeType.Forest:
            tileData = Resources.Load<TileData>("BiomeData/HillsBiomeTileData");
            break;

        }
    } 
}
