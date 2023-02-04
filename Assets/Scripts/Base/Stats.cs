//Author:Blake Henderson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject
{
    public string title;
    public Sprite sprite;

    public int hp;
    public int health;
    public int attack;
    public int defense;
    public int speed;
    public int evade;
}
