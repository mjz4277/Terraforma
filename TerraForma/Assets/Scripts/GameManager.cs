using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private enum SelectionMode
    {
        Default,
        UnitMove,
        UnitAttack,
        PowerUse
    }
    private SelectionMode selectionMode;


    public static GameManager instance = null;
    LevelManager m_level;
    HUDManager m_hud;
    GameObject selected;
    GameObject powerSelection;
    Unit selected_unit;
    Power selected_power;
    List<Tile> tilesInPower = new List<Tile>();
    List<Unit> unitsInPower = new List<Unit>();

    List<Tile> possibleTiles = new List<Tile>();
    List<Unit> possibleUnits = new List<Unit>();

    public GameObject playerPrefab;

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
        m_hud = GetComponent<HUDManager>();

        InitGame();
    }

    void InitGame()
    {
        m_hud.Init();
        m_level.BuildMap();
        InitPlayers();
        m_level.SetUpUnits(player_1);
        m_level.SetUpUnits(player_2);
        current_player = player_1;
        selectionMode = SelectionMode.Default;
        m_hud.HideUnitInfo();
        m_hud.HideUnitActions();
    }

    void InitPlayers()
    {
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
        if (selectionMode == SelectionMode.PowerUse)
        {
            powerSelection = GetSelectedObject();
            if (powerSelection && powerSelection.GetComponent<Tile>())
            {
                Tile powerTile = powerSelection.GetComponent<Tile>();
                if (!possibleTiles.Contains(powerTile))
                {
                    powerTile = m_level.GetNearestPossibleTile(powerTile, possibleTiles);
                }
                foreach (Tile t in possibleTiles)
                {
                    t.ClearSelection();
                }
                ProcessPowerType(powerTile);
            }
        }
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = GetSelectedObject();
            if (selected != null)
            {
                ProcessSelection();
            }
        }

        //Get the different possible unit actions
        if (selected_unit && selected_unit.Team == turn)
        {
            //A unit can't do anything if its turn is over
            if (!selected_unit.TurnOver)
            {
                //Switch between move and attack mode
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (selectionMode == SelectionMode.UnitAttack)
                    {
                        DisplayMove();
                    }
                    else
                    {
                        DisplayAttack();
                    }
                }

                //Choosing a power to use
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    selectionMode = SelectionMode.PowerUse;
                    selected_power = selected_unit.Powers[0];
                    DisplayPower();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    selectionMode = SelectionMode.PowerUse;
                    selected_power = selected_unit.Powers[1];
                    DisplayPower();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    selectionMode = SelectionMode.PowerUse;
                    selected_power = selected_unit.Powers[2];
                    DisplayPower();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    selectionMode = SelectionMode.PowerUse;
                    selected_power = selected_unit.Powers[3];
                    DisplayPower();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeTurn();
        }
    }

    public void DisplayAttack()
    {
        if (selected_unit.CanAttack)
        {
            selectionMode = SelectionMode.UnitAttack;
            m_level.ClearSelectedTiles();
            possibleTiles.Clear();
            possibleUnits.Clear();
            possibleTiles = m_level.GetPossibleTiles(selected_unit.CurrentTile, selected_unit.Range, true);
            foreach (Tile t in possibleTiles)
            {
                t.Adjacent = true;
                if (t.Occupied)
                {
                    Unit enemy = t.OccupiedBy;
                    if (enemy.Team != selected_unit.Team)
                    {
                        possibleUnits.Add(enemy);
                        t.Selected = true;
                    }
                }
            }
        }
    }

    public void DisplayMove()
    {
        selectionMode = SelectionMode.UnitMove;
        m_level.ClearSelectedTiles();
        possibleTiles.Clear();
        if (selected_unit.PossibleMove > 0)
            possibleTiles = m_level.GetPossibleTiles(selected_unit.CurrentTile, selected_unit.PossibleMove, false);

        foreach (Tile t in possibleTiles)
        {
            t.Adjacent = true;
        }
    }

    public void DisplayPower()
    {
        if (selected_unit.CanUsePower)
        {
            selectionMode = SelectionMode.PowerUse;
            m_level.ClearSelectedTiles();
            possibleTiles.Clear();
            possibleUnits.Clear();
            possibleTiles = m_level.GetPossibleTiles(selected_unit.CurrentTile, selected_power.Range, true);
        }
        else
        {
            Debug.Log("Silenced");
        }
    }

    public void ProcessPowerType(Tile t)
    {
        t.Selected = true;
        List<Tile> affectedTiles = new List<Tile>();
        List<Unit> affectedUnits = new List<Unit>();
        switch(selected_power.Type)
        {
            case PowerType.Area:
                affectedTiles = m_level.GetPossibleTiles(t, selected_power.AOE, true);
                break;
            case PowerType.Biome:
                break;
            case PowerType.Burst:
                affectedTiles = m_level.GetPossibleTiles(selected_unit.CurrentTile, selected_power.AOE, true);
                break;
            case PowerType.Cone:
                break;
            case PowerType.Line:
                break;
        }

        switch(selected_power.Target)
        {
            case PowerTarget.Tile:
                break;
            case PowerTarget.Unit:
                foreach(Tile tile in affectedTiles)
                {
                    if (tile.Occupied)
                        affectedUnits.Add(tile.OccupiedBy);
                }
                break;
            case PowerTarget.Both:
                foreach (Tile tile in affectedTiles)
                {
                    if (tile.Occupied)
                        affectedUnits.Add(tile.OccupiedBy);
                }
                break;
        }

        foreach(Tile aff in affectedTiles)
        {
            aff.Adjacent = true;
        }

        tilesInPower = affectedTiles;
        unitsInPower = affectedUnits;
    }

    public void ChangeTurn()
    {
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
        selected_unit = null;
        m_hud.HideUnitInfo();
        selectionMode = SelectionMode.Default;
        possibleUnits.Clear();
        possibleTiles.Clear();
        m_level.ClearSelectedTiles();
        m_level.ClearSelectedUnit();
    }

    void ProcessSelection()
    {
        Debug.Log("Process Selection");
        m_level.ClearSelectedUnit();
        m_level.ClearSelectedTiles();

        //If a unit is selected
        if (selected.GetComponent<Unit>())
        {
            Unit chosen = selected.GetComponent<Unit>();
            ProcessUnitSelection(chosen);
        }
        //If a tile is selected
        else if (selected.GetComponent<Tile>())
        {
            Tile sel_tile = selected.GetComponent<Tile>();
            ProcessTileSelection(sel_tile);
        }
    }

    private void ProcessUnitSelection(Unit chosen)
    {
        //If the selected piece is in attack mode
        if (selected_unit && selectionMode == SelectionMode.UnitAttack)
        {
            if (possibleUnits.Contains(chosen))
            {
                selected_unit.AttackUnit(chosen);
                selectionMode = SelectionMode.Default;
            }
        }
        else
        {
            selectionMode = SelectionMode.UnitMove;
            selected_unit = chosen;
            selected_unit.Selected = true;
            m_hud.ShowUnitInfo();
            if (selected_unit.Team != turn)
                m_hud.HideUnitActions();
            m_hud.SetUnitInfo(selected_unit);
            if (selected_unit.Team == current_player.Team)
                DisplayMove();
        }
    }

    private void ProcessTileSelection(Tile chosen)
    {
        m_level.SelectTile(selected);
        if (selected_unit)
        {
            if (selectionMode == SelectionMode.UnitMove)
            {
                if (selected_unit.CurrentTile && selected_unit.Team == turn)
                {
                    bool validMove = false;

                    //Check if tile selected is within the range of possible moves
                    foreach (Tile t in possibleTiles)
                    {
                        //Don't count clicking the current tile as a move action
                        if (chosen == selected_unit.CurrentTile) break;

                        if (chosen == t)
                        {
                            validMove = true;
                            break;
                        }
                    }
                    if (validMove)
                    {
                        selected_unit.PossibleMove -= (int)chosen.Distance;
                        m_level.MoveUnitToTile(selected_unit, chosen);
                        DisplayMove();
                    }
                    else
                    {
                        selected_unit = null;
                        m_hud.HideUnitInfo();
                        selectionMode = SelectionMode.Default;
                    }
                }
                else
                {
                    selected_unit = null;
                    m_hud.HideUnitInfo();
                    selectionMode = SelectionMode.Default;
                }
            }
            else if(selectionMode == SelectionMode.UnitAttack)
            {
                //Get if there is an attackable unit on the tile
                if(chosen.Occupied)
                {
                    Unit u = chosen.OccupiedBy;
                    if(u.Team != turn)
                    {
                        selected_unit.AttackUnit(u);
                        selectionMode = SelectionMode.Default;
                    }
                    else
                    {
                        selected_unit = null;
                        m_hud.HideUnitInfo();
                        selectionMode = SelectionMode.Default;
                    }
                }
            }
            else if(selectionMode == SelectionMode.PowerUse)
            {
                selected_power.UsePower(tilesInPower, unitsInPower);
                selectionMode = SelectionMode.Default;

                //Unit's turn ends after using a power
                selected_unit.PossibleMove = 0;
                selected_unit.CanAttack = false;
                selected_unit.TurnOver = true;
            }
            else
            {
                selected_unit = null;
                m_hud.HideUnitInfo();
                selectionMode = SelectionMode.Default;
            }
        }
    }

    GameObject GetSelectedObject()
    {
        //UI blocks raycast
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return null;

        RaycastHit hit = new RaycastHit();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            GameObject chosen = hit.transform.gameObject;

            if (chosen.transform.parent)
                chosen = chosen.transform.parent.gameObject;

            return chosen;

        }

        return null;
    }
}
