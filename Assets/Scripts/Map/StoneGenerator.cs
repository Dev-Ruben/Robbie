using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : MonoBehaviour
{
    public GameObject stone;
    GameObject stoneFolder;

    float pixelWidth;

    // Start is called before the first frame update
    void Start()
    {
        pixelWidth = GetComponent<MapGenerator>().pixelWidth;
        LoadResources();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setLayer(GameObject gameObject, int x, int y)
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = x * 2 - y * 2;
    }

    public void PlaceStone(int x, int y)
    {
        GameObject stoneTile = Instantiate(stone);

        setLayer(stoneTile, x, y);

        stoneTile.name = ("(" + x + ", " + y + ")");

        stoneTile.transform.parent = stoneTile.transform;

        stoneTile.transform.position = new Vector3(x * pixelWidth, y * pixelWidth + (pixelWidth * 0.75f));

    }

    private void LoadResources()
    {
        stone = (GameObject)Resources.Load("Stone");
    }

    public void CreateFolder()
    {
        stoneFolder = new GameObject();

        stoneFolder.name = "Stone";

    }

    public GameObject getFolder()
    {
        return stoneFolder;
    }
}
