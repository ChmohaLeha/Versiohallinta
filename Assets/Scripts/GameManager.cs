using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int money;
    [SerializeField]
    TextMeshProUGUI moneyField;
    [SerializeField]
    Canvas menu;
    public Building[] buildings;

    void Start()
    {
        menu = GameObject.Find("Canvas").GetComponent<Canvas>();
        moneyField = menu.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        foreach (Building singleBuilding in buildings)
        {
            singleBuilding.done = false;
            singleBuilding.paused = false;
            singleBuilding.buildingInProgress = false;
            Button buildingButton = Instantiate(singleBuilding.menuIcon);
            buildingButton.transform.SetParent(menu.transform.GetChild(0));

        }

    }

    void Update()
    {
        moneyField.text = money.ToString();
    }
}
