//Author:Blake Henderson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject
{
    public int hp;
    public int attack;
    public int defense;
    public int speed;
    public int eveade;

    public void raiseStat(int input, int ammount)
    {
        switch (input)
        {
            case 0:
                hp += ammount;
                break;
            case 1:
                attack += ammount;
                break;
            case 2:
                defense += ammount;
                break;
            case 3:
                speed += ammount;
                break;
            case 4:
                eveade += ammount;
                break;
            default:
                Debug.Log("Error unknown stat");
                break;
        }
    }

    public int getRenown()
    {
        return hp + attack + defense + speed + eveade;
    }
}
