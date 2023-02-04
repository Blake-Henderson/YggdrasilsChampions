using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script exist to reset cut-in animation to ensure it runs good
public class CutInAnim : MonoBehaviour
{
    //These are the bust backgrounds 
    public GameObject obj1;
    public GameObject obj2;

    //Will check if object is active and set the animation of the bust backgrounds accordingly
    void OnEnable()
    {
            obj1.GetComponent<Animator>().SetTrigger("Awake");
            obj2.GetComponent<Animator>().SetTrigger("Awake");
    }
}
