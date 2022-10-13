using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;


    private float lookAheadX;
    private float lookAheadY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Mathf.Clamp(player.position.x + lookAheadX, minX, maxX);
        float yPos = Mathf.Clamp(player.position.y + lookAheadY, minY, maxY);
        transform.position = new Vector3(xPos, yPos, transform.position.z);
        lookAheadX = Mathf.Lerp(lookAheadX, (xOffset * (player.localScale.x * -1)), Time.deltaTime * speed);
        lookAheadY = Mathf.Lerp(lookAheadY, yOffset, Time.deltaTime * speed);
    }
}
