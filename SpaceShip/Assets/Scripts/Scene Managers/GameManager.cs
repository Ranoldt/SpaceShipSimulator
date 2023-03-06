using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;
/// <summary>
/// Gets the number of players for the game and spawns prefabs and managers accordingly
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<LayerMask> playerCamLayers;

    public List<PlayerInput> players;

    [SerializeField]
    private GameObject playerPrefab;

    //implements a singleton pattern with a dont destroy on load so we can
    //determine the amount of players in the main screen and then initialize the game scene accordingly
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void AddPlayer(PlayerInput player)
    {
        var playerParent = player.transform.parent;
        players.Add(player);
        player.transform.parent.GetComponentInChildren<PlayerManager>().playerID = players.Count-1;



        //next is setting up the cinemachine camera

        //because the layer mask is a bit, we need to convert it to an int
        int layer = (int)Mathf.Log(playerCamLayers[players.Count - 1].value, 2);

        //set the camera to the layer
        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layer;
        //add the layer to the culling mask
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layer;

    }

    public int numberofPlayers = 1;
    public int initializedPlayers;


    public shipConfiguration[] playerShips { get { return _playerShips; } private set { _playerShips = value; } }
    [SerializeField]
    private shipConfiguration[] _playerShips;//the scriptable object that defines each player's ship
}

[System.Serializable]
public class shipConfiguration
{
    public MineObjects tool;
    public BoostComponent boost;
    public Mesh model;

    public shipConfiguration(MineObjects _tool, BoostComponent _boost, Mesh _model)
    {
        boost = _boost;
        tool = _tool;
        model = _model;
    }

    public shipConfiguration()
    {

    }
}
