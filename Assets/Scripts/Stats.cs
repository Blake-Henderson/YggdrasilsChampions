//Author:Blake Henderson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject
{
    public Sprite sprite;

    public int hp;
    public int health;
    public int attack;
    public int defense;
    public int speed;
    public int eveade;

    public void raiseStat(int input, int amount)
    {
        switch (input)
        {
            case 0:
                hp += amount;
                break;
            case 1:
                attack += amount;
                break;
            case 2:
                defense += amount;
                break;
            case 3:
                speed += amount;
                break;
            case 4:
                eveade += amount;
                break;
            default:
                Debug.Log("Error unknown stat");
                break;
        }
    }


    public void heal(int amount)
    {
        if (health < hp)
        {
            health += amount;
        }
        if (health > hp)
        {
            health = hp;
        }
    }

    public void takeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            //die
        }
    }

    public int getRenown()
    {
        return hp + attack + defense + speed + eveade;
    }
}
