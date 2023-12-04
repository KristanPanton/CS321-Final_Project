using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterAxis : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(-20, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
