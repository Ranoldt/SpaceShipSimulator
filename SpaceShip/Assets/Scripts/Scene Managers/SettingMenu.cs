using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SettingMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingMenu;

    public void openMenu()
    {
        settingMenu.SetActive(true);
    }

    public void closeMenu()
    {
        settingMenu.SetActive(false);
    }

}
