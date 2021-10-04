using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public IntegerVariable playerMoney;
    public Inventory inventory;
    public PassiveInventory passiveInventory;
    public Image icon;
    public TMPro.TMP_Text nameText;
    public TMPro.TMP_Text priceText;
    public AbilityRuntimeSet abilityShopItem;
    public PassiveAbilityRuntimeSet passiveShopItem;
    public AudioClip successClip;
    public AbilitiesUI abilitiesUI;
    public ShopUI shopUI;

    private Ability abilityOwned;
    private Ability abilityShown;
    private PassiveAbility passiveAbilityOwned;
    private PassiveAbility passiveAbilityShown;

    private bool IsPassive => this.passiveShopItem != null;

    void Start()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (this.abilityShopItem != null) {
            this.abilityOwned = null;
            this.abilityShown = null;
            foreach (var item in this.abilityShopItem.Items) {
                if (this.abilityOwned != null) {
                    this.abilityShown = item;
                    break;
                }
                if (this.inventory.abilities.Any((ability) => ability == item)) {
                    this.abilityOwned = item;
                }
            }
            if (this.abilityShown == null && this.abilityOwned == null) {
                this.abilityShown = this.abilityShopItem.Items[0];
            }
            if (this.abilityShown == null) {
                // Show sold out
                this.priceText.text = "Sold Out!";
                return;
            }
            this.icon.sprite = this.abilityShown.sprite;
            this.nameText.text = this.abilityShown.name;
            if (this.abilityShown.cost == 0) {
                this.priceText.text = "Free!";
            } else {
                this.priceText.text = string.Format("${0}", this.abilityShown.cost);
            }
        } else if (this.passiveShopItem != null) {
            this.passiveAbilityOwned = null;
            this.passiveAbilityShown = null;
            foreach (var item in this.passiveShopItem.Items) {
                if (this.passiveAbilityOwned != null) {
                    this.passiveAbilityShown = item;
                    break;
                }
                if (this.passiveInventory.passives.Any((ability) => ability == item)) {
                    this.passiveAbilityOwned = item;
                }
            }
            if (this.passiveAbilityShown == null && this.passiveAbilityOwned == null) {
                this.passiveAbilityShown = this.passiveShopItem.Items[0];
            }
            if (this.passiveAbilityShown == null) {
                // Show sold out
                this.priceText.text = "Sold Out!";
                return;
            }
            this.icon.sprite = this.passiveAbilityShown.sprite;
            this.nameText.text = this.passiveAbilityShown.name;
            if (this.passiveAbilityShown.cost == 0) {
                this.priceText.text = "Free!";
            } else {
                this.priceText.text = string.Format("${0}", this.passiveAbilityShown.cost);
            }
        }
    }

    public void PurchaseUpgrade()
    {
        if (IsPassive) {
            if (this.passiveAbilityShown == null) {
                return;
            }
            // Find index in inventory of current upgrade
            int idx = this.passiveInventory.passives.FindIndex((passive) => passive == this.passiveAbilityOwned);
            if (this.playerMoney.Value >= this.passiveAbilityShown.cost) {
                this.playerMoney.Value -= this.passiveAbilityShown.cost;
                if (idx == -1) {
                    this.passiveInventory.passives.Add(this.passiveAbilityShown);
                } else {
                    this.passiveInventory.passives[idx] = this.passiveAbilityShown;
                }
                GetComponent<AudioSource>().PlayOneShot(this.successClip);
                this.abilitiesUI.UpdateUI();
                this.shopUI.EnablePlay(true);
            }
        } else {
            if (this.abilityShown == null) {
                return;
            }
            // Find index in inventory of current upgrade
            int idx = this.inventory.abilities.FindIndex((ability) => ability == this.abilityOwned);
            if (this.playerMoney.Value >= this.abilityShown.cost) {
                this.playerMoney.Value -= this.abilityShown.cost;
                if (idx == -1) {
                    this.inventory.abilities.Add(this.abilityShown);
                } else {
                    this.inventory.abilities[idx] = this.abilityShown;
                }
                GetComponent<AudioSource>().PlayOneShot(this.successClip);
                this.abilitiesUI.UpdateUI();
                this.shopUI.EnablePlay(true);
            }
        }
        UpdateSlot();
    }
}
