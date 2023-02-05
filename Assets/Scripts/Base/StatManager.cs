using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public Stats baseStats;
    public int hp;
    public int health;
    public int attack;
    public int tempAttack;
    public int defense;
    public int speed;
    public int evade;

    public bool canRun = false;

    private void Start()
    {
        hp = baseStats.hp;
        health = hp;
        attack = baseStats.attack;
        tempAttack = attack;
        defense = baseStats.defense;
        speed = baseStats.speed;
        evade = baseStats.evade;
    }
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
                evade += amount;
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
        return hp + attack + defense + speed + evade;
    }
}
