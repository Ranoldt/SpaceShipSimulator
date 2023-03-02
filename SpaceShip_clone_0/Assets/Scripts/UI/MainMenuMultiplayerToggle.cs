using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuMultiplayerToggle : MonoBehaviour
{
    /// <summary>
    /// used for main menu buttons to switch between single and two player mode
    /// </summary>

    [SerializeField]
    private TMP_Text buttonText;

    public void toggleSinglePlayer()
    {
        buttonText.text = "Start Singleplayer";
        GameManager.instance.numberofPlayers = 1;
    }

    //no plans for more than 2 players so this is ok
    public void toggleMultiPlayer()
    {
        buttonText.text = "Start Multiplayer";
        GameManager.instance.numberofPlayers = 2;
    }



}
