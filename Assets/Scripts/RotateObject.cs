using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    // Rotation speed around each axis
    private Vector3 rotationSpeed = new Vector3(0, 0, 10);

    // Initial X and Y positions
    private float initialX;
    private float initialY;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial X and Y positions
        initialX = transform.position.x;
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object based on rotation speed and time
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Freeze the X and Y positions
        transform.position = new Vector3(initialX, initialY, transform.position.z);
    }
}
