using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetYggdrasil : MonoBehaviour
{
    public static GetYggdrasil instance;
    public Character character;

    private void Start()
    {
        instance = this;
    }
}
