using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    ResourceManager m_resources;
    LevelManager m_level;
    TileManager m_tiles;
    UnitManager m_units;
    HUDManager m_hud;

    Player player_1;
    Player player_2;
    Player current_player;

    int turn = 1;

    // Use this for initialization
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Debug.Log("GameManager Started");
        m_level = GetComponent<LevelManager>();
        m_tiles = GetComponent<TileManager>();
        m_units = GetComponent<UnitManager>();
        m_hud = GetComponent<HUDManager>();
        m_resources = GetComponent<ResourceManager>();

        InitGame();
    }

    void InitGame()
    {
        m_hud.Init();
        InitPlayers();
        //m_level.SetUpUnits(player_1);
        //m_level.SetUpUnits(player_2);
        current_player = player_1;
        m_hud.HideUnitInfo();
        m_hud.HideUnitActions();
    }

    void InitPlayers()
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        GameObject player1 = Instantiate(playerPrefab) as GameObject;
        player1.name = "Player_1";
        player_1 = player1.GetComponent<Player>();
        player_1.Init();
        player_1.Name = "Player 1";
        player_1.Team = 1;
        player_1.Element0 = Element.Fire;
        player_1.Element1 = Element.Earth;
        player_1.Element2 = Element.Water;
        player_1.Element3 = Element.Nature;

        GameObject player2 = Instantiate(playerPrefab) as GameObject;
        player2.name = "Player_2";
        player_2 = player2.GetComponent<Player>();
        player_2.Init();
        player_2.Name = "Player 2";
        player_2.Team = 2;
        player_2.Element0 = Element.Dark;
        player_2.Element1 = Element.Light;
        player_2.Element2 = Element.Electric;
        player_2.Element3 = Element.Ice;

        m_hud.SetPlayerInfo(player_1);
        player_1.StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeTurn();
        }
    }

    public void ChangeTurn()
    {
        current_player.EndTurn();
        if(turn == 1)
        {
            turn = 2;
            current_player = player_2;
        }
        else
        {
            turn = 1;
            current_player = player_1;
        }

        m_level.ChangeTurn(turn);

        m_hud.SetPlayerInfo(current_player);
        current_player.StartTurn();

        m_hud.HideUnitInfo();
        m_tiles.ClearSelectedTiles();
        m_units.ClearSelectedUnits();
    }
}
