using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, IActionable
{
    private int id;
    Animator chestAnimator;
    
    private void Start(){
        chestAnimator = GetComponent<Animator>();
    }

    public void FillChest(ItemData[] itemDataList)
    {
        foreach (ItemData itemData in itemDataList)
        {
            StorageManager.Instance.GetStorage(id,StorageTypes.Chest).Add(itemData,Random.Range(1,itemData.maxDropAmount));
        }
    }

    public void SetID(int chestID)
    {
        id = chestID;
        
    } 
    public int GetID()
    {
        return id;
    }
    public void UseAction()
    {
        bool state = UIManager.Instance.ToggleChest(id,0);
        if (state){
            GameManager.Instance.currentGameState = GameState.InAction;
            chestAnimator.SetBool("IsOpen", ChestStateToBool(ChestState.Open));

        }
        else{
            GameManager.Instance.currentGameState = GameState.FloorPlaying;
            chestAnimator.SetBool("IsOpen", ChestStateToBool(ChestState.Closed));
        } 
        
    }

    private bool ChestStateToBool(ChestState chestState){
        if(chestState == ChestState.Open){
            return true;
        }

        return false;
    }
}
