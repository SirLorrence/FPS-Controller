using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float minX = -60f;
    public float maxX = 60f;
    public float minY = -360;
    public float maxY = 360;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public Camera cam;
    public GameObject player;

    private float rotationY = 0f;
    private float rotationX = 0f;
    private Vector3 offset;

    private void Awake()
    {
        offset = cam.transform.position - player.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        rotationY += Input.GetAxis("Mouse X") * sensitivityY;
        rotationX += Input.GetAxis("Mouse Y") * sensitivityX;

        rotationX = Mathf.Clamp(rotationX, minX, maxX);

        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        cam.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

    }

    private void LateUpdate()
    {
        cam.transform.position = player.transform.position + offset;
    }

}
