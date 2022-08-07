using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : SingletonMonobehaviour<MapGenerator>
{
    private Biome biome;
    public int[,] map;
    public List<Vector2> mapCoordinates;
    public List<Vector2> mapInner;
    private List<Vector2> mapOccupied;

    private readonly int rows = 30;
    private readonly int columns = 30;
    public readonly float pixelWidth = 0.32f;
    int mapOccupyCount = 0;

    public long seed;
    public bool useRandomSeed;
    private System.Random pseudoNumber;

    GameObject mapFolder;
    GameObject sideFolder;
    GameObject outerFolder;
    GameObject innerFolder;

    TreeGenerator treeGenerator;
    public SpawnGenerator spawnGenerator;
    StoneGenerator stoneGenerator;

    Layering layer;

    GameObject mapGameobject;



    // Start is called before the first frame update
    void Start()
    {
        biome = GetComponent<Biome>();
        treeGenerator = GetComponent<TreeGenerator>();
        spawnGenerator = GetComponent<SpawnGenerator>();
        stoneGenerator = GetComponent<StoneGenerator>();

        layer = gameObject.GetComponent<Layering>();
    }

    public void RefillMap() {
        SetSeed();

        CreateFolders();
        CreateMap();
        GenerateTiles();
        SetCorners();
        SetInnerCorners();
        SetSides();

        CalculateInnerLand();
        createSpawnArea();
        SpawnTreesAndRocks();
        //SpawnPlayer();
        //GetOccupiedCoordinates();
    }

    public void deleteMap() {

        mapGameobject = GameObject.Find("Map");


        Destroy(mapGameobject);

        StartCoroutine(DeletingMap(0.1f));
    }

    IEnumerator DeletingMap(float time)
    {
        yield return new WaitForSeconds(time);

        StopCoroutine("DeletingMap");
    }

    void CreateMap()
    {
        map = new int[rows, columns];
        mapCoordinates = new List<Vector2>();
        mapInner = new List<Vector2>();
        mapOccupied = new List<Vector2>();
        RandomFillMap();

        for (int x = 0; x < 10; x++)
        {
            SmoothMap();
        }

    }

    private int GetTileType(int gridX, int gridY) {
        return map[gridX, gridY];
    }

    void SetSeed() {
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.Ticks;
        }

        pseudoNumber = new System.Random(seed.GetHashCode());

    }

    void RandomFillMap()
    {

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (row == 0 || row == rows - 1 || column == 0 || column == columns - 1)
                {
                    map[row, column] = 1;
                }
                else {
                    map[row, column] = (pseudoNumber.Next(0, 100) < Random.Range(25, 48)) ? 1 : 0;
                    mapCoordinates.Add(new Vector2(row, column));
                }
                    
            }
        }

    }

    void SmoothMap()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < rows && neighbourY >= 0 && neighbourY < columns)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    void CalculateInnerLand()
    {

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (GetTileType(row, column) == 0)
                {
                    if (row >= 2 && row < rows - 2 && column >= 2 && column < columns - 2)
                    {
                        if (GetTileType(row - 1, column) == 0
                            && GetTileType(row + 1, column) == 0
                            && GetTileType(row, column - 1) == 0
                            && GetTileType(row, column + 1) == 0
                            && GetTileType(row + 1, column + 1) != 1
                            && GetTileType(row - 1, column - 1) != 1
                            && GetTileType(row + 1, column - 1) != 1
                            && GetTileType(row - 1, column + 1) != 1)
                        {
                            mapInner.Add(new Vector2(row, column));
                        }
                    }
                }
            }
        }
    }

    void SetCorners()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (x >= 1 && x < rows - 1 && y >= 1 && y < columns - 1)
                {
                    if (GetTileType(x, y) == 0)
                    {
                        if (GetTileType(x + 1, y) == 1
                        && GetTileType(x - 1, y) == 0
                        && GetTileType(x, y + 1) == 1
                        && GetTileType(x, y - 1) == 0)
                        {

                           PlaceTile(biome.getTileData().getTileObject(TileType.CornerTopRight), innerFolder, x, y);
                        }

                        if (GetTileType(x + 1, y) == 0
                        && GetTileType(x - 1, y) == 1
                        && GetTileType(x, y + 1) == 1
                        && GetTileType(x, y - 1) == 0)
                        {

                           PlaceTile(biome.getTileData().getTileObject(TileType.CornerTopLeft), innerFolder, x, y);
                        }

                        if (GetTileType(x + 1, y) == 1
                        && GetTileType(x - 1, y) == 0
                        && GetTileType(x, y + 1) == 0
                        && GetTileType(x, y - 1) == 1)
                        {

                            PlaceTile(biome.getTileData().getTileObject(TileType.CornerDownRight), innerFolder, x, y);
                        }

                        if (GetTileType(x + 1, y) == 0
                        && GetTileType(x - 1, y) == 1
                        && GetTileType(x, y + 1) == 0
                        && GetTileType(x, y - 1) == 1)
                        {

                            PlaceTile(biome.getTileData().getTileObject(TileType.CornerDownLeft), innerFolder, x, y);
                        }


                    }
                }

            }
        }
    }

    void SetInnerCorners()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (x >= 1 && x < rows - 2 && y >= 1 && y < columns - 2)
                {
                    if (GetTileType(x, y) == 0)
                    {
                        if (GetTileType(x + 1, y + 1) == 1
                            && GetTileType(x + 1, y) == 0
                            && GetTileType(x, y + 1) == 0)
                        {
                            PlaceTile(biome.getTileData().getTileObject(TileType.InnerCornerTopRight), innerFolder, x, y);
                        }

                        if (GetTileType(x - 1, y - 1) == 1
                            && GetTileType(x - 1, y) == 0
                            && GetTileType(x, y - 1) == 0)
                        {
                            PlaceTile(biome.getTileData().getTileObject(TileType.InnerCornerDownLeft), innerFolder, x, y);

                        }

                        if (GetTileType(x - 1, y + 1) == 1
                            && GetTileType(x - 1, y) == 0
                            && GetTileType(x, y + 1) == 0)
                        {
                            PlaceTile(biome.getTileData().getTileObject(TileType.InnerCornerTopLeft), innerFolder, x, y);

                        }

                        if (GetTileType(x + 1, y - 1) == 1
                            && GetTileType(x + 1, y) == 0
                            && GetTileType(x, y - 1) == 0)
                        {
                            PlaceTile(biome.getTileData().getTileObject(TileType.InnerCornerDownRight), innerFolder, x, y);
                            
                        }

                    }
                }

            }
        }
    }

    void SetSides()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (x >= 1 && x < rows - 1 && y >= 1 && y < columns - 1)
                {
                    if(GetTileType(x, y) == 0) 
                    { 
                        if (GetTileType(x + 1, y) == 0
                        && GetTileType(x - 1, y) == 0
                        && GetTileType(x, y + 1) == 1
                        && GetTileType(x, y - 1) == 0)
                        {

                            PlaceTile(biome.getTileData().getTileObject(TileType.SideTop), sideFolder, x, y);

                        }

                        if (GetTileType(x + 1, y) == 0
                        && GetTileType(x - 1, y) == 0
                        && GetTileType(x, y + 1) == 0
                        && GetTileType(x, y - 1) == 1)
                        {

                           PlaceTile(biome.getTileData().getTileObject(TileType.SideDown), sideFolder, x, y);
                        }

                        if (GetTileType(x + 1, y) == 1
                        && GetTileType(x - 1, y) == 0
                        && GetTileType(x, y + 1) == 0
                        && GetTileType(x, y - 1) == 0)
                        {

                           PlaceTile(biome.getTileData().getTileObject(TileType.SideRight), sideFolder, x, y);
                        }

                        if (GetTileType(x + 1, y) == 0
                        && GetTileType(x - 1, y) == 1
                        && GetTileType(x, y + 1) == 0
                        && GetTileType(x, y - 1) == 0)
                        {

                            PlaceTile(biome.getTileData().getTileObject(TileType.SideLeft), sideFolder, x, y);
                        }


                    }
                }

            }
        }
    }

    private void GenerateTiles()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                if (map[row, column] % 2 == 1)
                {
                    PlaceTile(biome.getTileData().getTileObject(TileType.OuterBase), outerFolder, row, column);
                }

                else {
                    PlaceTile(biome.getTileData().getTileObject(TileType.InnerBase), innerFolder, row, column);
                }
            }

        }      
    }

    private void checkDuplicateTile(float x, float y)
    {

        if (mapCoordinates.Contains(new Vector2(x, y)))
        {
            
            GameObject duplicateTile = GameObject.Find("(" + x + ", " + y + ")");
            GameObject test = GameObject.Find("(" + x + ", " + y + ")");
            Destroy(test);
        }
    }

    void PlaceTile(GameObject tileType, GameObject map, int x, int y) {
        checkDuplicateTile(x, y);

        GameObject tile = Instantiate(tileType);
        tile.name = ("(" + x + ", " + y + ")");
          
        layer.SetLayer(tile, columns);

        tile.transform.parent = map.transform;

        tile.transform.position = new Vector3(x * pixelWidth, y * pixelWidth);

    }

    private bool checkOccupiedTile(float x, float y)
    {
        if (mapOccupied.Contains(new Vector2(x, y)))
        {
            return true;
        }

        return false;
    }

    void createSpawnArea() {
        spawnGenerator.createSpawn(rows, columns);

        foreach (Vector2 coordinates in spawnGenerator.GetSpawnAreaCoordinates()) {
            mapOccupied.Add(coordinates);
        }
    }


    /*void SpawnPlayer()
    {

        List<Vector2> mapInnerSorted = mapInner.OrderBy(inner => inner.x).ThenBy(inner => inner.y).ToList();
        List<Vector2> mapOccupiedSorted = mapOccupied.OrderBy(occupied => occupied.x).ThenBy(occupied => occupied.y).ToList();

        foreach (Vector2 gridInner in mapOccupiedSorted)
        {            
            foreach (Vector2 gridOccupied in mapOccupiedSorted)
            {
                if (gridInner.Equals(gridOccupied))
                {

                    mapInnerSorted.Remove(gridInner);
                }
            }
        }

        int randomSpawnPoint = Random.Range(0, mapInnerSorted.Count - 1);

        playerSpawner.SpawnPlayer(mapInnerSorted[randomSpawnPoint].x, mapInnerSorted[randomSpawnPoint].y);
    }*/

    

    void SpawnTreesAndRocks()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {

                foreach (Vector2 grid in mapInner)
                {
                    if (grid.x == row && grid.y == column)
                    {
                        if(!checkOccupiedTile(grid.x, grid.y))
                        {
                            if (pseudoNumber.Next(0, 100) < Random.Range(25, 30))
                            {
                                mapOccupied.Add(new Vector2(row, column));

                                treeGenerator.PlaceTree(row, column);

                                mapOccupyCount++;

                            }

                            /*else if (pseudoNumber.Next(0, 100) > Random.Range(75, 100))
                            {
                                mapOccupied.Add(new Vector2(row, column));

                                stoneGenerator.PlaceStone(row, column);

                                mapOccupyCount++;

                            }*/
                        }
                    }
                }
            }
        }
    }

    private void CreateFolders() {
        mapFolder = new GameObject();
        sideFolder = new GameObject();
        outerFolder = new GameObject();
        innerFolder = new GameObject();

        treeGenerator.CreateFolder();
        stoneGenerator.CreateFolder();
        spawnGenerator.CreateFolder();

        mapFolder.name = "Map";
        innerFolder.name = "Inner";
        sideFolder.name = "Sides";
        outerFolder.name = "Outer";


        treeGenerator.getFolder().transform.parent = mapFolder.transform;
        stoneGenerator.getFolder().transform.parent = mapFolder.transform;
        spawnGenerator.getFolder().transform.parent = mapFolder.transform;
        sideFolder.transform.parent = mapFolder.transform;
        outerFolder.transform.parent = mapFolder.transform;
        innerFolder.transform.parent = mapFolder.transform;        
    }

    public Biome GetBiome(){
        return biome;
    }


    public Vector2 GetMapSize()
    {
        return new Vector2(rows,columns);
    }
}
