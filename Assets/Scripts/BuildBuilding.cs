using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildBuilding : MonoBehaviour, IPointerClickHandler
{

    public UnityEvent OnLeft;
    public UnityEvent OnRight;
    public UnityEvent OnMiddle;

    public Image progress;
    public Building building;
    public GameManager gameManager;
    [SerializeField]
    private float counter;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeft.Invoke();

        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRight.Invoke();
        }
        else if(eventData.button == PointerEventData.InputButton.Middle)
        {

            OnMiddle.Invoke();
        }


    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(building.buildingInProgress == true)
        {
            if(gameManager.money > 0)
            {
                if(counter < building.buildingDuration)
                {
                    counter += Time.deltaTime;
                }
                else
                {
                    counter = 0;
                }
                progress.fillAmount = counter / building.buildingDuration;
                gameManager.money -= ChangeMoney(building.cost);

                if(counter >= building.buildingDuration)
                {
                    building.buildingInProgress = false;
                    building.done = true;
                }
            }


        }
    }

    public void StartBuilding()
    {
        Debug.Log("Building has started");
        if(building.paused == false)
        {
            if(building.done == true)
            {
                GameObject builtBuilding = Instantiate(building.model, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                Camera.main.GetComponent<CameraControl>().draggedBuilding = builtBuilding;
            }
            else if(building.buildingInProgress == false)
            {
                counter = 0;
                building.buildingInProgress = true;
            }
            else if(building.buildingInProgress == true)
            {
                PauseBuilding();
            }
        }
        else
        {
            ResumeBuilding();
        }

    }

    public void CancelBuilding()
    {
        gameManager.money += Mathf.RoundToInt(building.cost * (counter / building.buildingDuration));
        Debug.Log("Building is cancelled");
        building.buildingInProgress = false;
        building.done = false;
        building.paused = false;
        counter = 0;
        progress.fillAmount = 0;
    }


    private void PauseBuilding()
    {
        building.buildingInProgress = false;
        building.paused = true;

    }

    private void ResumeBuilding()
    {
        building.buildingInProgress = true;
        building.paused = false;

    }

    public int ChangeMoney(int amount)
    {
        int result = Mathf.RoundToInt(amount * (counter/building.buildingDuration)) - Mathf.RoundToInt(amount * ((counter - Time.deltaTime)/building.buildingDuration));
        return result;
    }
}
