using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotherShip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
