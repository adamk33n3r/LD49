using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fire Bomb")]
public class FireBombAbility : Ability
{
    public float radius;
    public Sprite shapeSprite;
    public Sprite redCircleSprite;
    public float spriteColorAlpha;

    private GameObject go;

    public override void Start()
    {
        // this.go = new GameObject();
        // SpriteRenderer goSpriteRenderer = this.go.AddComponent<SpriteRenderer>();
        // goSpriteRenderer.sprite = this.redCircleSprite;
        // goSpriteRenderer.color = new Color(1, 1, 1, this.spriteColorAlpha);
        // this.go.transform.localScale = Vector3.one * this.radius;
    }

    public override void Activate(GameObject placeholder)
    {
        base.Activate(placeholder);
        Vector3 worldPos = placeholder.transform.position;
        RaycastHit2D[] spawnableHits = Physics2D.CircleCastAll(worldPos, this.radius, Vector2.zero, 0, LayerMask.GetMask("Spawnable Objects"));
        RaycastHit2D[] frozenHits = Physics2D.CircleCastAll(worldPos, this.radius, Vector2.zero, 0, LayerMask.GetMask("Frozen Objects"));
        //RaycastHit2D[] hits = Physics2D.CircleCastAll(worldPos, this.radius, Vector2.zero, 0, LayerMask.GetMask("Spawnable Objects"));
        foreach (RaycastHit2D hit in spawnableHits) {
            Destroy(hit.collider.gameObject);
        }
        foreach (RaycastHit2D hit in frozenHits) {
            Rigidbody2D hitRigidbody = hit.collider.gameObject.AddComponent<Rigidbody2D>();
            hitRigidbody.mass = 0.1F;
            hit.collider.transform.GetComponentInParent<Rigidbody2D>().mass -= hitRigidbody.mass;
            hit.collider.gameObject.layer = LayerMask.NameToLayer("Spawnable Objects");
            hit.collider.transform.parent = null;
            hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = this.shapeSprite;
        }
    }
    // public override void Update()
    // {
    //     base.Update();
    //     if (this.go == null) {
    //         return;
    //     }
    //     Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     this.go.transform.position = worldPos;
    // }
}
