using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{

    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }
    

   

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
}
