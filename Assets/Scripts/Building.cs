using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Building", menuName = "Buildings/Building")]

public class Building : ScriptableObject
{
    public string buildingName;
    public float health;
    public Button menuIcon;
    public Button[] production;
    public GameObject model;
    public int cost;
    public float buildingDuration;

    public bool buildingInProgress;
    public bool paused;
    public bool done;

}
