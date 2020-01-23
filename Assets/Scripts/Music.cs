using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    // Use this for initialization
    private static GameObject music;
    protected void Awake()
    {   
        DontDestroyOnLoad(gameObject);
        if (music == null)
            music = gameObject;
        else
            Destroy(gameObject);
    }
}
