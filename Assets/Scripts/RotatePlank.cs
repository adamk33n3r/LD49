using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlank : MonoBehaviour
{
    public FloatVariable torqueSpeed;
    public FloatVariable plankRotation;
    public FloatVariable plankAngleLimit;
    public FloatVariable plankLength;
    public Vector3Variable plankExtents;
    public BoolReference isGamePaused;

    private Rigidbody2D plankObjRigidBody;
    private HingeJoint2D plankObjHingeJoint;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private float torqueVal;
    private float startingPlankWidth;
    void Start()
    {
        this.plankObjHingeJoint = GetComponent<HingeJoint2D>();
        this.plankObjRigidBody = GetComponent<Rigidbody2D>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.plankAngleLimit.Value = GetComponent<HingeJoint2D>().limits.max;
        this.startingPlankWidth = this.transform.localScale.x;
    }

    void Update()
    {
        if(this.isGamePaused) {
            return;
        }
        this.plankRotation.Value = this.plankObjHingeJoint.jointAngle;
        this.torqueVal = Input.GetAxis("Horizontal");
        this.plankExtents.Value = this.boxCollider2D.bounds.extents;
        Vector2 newSize = new Vector2();
        newSize.x = plankLength;
        newSize.y = this.spriteRenderer.size.y;
        this.spriteRenderer.size = newSize;
        this.boxCollider2D.size = newSize;

    }

    void FixedUpdate()
    {
        this.plankObjRigidBody.AddTorque(-this.torqueVal * this.torqueSpeed.Value);
    }

    public void ScalePlankWidth(float scale)
    {
        Vector3 currentTransform = this.transform.localScale;
        currentTransform.x = startingPlankWidth * scale;
        this.transform.localScale = currentTransform;
    }

    public void SetPlankTorque(float val)
    {
        this.torqueSpeed.Value = val;
    }

    public void SetRotation(float val)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, val);
        this.plankRotation.Value = val;
        this.plankObjRigidBody.angularVelocity = 0;
    }
}
