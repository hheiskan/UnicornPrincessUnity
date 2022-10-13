using UnityEngine;

public class Stars : MonoBehaviour
{
    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3 (cam.position.x / 2, cam.position.y / 2, 0f);
    }
}
