using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNPCForFight : MonoBehaviour
{
    public static GetNPCForFight instance;
    private void Start()
    {
        instance = this;
    }
    public Character GetCharacter()
    {
        Character temp = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Character>();
        temp.stats.hp = temp.stats.baseStats.hp;
        temp.stats.health = temp.stats.baseStats.hp;
        temp.stats.attack = temp.stats.baseStats.attack;
        temp.stats.defense = temp.stats.baseStats.defense;
        temp.stats.speed = temp.stats.baseStats.speed;
        temp.stats.evade = temp.stats.baseStats.evade;
        return temp;
    }
}
