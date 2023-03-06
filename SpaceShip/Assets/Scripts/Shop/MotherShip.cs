using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class MotherShip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private GameObject sellMenu;
    [SerializeField]
    private GameObject UI;//player ui
    [SerializeField]
    private InventoryMenu inventoryMenu;

    bool isMenu  = false;

    // Update is called once per frame
    public void openShop(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && text.isActiveAndEnabled)
        {
            text.gameObject.SetActive(false);
            sellMenu.SetActive(true);
            UI.SetActive(false);
            isMenu = true;
            var inputs = gameObject.transform.root.GetComponentInChildren<PlayerInput>();
            inputs.DeactivateInput();
            inputs.actions.FindAction("LeaveMenus").Enable();
        }
    }

    public void closeShop(InputAction.CallbackContext ctx)
    {
        if (isMenu && ctx.performed)
        {
            text.gameObject.SetActive(true);
            isMenu = false;
            sellMenu.SetActive(false);
            UI.SetActive(true);
            gameObject.transform.root.GetComponentInChildren<PlayerInput>().ActivateInput();
            inventoryMenu.UntoggleSelectibles();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            text.gameObject.SetActive(false);
        }
    }

}
