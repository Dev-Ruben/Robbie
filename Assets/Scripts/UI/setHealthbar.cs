using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class setHealthbar : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    GameObject player;
    float currentHealth;

    

    // Start is called before the first frame update
    void Start()
    {
        setHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setHealth() {
        currentHealth = player.GetComponent<PlayerStats>().getCurrentHealth();
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString();
    }

    void OnEnable() {
        CharacterStats.onTakeDamage += setHealth;
        
    }
    void OnDisable()
    {
        CharacterStats.onTakeDamage -= setHealth;
    }
}
