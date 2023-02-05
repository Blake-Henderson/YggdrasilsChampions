using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    public float time = 1.0f;

    public void Set()
    {
        Time.timeScale = time;
    }
}
