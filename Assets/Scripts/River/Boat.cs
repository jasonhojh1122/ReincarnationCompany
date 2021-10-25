using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Boat : MonoBehaviour {

    [SerializeField] int life;
    [SerializeField] int money;
    [SerializeField] List<GrabPosition> grabPositions;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI moneyText;


    private void Start() {
        lifeText.text = life.ToString();
        moneyText.text = money.ToString();
        foreach (GrabPosition gp in grabPositions) {
            gp.SetBoat(this);
        }
    }

    public void AddToLife(int amount) {
        life += amount;
        lifeText.text = life.ToString();
    }

    public void AddToMoney(int amount) {
        money += amount;
        moneyText.text = money.ToString();
    }

}