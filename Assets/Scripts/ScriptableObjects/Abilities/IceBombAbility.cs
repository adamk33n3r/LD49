using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Ice Bomb")]
public class IceBombAbility : Ability
{
    public float radius;
    public Sprite iceSprite;
    public Sprite blueCircleSprite;
    public float spriteColorAlpha;

    private GameObject go;

    public override void Start()
    {
        // this.go = new GameObject();
        // SpriteRenderer goSpriteRenderer = this.go.AddComponent<SpriteRenderer>();
        // goSpriteRenderer.sprite = this.blueCircleSprite;
        // goSpriteRenderer.color = new Color(1, 1, 1, this.spriteColorAlpha);
        // this.go.transform.localScale = Vector3.one * this.radius;
    }

    public override bool CanActivate(GameObject placeholder)
    {
        return base.CanActivate(placeholder) && Physics2D.BoxCast(placeholder.transform.position, placeholder.transform.localScale, 0, Vector2.zero, 0, LayerMask.GetMask("Spawnable Objects", "Frozen Objects")).collider != null;
    }
    public override void Activate(GameObject placeholder)
    {
        base.Activate(placeholder);
        Vector3 worldPos = placeholder.transform.position;
        RaycastHit2D[] spawnableHits = Physics2D.CircleCastAll(worldPos, this.radius, Vector2.zero, 0, LayerMask.GetMask("Spawnable Objects"));
        RaycastHit2D[] frozenHits = Physics2D.CircleCastAll(worldPos, this.radius, Vector2.zero, 0, LayerMask.GetMask("Frozen Objects"));

        HashSet<GameObject> parentObjects = new HashSet<GameObject>();
        foreach (RaycastHit2D hit in frozenHits) {
            parentObjects.Add(hit.collider.transform.parent.gameObject);
        }
        GameObject newParentObj = new GameObject("Ice Block");
        newParentObj.transform.position = worldPos;
        Rigidbody2D parentRigidBody = newParentObj.AddComponent<Rigidbody2D>();
        parentRigidBody.mass = 0;
        foreach (GameObject parentObj in parentObjects) {
            // Need to store here because setting the children parents updates the value
            int childCount = parentObj.transform.childCount;
            for (int i = 0; i < childCount; i++) {
                Transform childObj = parentObj.transform.GetChild(0);
                childObj.parent = newParentObj.transform;
            }
            parentRigidBody.mass += parentObj.GetComponent<Rigidbody2D>().mass;
            Destroy(parentObj);
        }
        foreach (RaycastHit2D hit in spawnableHits) {
            parentRigidBody.mass += hit.rigidbody.mass;
            // collider points to exact hit, transform points to parent always
            hit.collider.gameObject.layer = LayerMask.NameToLayer("Frozen Objects");
            hit.collider.gameObject.transform.parent = newParentObj.transform;
            hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = this.iceSprite;
            Destroy(hit.rigidbody);
        }
    }

    // public override void Update()
    // {
    //     base.Update();
    //     if (this.go == null) {
    //         //Debug.LogError("error");
    //         return;
    //     }
    //     Debug.LogError("yay?");
    //     Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     this.go.transform.position = worldPos;
    // }
}
