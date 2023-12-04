using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance { get; set; }
    public GameObject bulletImpactEffectPrefab;
    public static int numEnemies;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (numEnemies == 0)
        {
            print("You win!");
            // load next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        }
    }
}
