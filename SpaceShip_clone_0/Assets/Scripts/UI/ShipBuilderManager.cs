using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShipBuilderManager : MonoBehaviour
{
    [SerializeField]
    private SelectedPart minePart;

    [SerializeField]
    private SelectedPart boostPart;

    [SerializeField]
    private TMP_Text playerNumberText;

    [SerializeField]
    private shipConfiguration editedShip;

    public static ShipBuilderManager instance;
    private void Awake()
    {
        editedShip = GameManager.instance.playerShips[GameManager.instance.initializedPlayers];
        playerNumberText.text = "Player " + GameManager.instance.initializedPlayers + 1.ToString();
    }

    //keep track of the initialized ships vs. the uninitialized ships
    //if the initialized ship count = player number,

    public void ShipBuildConfirmation()
    {
        editedShip.tool = minePart.shipComponent as MineObjects;
        editedShip.boost = boostPart.shipComponent as BoostComponent;

        GameManager.instance.initializedPlayers += 1;
        if(GameManager.instance.initializedPlayers != GameManager.instance.numberofPlayers)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
