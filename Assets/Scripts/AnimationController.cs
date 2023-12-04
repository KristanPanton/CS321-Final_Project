using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        //RuntimeAnimatorController rac = anim.runtimeAnimatorController;

        //check if scene is loaded
        var curScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log(curScene);
        if (curScene == "Win")
        {
            anim.Play("Walk");
        }
        if (curScene == "Lose")
        {
            Debug.Log("Lose animation");
            anim.Play("Scare");
        }
        if (curScene == "MainMenu")
        {
            anim.Play("ChainsawIdle");
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}