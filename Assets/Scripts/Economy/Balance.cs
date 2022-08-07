using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance
{
    private int id;
    private int balance;

    public void Create() { balance = 0; }
    public void SetID(int balanceID){id = balanceID;}
    public int GetID(){ return id;}
    public int GetBalance() { return balance; }
    public void Add(int coinAmount) { balance += coinAmount; }
    public void Remove(int coinAmount) { balance -= coinAmount; }

}
