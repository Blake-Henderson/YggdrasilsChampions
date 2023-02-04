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
        return transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Character>();
    }
}
