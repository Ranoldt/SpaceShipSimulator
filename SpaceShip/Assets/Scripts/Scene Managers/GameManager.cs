using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
/// <summary>
/// Gets the number of players for the game and spawns prefabs and managers accordingly
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pManager;
    [SerializeField]
    private GameObject iManager;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject cameraPrefab;

    //implements a singleton pattern with a dont destroy on load so we can
    //determine the amount of players in the main screen and then initialize the game scene accordingly
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SpawnPlayers;
    }

    private void SpawnPlayers(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GampeplayScene")
        {
            for (int i = 0; i < numberofPlayers; i++)
            {
                var spawnedPManager = Instantiate(pManager);
                var spawnedIManager = Instantiate(iManager);
                var spawnedPlayer = Instantiate(playerPrefab);
                var spawnedCamera = Instantiate(cameraPrefab);


                //initialize UI
                var spawnedUI = spawnedCamera.GetComponentInChildren<UIManager>();
                spawnedUI.inv = spawnedIManager.GetComponent<InventoryManager>();
                spawnedUI.player = spawnedPManager.GetComponent<PlayerManager>();


                //initialize Camera
                var cam = spawnedCamera.GetComponent<Camera>();
                var vcam = cam.GetComponentInChildren<CinemachineVirtualCamera>();

                vcam.LookAt = spawnedPlayer.gameObject.transform;
                vcam.Follow = spawnedPlayer.gameObject.transform;


                //initialize split screen
                if(numberofPlayers == 2)
                {
                    if(i == 0)
                    {
                        cam.rect = new Rect(0, 0.5f, 1, 0.5f);
                    }
                    if(i == 1)
                    {
                        cam.rect = new Rect(0, 0, 1, 0.5f);
                    }
                }

                //initialize manager
                spawnedIManager.GetComponent<InventoryManager>().equippedMineTool = playerShips[i].tool;
                spawnedIManager.GetComponent<InventoryManager>().equippedBoost = playerShips[i].boost;

                //initialize SpaceShip script
                spawnedPlayer.GetComponent<SpaceShip>().inv = spawnedIManager.GetComponent<InventoryManager>();
                spawnedPlayer.GetComponent<SpaceShip>().playerData = spawnedPManager.GetComponent<PlayerManager>();

                //initialize player model
                spawnedPlayer.GetComponentInChildren<MeshFilter>().mesh = playerShips[i].model;
            }
        }

    }

    public int numberofPlayers = 1;
    public int initializedPlayers;


    public shipConfiguration[] playerShips { get { return _playerShips; } private set { _playerShips = value; } }
    [SerializeField]
    private shipConfiguration[] _playerShips;//the scriptable object that defines each player's ship

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SpawnPlayers;
    }
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
