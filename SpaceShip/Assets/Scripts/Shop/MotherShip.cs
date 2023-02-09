using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotherShip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private GameObject sellMenu;

    bool isMenu  = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && text.isActiveAndEnabled)
        {
            sellMenu.SetActive(true);
            isMenu = true;
        }

        if (isMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            isMenu = false;
            sellMenu.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
        }
    }

}
