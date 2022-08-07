using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor : SingletonMonobehaviour<Floor>
{
    private int floorNumber = 1;
    private Scene currentFloor;
    private Scene nextFloor;


    // Start is called before the first frame update
    void Start()
    {
        currentFloor = SceneManager.GetActiveScene();
        createNewFloor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            {             
                GameManager.Instance.currentGameState = GameState.FloorCompleted;
                loadNewFloor();
            }
        if (MapGenerator.Instance.spawnGenerator.portal.PortalIsOpen())
        {
            
        }
        
    }

    private void createNewFloor() {       
        floorNumber++;
        string newfloorName = "Floor " + floorNumber;
        nextFloor = SceneManager.CreateScene(newfloorName);

        
    }

    public void loadNewFloor() {
        currentFloor = SceneManager.GetActiveScene();
        deleteFloor();
        SceneManager.MoveGameObjectToScene(GameObject.Find("NeededObjects"), nextFloor);
        
        MapGenerator.Instance.deleteMap();
        createNewFloor();
               
    }

    private void deleteFloor() {
        SceneManager.UnloadSceneAsync("Floor " + (floorNumber - 1));
    }
    
    public int getFloorNumber() {
        return floorNumber - 1;
    }
}
