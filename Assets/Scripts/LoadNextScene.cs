using TMPro;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
{
    public string nextScene;
    private int count = 0;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
            {
                var tmp = GameObject.Find("Instructions").GetComponent<TextMeshProUGUI>();
                var insPlane = GameObject.Find("InstructionsPlane").GetComponent<MeshRenderer>();
                var insSkull = GameObject.Find("InstructionsSkull").GetComponent<MeshRenderer>();
                var title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
                switch (count)
                {
                    case 0:
                        tmp.enabled = true;
                        insPlane.enabled = true;
                        insSkull.enabled = true;
                        title.enabled = false;
                        count++;
                        break;

                    case 1:
                        count = 0;
                        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
                        break;
                }
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
            }
        }
    }
}