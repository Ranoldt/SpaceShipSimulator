using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
/// <summary>
/// OBSOLOETE WE USE SCRIPTABLE OBJECTS IN THIS HOUSE
/// PTEW PTEW I SPIT ON SINGLETONS
/// </summary>
public class UIManager : MonoBehaviour
{
    //singleton manager for ui
    private static UIManager _instance;

    public static UIManager UIinstance { get { return _instance; } }

    private void Awake()
    {
        if(UIinstance != null && UIinstance != this){
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //UI components
    public TMP_Text moneyText;

    [SerializeField] private Slider LaserHeat;
    private LaserShooting lasershooting;

    [SerializeField]
    private GameObject inventory_prefab;

    private GameObject inv;

    public GameObject _inv { get; private set; }


    // Start is called before the first frame update
    private void Start()
    {
        lasershooting = FindObjectOfType<LaserShooting>();
        inv = Instantiate(inventory_prefab,this.transform); //create inv ui on startup and store it as a variable 
        //makes this component accessible bc this is a singleton, and reduces coupling 
        _inv = inv;
        
     }

    // Update is called once per frame
    private void Update()
    {
        //if(lasershooting != null)
        //{
        //    LaserHeat.value = lasershooting.Currentlaserheat / lasershooting.LaserHeatThreshold;
        //}
    }

    public void InvToggle(InputAction.CallbackContext context)
    {
        if (inv.activeSelf)
        {
            inv.SetActive(false);
        }
        else
        {
            inv.SetActive(true);
        }
    }
}
