using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject container;
    public GameObject abilityPrefab;
    public GameObject abilitySelector;
    public IntegerVariable selectedAbility;
    public BoolReference isGamePaused;
    public BoolReference isFirstRun;
    public Vector3Variable cursorPosition;

    private bool HasAbility => this.inventory.abilities.Count > 0;

    private List<GameObject> abilityObjects = new List<GameObject>();
    private GameObject placeholderObject;
    private SpriteRenderer placeholderSpriteRenderer;
    private float shapeSize = 1;
    private float cursorSpeed = 10;
    private bool movedMouse = false;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        this.placeholderObject = new GameObject("Ability Placeholder");
        this.placeholderSpriteRenderer = this.placeholderObject.AddComponent<SpriteRenderer>();
        Color transparent = Color.white;
        transparent.a = 0.5f;
        this.placeholderSpriteRenderer.color = transparent;
    }

    // Update is called once per frame
    void Update()
    {
        float altH = Input.GetAxis("Horizontal Alt");
        float altV = Input.GetAxis("Vertical Alt");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseX != 0 || mouseY != 0) {
            this.movedMouse = true;
        }
        if (altH != 0 || altV != 0) {
            this.movedMouse = false;
            this.cursorPosition.Value = new Vector3(this.cursorPosition.Value.x + altH * Time.deltaTime * this.cursorSpeed, this.cursorPosition.Value.y - altV * Time.deltaTime * this.cursorSpeed, -1);
        }
        if (this.movedMouse) {
            this.cursorPosition.Value = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 9);
        }
        if (this.isGamePaused || !HasAbility) {
            this.abilitySelector.SetActive(false);
            return;
        }
        this.abilitySelector.SetActive(true);
        Ability curAbility = this.inventory.abilities[this.selectedAbility.Value];
        bool canActivate = curAbility.CanActivate(this.placeholderObject);
        this.placeholderSpriteRenderer.color = canActivate ? Color.white : Color.red;
        if (Input.GetButtonDown("Fire1") && canActivate) {
            curAbility.Activate(this.placeholderObject);
        } else {

            if (Input.GetButtonDown("Next Ability")) {
                this.selectedAbility++;
            } else if (Input.GetButtonDown("Previous Ability")) {
                this.selectedAbility--;
            }
            if (this.selectedAbility.Value < 0) {
                this.selectedAbility.Value = 0;
            } else if (this.selectedAbility.Value >= this.inventory.abilities.Count) {
                this.selectedAbility.Value = this.inventory.abilities.Count - 1;
            }

            var firstChild = this.container.transform.GetChild(this.selectedAbility.Value);
            this.abilitySelector.transform.position = firstChild.transform.position;
        }

        this.placeholderObject.transform.position = this.cursorPosition.Value;
        this.placeholderSpriteRenderer.sprite = curAbility.sprite;
        Vector3 newScale = this.placeholderObject.transform.localScale;
        if (curAbility.GetType() == typeof(PlaceShapeAbility)) {
            this.shapeSize = (curAbility as PlaceShapeAbility).shapeSize;
        } else {
            this.shapeSize = 1;
        }
        newScale.x = newScale.y = this.shapeSize;
        this.placeholderObject.transform.localScale = newScale;

        // Update cooldown info
        for (int i = 0; i < this.inventory.abilities.Count; i++) {
            Ability ability = this.inventory.abilities[i];
            ability.Update();
            var go = this.abilityObjects[i];
            var imgs = go.GetComponentsInChildren<Image>();
            imgs[1].fillAmount = ability.RemainingCooldownTime / ability.cooldownTime;
            int timeLeft = Mathf.CeilToInt(ability.RemainingCooldownTime);
            var textGO = go.GetComponentInChildren<TMPro.TMP_Text>();
            if (timeLeft > 0) {
                textGO.text = "" + timeLeft;
            } else {
                textGO.text = "";
            }
        }
    }

    public void UpdateUI()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 size = rt.sizeDelta;
        size.x = 300 * this.inventory.abilities.Count;
        rt.sizeDelta = size;
        // Clear existing abilities
        foreach (Transform child in this.container.transform) {
            Destroy(child.gameObject);
        }
        this.abilityObjects.Clear();
        foreach (Ability ability in this.inventory.abilities) {
            var go = Instantiate(this.abilityPrefab, Vector3.zero, Quaternion.identity, this.container.transform);
            this.abilityObjects.Add(go);
            var imgs = go.GetComponentsInChildren<Image>();
            imgs[0].sprite = ability.sprite;
            imgs[1].fillAmount = 0;
        }
    }

    public void ResetCooldowns()
    {
        foreach (Ability ability in this.inventory.abilities) {
            ability.Reset();
        }
    }
}
