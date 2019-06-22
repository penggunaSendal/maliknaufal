using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundLoop : MonoBehaviour
{

    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

}
