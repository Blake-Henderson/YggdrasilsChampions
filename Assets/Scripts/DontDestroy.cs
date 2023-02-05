using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject track1;
    public GameObject track2;
    public static DontDestroy instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        track1.SetActive(true);
        track2.SetActive(false);
    }

    public void swapTracks()
    {
        track1.SetActive(track2.activeInHierarchy);
        track2.SetActive(!track1.activeInHierarchy);
    }
}
