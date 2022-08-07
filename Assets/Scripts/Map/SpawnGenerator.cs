using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject spawnAreaTile;
    GameObject spawnAreaFolder;
    private List<Vector2> spawnAreaCoordinates;

    public Portal portal;
    public GameObject player;
    public GameObject demonMerchant;

    CharacterSpawner characterSpawner;

    Layering layer;

    int spawnRadius = 6;
    float pixelWidth;

    void Awake()
    {

        pixelWidth = GetComponent<MapGenerator>().pixelWidth;
        spawnAreaCoordinates = new List<Vector2>();

        characterSpawner = GetComponent<CharacterSpawner>();
        layer = gameObject.GetComponent<Layering>();

    }

    public void createSpawn(int row, int column)
    {

        Vector2 middleMapPoint = new Vector2(row / 2, column / 2);

        int x = 0;
        int y = spawnRadius;

        int decisionParameter = 3 - 2 * spawnRadius;

        createArea(middleMapPoint.x, middleMapPoint.y, x, y);
        while (y >= x) {
            x++;
            if (decisionParameter > 0)
            {
                y--;
                decisionParameter = decisionParameter + 4 * (x - y) + 10;
            }
            else {
                decisionParameter = decisionParameter + 4 * x + 6;
            }
            createArea(middleMapPoint.x, middleMapPoint.y, x, y);

        }

        characterSpawner.SpawnCharacter(player, middleMapPoint.x - 1, middleMapPoint.y - 2);
        characterSpawner.SpawnCharacter(demonMerchant, middleMapPoint.x + 2, middleMapPoint.y - 1);
    }

    private void createArea(float x, float y, int x0, int y0) {
        drawLine(x - x0, x + x0, y + y0);
        drawLine(x - x0, x + x0, y - y0);

        drawLine(x - y0, x + y0, y + x0);
        drawLine(x - y0, x + y0, y - x0);
    }

    private void drawLine(float x0, float x1, float y0) {
        for (float i = x0; i <= x1; i++) {
            instantiateAreaTile(i, y0);
        }
    }

    private void instantiateAreaTile(float x, float y) {
        if(!checkDuplicateTile(x, y)){
            GameObject areaTile = Instantiate(spawnAreaTile);
            areaTile.name = ("(" + x + ", " + y + ")");
            areaTile.transform.parent = spawnAreaFolder.transform;
            areaTile.transform.position = new Vector3(x * pixelWidth, y * pixelWidth);

            layer.SetLayer(areaTile, 100);

            spawnAreaCoordinates.Add(new Vector2(x, y));
        }      
    }

    private bool checkDuplicateTile(float x, float y) {
        if (spawnAreaCoordinates.Contains(new Vector2(x, y))) {
            return true;
        }

        return false;
    }

    public void CreateFolder()
    {
        spawnAreaFolder = new GameObject();

        spawnAreaFolder.name = "SpawnArea";
    }

    public GameObject getFolder()
    {
        return spawnAreaFolder;
    }

    public List<Vector2> GetSpawnAreaCoordinates() {
        return spawnAreaCoordinates;
    }
}
