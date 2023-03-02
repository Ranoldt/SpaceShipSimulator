using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Gets the number of players for the game and spawns prefabs and managers accordingly
/// </summary>
public class GameManager : MonoBehaviour
{

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
            // player spawning logic here
        }

    }

    public int numberofPlayers;
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

    public shipConfiguration(MineObjects _tool, BoostComponent _boost)
    {
        boost = _boost;
        tool = _tool;
    }

    public shipConfiguration()
    {

    }
}
