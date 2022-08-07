using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject quitButton;
    public GameObject returnToMainMenuButton;

    private bool isCreditsLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        returnToMainMenuButton.SetActive(false);
    }

    public void startGame() {
        SceneManager.LoadScene("Floor 1");
    }

    public void loadMainMenu() {
        returnToMainMenuButton.SetActive(false);

        if (isCreditsLoaded) {
            SceneManager.UnloadSceneAsync("CreditsScreen");

            isCreditsLoaded = false;
        }

        playButton.SetActive(true);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);

        
    }

    public void loadCredits() {
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        quitButton.SetActive(false);

        isCreditsLoaded = true;

        returnToMainMenuButton.SetActive(true);

        SceneManager.LoadScene("CreditsScreen", LoadSceneMode.Additive);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
