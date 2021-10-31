using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Boat : MonoBehaviour {

    [SerializeField] int life;
    [SerializeField] int money;
    [SerializeField] List<GrabPosition> grabPositions;
    [SerializeField] BoatLife boatLife;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Transform moneyGainPrefab;
    [SerializeField] Transform itemPickupPrefab;


    private void Start() {
        moneyText.text = money.ToString();
        foreach (GrabPosition gp in grabPositions) {
            gp.SetBoat(this);
        }
    }

    public void AddToLife(int amount) {
        boatLife.AddToLife(amount);
    }

    public void AddToMoney(int amount) {
        money += amount;
        moneyText.text = money.ToString();
    }

    public void AddToBackpack(string name) {
        UserStateManager.Instance.Backpack.AddItemToBackpack(name, 1);
    }

    public void ShowMoneyGain() {

    }

    public void ShowItemPickup() {

    }

}