using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : SingletonMonobehaviour<CharacterSpawner>
{
    float pixelWidth;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        pixelWidth = GetComponent<MapGenerator>().pixelWidth;
    }

    public void SpawnCharacter(GameObject character,float x, float y)
    {
        character.transform.position = new Vector3(x * pixelWidth, y * pixelWidth);
    }
}
