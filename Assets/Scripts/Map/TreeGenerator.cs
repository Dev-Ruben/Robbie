using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    GameObject tree;
    GameObject treeFolder;
    Layering layer;

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

    public void PlaceTree(int x, int y)
    {
        GameObject treeTile = Instantiate(tree);

        //setLayer(treeTile, x, y);

        treeTile.name = ("(" + x + ", " + y + ")");

        treeTile.transform.parent = treeFolder.transform;

        treeTile.transform.position = new Vector3(x * pixelWidth, y * pixelWidth + (pixelWidth * 0.75f));

        layer = gameObject.GetComponent<Layering>();
        layer.SetLayer(treeTile , treeTile.transform.position.y);

    }

    private void LoadResources()
    {
        tree = (GameObject)Resources.Load("Tree");
    }

    public void CreateFolder()
    {        
        treeFolder = new GameObject();

        treeFolder.name = "Tree";

    }

    public GameObject getFolder() {
        return treeFolder;
    }
}

