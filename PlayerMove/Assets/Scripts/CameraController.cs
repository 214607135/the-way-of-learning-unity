using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player is the capsule
    public Transform player;
    // get mouse X and Y movement
    private float mouseX, mouseY;
    public float sensitivity;
    public float xRotation;
    private void Update() {
        // Time.deltaTime is how many seconds for one frame
        // so mouseX is horizontal movement, used for camera vision
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        // Vector3.up = [0, 1, 0], so this is used for Y orient rotate.
        player.Rotate(Vector3.up * mouseX);
        // localRotation: own rotation related to the parent, in this case, parent is player
        // Quaternion.Euler is used for rotate for (x, y, z)
        // Question: why just for x?????
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
