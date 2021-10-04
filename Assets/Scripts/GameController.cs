using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public FloatReference plankRotation;
    public FloatReference plankAngleLimit;
    public FloatReference plankLength;
    public FloatReference maxCameraHeight;
    public FloatVariable maxHeightReached;
    public GameObject squarePrefab;
    public RotatePlank plankObj;
    public ScriptableEvents.Events.SimpleScriptableEvent startRoundEvent;
    public ScriptableEvents.Events.SimpleScriptableEvent endRoundEvent;
    public GameObjectRuntimeSet shapeSet;
    public IntegerVariable destroyedShapeCount;
    public IntegerVariable playerMoney;
    public BoolVariable isGamePaused;
    public BoolReference isMainMenuDemo;
    public BoolVariable isFirstRun;

    private int spawnCounter = 1;
    private float timer = 0;

    private bool reset = false;
    private int lossCounter = 0;

    public void Start()
    {
        this.startRoundEvent.Raise();
    }

    public void Update()
    {
        if (this.reset) {
            this.reset = false;
            this.destroyedShapeCount.Value = 0;
            this.maxHeightReached.Value = 0;
            this.plankObj.SetRotation(0);
            GameObject[] array = new GameObject[this.shapeSet.Items.Count];
            this.shapeSet.Items.CopyTo(array);
            foreach (GameObject shape in array) {
                Destroy(shape);
            }
            this.isGamePaused.Value = false;
        }
        if (this.isGamePaused) {
            return;
        }
        if (!this.isMainMenuDemo && this.destroyedShapeCount.Value >= 10) {
            this.endRoundEvent.Raise();
            if (!this.isFirstRun) {
                this.playerMoney.Value += Mathf.FloorToInt(this.maxHeightReached) * 10;
            }
            this.isGamePaused.Value = true;
            this.lossCounter++;
            return;
        }

        this.timer += Time.deltaTime;

        if ((this.isMainMenuDemo || this.isFirstRun) && this.spawnCounter <= 50 && this.timer >= 0.05f) {
            Vector2 pos = Vector2.up * (this.spawnCounter + 1) * 0.5f;
            pos.x = Random.Range(-5f, 6f);
            Instantiate(this.squarePrefab, pos, Quaternion.identity);
            this.spawnCounter++;
            this.timer = 0;
        }

        if (this.timer > 10) {
            this.spawnCounter = 1;
        }

        if (this.isFirstRun) {
            return;
        }
        // TODO: just do this in camera movement lol
        var hit = Physics2D.BoxCast(new Vector2(0, maxCameraHeight), new Vector2(this.plankLength, 1), 0, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Spawnable Objects"));
        if (hit.collider != null) {
            float height = hit.transform.position.y;
            if (height > this.maxHeightReached) {
                //Debug.Log("new height! " + height);
                this.maxHeightReached.Value = height;
            }
        }
    }

    public void DestroyShape(GameObject gameObject)
    {
        Destroy(gameObject);
        if (gameObject.GetComponent<Shape>() != null) {
            this.destroyedShapeCount++;
        }
    }

    public void StartRun()
    {
        this.reset = true;
        if (this.isFirstRun && lossCounter > 0) {
            this.isFirstRun.Value = false;
        }
    }
}
