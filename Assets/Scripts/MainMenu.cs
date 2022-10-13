using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip menuSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.instance.PlaySound(menuSound);
            animator.speed = 3;
            StartCoroutine(WaitAndLoad(2f));
        }
    }

    IEnumerator WaitAndLoad(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Level1");
    }
}