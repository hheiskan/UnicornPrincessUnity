using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D col;

    //private bool grounded;
    private int jumpCount;

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private GameObject dustPrefab;
    [SerializeField] private GameObject winPrefab;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip diamondSound;
    [SerializeField] private AudioClip winSound;

    bool freeze = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze) return;

        float horizontal = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);

        if (horizontal < -0.01f)
        {
            transform.localScale = Vector3.one;
        } else if (horizontal > 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }

        animator.SetBool("run", horizontal != 0);
        animator.SetBool("jump", body.velocity.y > 0.12f);
        animator.SetBool("fall", body.velocity.y < -0.12f);
    }

    private void jump()
    {
        //Debug.Log("Jump count " + jumpCount);
        bool grounded = isGrounded();
        if (grounded || (jumpCount == 1))
        {
            if (grounded) jumpCount = 0;
            jumpCount++;
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            grounded = false;
            GameObject dust = Instantiate(dustPrefab, new Vector3(transform.position.x, transform.position.y - 0.2f, 0), Quaternion.identity);
            SoundManager.instance.PlaySound(jumpSound);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        if (hit.collider == null) return false;
        Debug.Log("Hit: " + hit.collider.gameObject);
        return hit.collider.gameObject.tag == "Ground";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(diamondSound);
        Destroy(collision.gameObject);

        if (GameObject.FindGameObjectsWithTag("Collectible").Length == 1)
        {
            Debug.Log("TODO: WIN");
            body.constraints = RigidbodyConstraints2D.FreezePosition;
            animator.enabled = false;
            freeze = true;
            SoundManager.instance.StopAll();
            GameObject dust = Instantiate(winPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            StartCoroutine(EndJingle(0.2f, 5f));
        }
    }

    IEnumerator EndJingle(float time, float time2)
    {
        yield return new WaitForSeconds(time);
        SoundManager.instance.PlaySound(winSound);
        StartCoroutine(Reload(time2));
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex < 2)
        {
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
