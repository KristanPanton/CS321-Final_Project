using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int AICount = 5;
    //public GameObject AIPrefab;
    public List<GameObject> AIObjectList;

    void Start()
    {
        for (int i = 0; i < AICount; i++)
        {
            var pos = GetPositionForAI();
            var ai = Instantiate(AIObjectList[UnityEngine.Random.Range(0, AIObjectList.Count)], pos, Quaternion.identity);
            ai.transform.LookAt(Vector3.zero);
        }
    }

    Vector3 GetPositionForAI()
    {
        for (int i = 0; i < 100; i++)
        //while (true)
        {
            var pos = new Vector3(UnityEngine.Random.Range(-20, 20), 1, UnityEngine.Random.Range(-20, 20));
            //Debug.Log("X: " + pos.x + "\tZ: " + pos.z);
            // make cube half the height to not overlap with the ground, causing a endless loop
            var colliders = Physics.OverlapBox(pos, new Vector3(0, 0.5f, 1));
            //Debug.Log("Colliders: " + colliders.Length);
            if (colliders.Length == 0)
            {
                Debug.Log("Found position for AI");
                return pos;
            }
        }
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
