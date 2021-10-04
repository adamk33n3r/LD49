using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Place Shape")]
public class PlaceShapeAbility : Ability
{
    public GameObject shapeToSpawn;
    public Vector3Reference plankExtents;
    public FloatReference maxCameraHeight;
    public float shapeSize;

    public override bool CanActivate(GameObject placeholder)
    {
        return base.CanActivate(placeholder) &&
            placeholder.transform.position.y < (this.maxCameraHeight + 5) &&
            Physics2D.BoxCast(placeholder.transform.position, placeholder.transform.localScale, 0, Vector2.zero).collider == null;
    }

    public override void Activate(GameObject placeholder)
    {
        base.Activate(placeholder);
        Vector3 newScale = new Vector3(this.shapeSize, this.shapeSize, this.shapeToSpawn.transform.localScale.z);
        GameObject go = Instantiate(this.shapeToSpawn, placeholder.transform.position + Vector3.forward, Quaternion.identity);
        go.transform.localScale = newScale;
        go.GetComponent<Rigidbody2D>().mass *= this.shapeSize * this.shapeSize;
    }
}
