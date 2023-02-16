using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

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
        inv = Instantiate(inventory_prefab,this.transform); //create inv ui on startup 
        _inv = inv;
        
     }

    // Update is called once per frame
    private void Update()
    {
        if(lasershooting != null)
        {
            LaserHeat.value = lasershooting.Currentlaserheat / lasershooting.LaserHeatThreshold;
        }
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
