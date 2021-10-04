using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public FloatReference smoothSpeed;
    public FloatReference scrollSpeed;
    public FloatReference minCameraHeight;
    public FloatVariable maxCameraHeight;
    public FloatVariable cameraSize;
    public Vector3Variable targetPosition;
    public GameObjectRuntimeSet shapeSet;

    private float scrollAmount;
    private float scrollAmount2;


    // Start is called before the first frame update
    void Start()
    {
        this.targetPosition.Value = this.transform.position;
    }

    public void Update()
    {
        float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        if (scrollAxis != 0) {
            this.scrollAmount = scrollAxis * this.scrollSpeed;
        } else {
            float vertAxis = Input.GetAxis("Vertical");
            this.scrollAmount = vertAxis * this.scrollSpeed;
        }

        Vector3 newPos = this.targetPosition.Value;
        newPos.y = Mathf.Clamp(newPos.y + this.scrollAmount, this.minCameraHeight, Mathf.Max(this.maxCameraHeight.Value, this.minCameraHeight));
        this.targetPosition.Value = newPos;
        this.GetComponent<Camera>().orthographicSize = this.cameraSize;
    }

    // You'd think that LateUpdate would be better here, but it's jittery when the blocks topple because the camera moves down as they do so.
    void FixedUpdate()
    {
        // Find highest block to set height limit
        float highestY = float.NegativeInfinity;
        foreach (GameObject shape in this.shapeSet.Items) {
            float yPos = shape.transform.position.y;
            if (yPos > highestY) {
                highestY = yPos;
            }
        }
        this.maxCameraHeight.Value = Mathf.Max(highestY, 0);

        Vector3 smoothedPos = Vector3.Lerp(this.transform.position, this.targetPosition.Value, this.smoothSpeed * Time.deltaTime);
        this.transform.position = smoothedPos;
    }
}
