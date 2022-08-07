using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private GameObject pauseMenu;

    [HideInInspector] public GameState currentGameState;
    [HideInInspector] public GameState previousGameState;

    private GameMode gameMode;

    private List<Item> portalItems;

    [SerializeField] private GameObject player;

    [SerializeField] private List<GameObject> npcList;

    protected override void Awake()
    {
        base.Awake();

        portalItems = new List<Item>();

        //InstantiatePlayer();

    }

    void Start()
    {
        gameMode = GameMode.Normal;       
        previousGameState = GameState.GameStarted;
        currentGameState = GameState.GameStarted;        
    }

    void Update()
    {
        handleGameSate();
        Debug.Log(MapGenerator.Instance.GetBiome().GetBiomeType());
    }

    private void InstantiatePlayer() {
        //player = Instantiate(GameResources.Instance.playerPrefab);
        //CharacterSpawner.Instance.SpawnCharacter(player, 15, 15);
    }

    private IEnumerator gameOver() {
        yield return VisualManager.Instance.screenFader(0f, 1f, 4f, Color.black);
        
        string messageText = "U DIED";

        yield return StartCoroutine(VisualManager.Instance.displayMessage(messageText, Color.white, 3f));
        SceneManager.LoadScene("GameOverScreen");
    }

    public void handleGameSate() {
        switch (currentGameState) {
            case GameState.GameStarted:
                chooseBiome();
                currentGameState = GameState.FloorPlaying;
                break;

            case GameState.FloorPlaying:
                if (InputManager.Instance.getButtonDown("Escape")) {
                    pauseGameMenu();
                }
                if (InputManager.Instance.getButtonDown("Inventory")) {
                    UIManager.Instance.ToggleInventory(0);
                    switchInventoryState();
                }
                break;
            case GameState.InAction:
                break;

            case GameState.PlayerDied:
                StartCoroutine(gameOver());
                break;

            case GameState.GamePaused:
                if (InputManager.Instance.getButtonDown("Escape")) {
                    pauseGameMenu();
                }
                break;
            case GameState.FloorCompleted:       
                currentGameState = GameState.GameStarted;
                break;

        }
    }

    public void pauseGameMenu() {
        if (currentGameState != GameState.GamePaused) {
            pauseMenu.SetActive(true);
            player.SetActive(false);

            previousGameState = currentGameState;
            currentGameState = GameState.GamePaused;
        }
        else if (currentGameState == GameState.GamePaused) {
            pauseMenu.SetActive(false);
            player.SetActive(true);

            currentGameState = previousGameState;
            previousGameState = GameState.GamePaused;
        }
    }

    public void switchInventoryState()
    {
        switch (UIManager.Instance.GetInventoryState())
        {
            case InventoryState.Open:
                player.GetComponent<Movement>().enabled = false;
                player.GetComponent<PlayerAttack>().enabled = false;
                player.GetComponent<PlayerBreak>().enabled = false;
                player.GetComponent<PlayerAction>().enabled = false;
                player.GetComponent<Dash>().enabled = false;
                break;
            case InventoryState.Closed:
                player.GetComponent<Movement>().enabled = true;
                player.GetComponent<PlayerAttack>().enabled = true;
                player.GetComponent<PlayerBreak>().enabled = true;
                player.GetComponent<PlayerAction>().enabled = true;
                player.GetComponent<Dash>().enabled = true;
                break;
        }
    }

    public void switchActionableState()
    {
        switch (UIManager.Instance.GetInventoryState())
        {
            case InventoryState.Open:
                player.GetComponent<Movement>().enabled = false;
                player.GetComponent<PlayerAttack>().enabled = false;
                player.GetComponent<PlayerBreak>().enabled = false;
                player.GetComponent<Dash>().enabled = false;
                break;
            case InventoryState.Closed:
                player.GetComponent<Movement>().enabled = true;
                player.GetComponent<PlayerAttack>().enabled = true;
                player.GetComponent<PlayerBreak>().enabled = true;
                player.GetComponent<Dash>().enabled = true;
                break;
        }
    }

    public void chooseBiome(){
        switch(this.gameMode){
            case GameMode.Normal:
            MapGenerator.Instance.GetBiome().setRandomBiomeType();
            MapGenerator.Instance.GetBiome().setTileData();
            MapGenerator.Instance.RefillMap();
            break;
            case GameMode.Random:
            MapGenerator.Instance.GetBiome().setRandomBiomeType();
            MapGenerator.Instance.GetBiome().setTileData();
            MapGenerator.Instance.RefillMap();
            break;
        }
    }

    public GameObject getPlayer() {
        return player;
    }

    public List<Item> getPortalItems() {
        return portalItems;
    }
    public void AddToNpcList(GameObject npc){
        npcList.Add(npc);
    }
    public GameObject GetNpc(int id){
        return npcList[id];
    }

    public void setGameMode(GameMode mode){
        this.gameMode = mode;
    }
}
