using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public enum PlayerState
    {
        Default,
        UnitMove,
        UnitAttack,
        UnitPower,
        Biome
    }
    private PlayerState e_playerState;

    private delegate void del_Action(Tile t);
    private del_Action ResolveAction;

    private Player player;
    private TileManager m_tiles;
    private UnitManager m_units;
    private HUDManager m_hud;
    private PowerManager m_power;

    private Unit selectedUnit;
    private Power selectedPower;

    private List<Tile> tilesInPower;
    private List<Unit> unitsInPower;

    public PlayerState E_PlayerState
    {
        get { return e_playerState; }
    }

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        m_power = GetComponent<PowerManager>();
        m_tiles = GameObject.FindGameObjectWithTag("GameController").GetComponent<TileManager>();
        m_units = GameObject.FindGameObjectWithTag("GameController").GetComponent<UnitManager>();
        m_hud = GameObject.FindGameObjectWithTag("GameController").GetComponent<HUDManager>();

        tilesInPower = new List<Tile>();
        unitsInPower = new List<Unit>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    //Decide what is done if a unit was selected
    public void ResolveUnitSelection(Unit chosen)
    {
        m_tiles.ClearSelectedTiles();
        m_units.ClearSelectedUnits();
        //If the selected piece is in attack mode
        if (selectedUnit && e_playerState == PlayerState.UnitAttack)
        {
            if (selectedUnit.GetComponent<UnitAttack>().Attack(chosen))
            {
                selectedUnit.EndTurn();
                e_playerState = PlayerState.Default;
            }
            else
            {
                CancelAction();
            }
        }
        else
        {
            e_playerState = PlayerState.UnitMove;
            selectedUnit = chosen;
            selectedUnit.Selected = true;
            m_hud.ShowUnitInfo();
            if (selectedUnit.Team != player.Team)
                m_hud.HideUnitActions();
            m_hud.SetUnitInfo(selectedUnit);
            if (selectedUnit.Team == player.Team)
            {
                if (selectedUnit.GetComponent<UnitMove>().CanMove())
                    DisplayMove();
                else
                    DisplayAttack();
            }
                
        }
    }

    //Decide what is done if a tile is selected
    public void ResolveTileSelection(Tile chosen)
    {
        m_tiles.ClearSelectedTiles();

        m_tiles.SelectTile(chosen);

        if (selectedUnit && selectedUnit.Team == player.Team)
        {
            if (e_playerState == PlayerState.UnitMove)
            {
                this.ResolveAction = ResolveMove;
            }
            else if (e_playerState == PlayerState.UnitAttack)
            {
                this.ResolveAction = ResolveAttack;
            }
            else if (e_playerState == PlayerState.UnitPower)
            {
                this.ResolveAction = ResolvePower;
            }

            ResolveAction(chosen);
        }
    }

    //Switch between Move and Attack Mode
    public void CycleAction()
    {
        //Get the different possible unit actions
        if (selectedUnit && selectedUnit.Team == player.Team)
        {
            //A unit can't do anything if its turn is over
            if (!selectedUnit.TurnOver)
            {
                if (e_playerState == PlayerState.UnitAttack)
                {
                    //Don't display movement if unit cannot move
                    if(selectedUnit.GetComponent<UnitMove>().CanMove())
                        DisplayMove();
                }
                else
                {
                    DisplayAttack();
                }
            }
        }
    }

    //Display the current possible tiles that the chosen unit can move to
    public void DisplayMove()
    {
        e_playerState = PlayerState.UnitMove;
        selectedUnit.GetComponent<UnitMove>().DisplayMove();
    }

    //Display the current possible tiles and units the chosen unit can attack
    public void DisplayAttack()
    {
        if (selectedUnit.CanAttack)
        {
            e_playerState = PlayerState.UnitAttack;
            m_tiles.ClearSelectedTiles();
            selectedUnit.GetComponent<UnitAttack>().DisplayAttack();
        }
    }

    //Display the current possible tiles and units the chosen unit can hit with a power
    public void DisplayPower(int pIndex)
    {
        if (selectedUnit)
        {
            e_playerState = PlayerState.UnitPower;

            //Check for 
            if (pIndex < selectedUnit.Powers.Length)
            {
                selectedPower = selectedUnit.Powers[pIndex];
                m_power.CurrentPower = selectedPower;
            }
        }
            
    }

    //Power target is updated every frame if being displayed
    public void ProcessPowerType(Tile chosen)
    {
        if (!m_power.CurrentPower)
        {
            //Debug.Log("No Power Selected");
            return;
        }
        tilesInPower = m_power.ProcessPowerType(chosen, selectedUnit.CurrentTile);

        m_tiles.ClearSelectedTiles();
        
        foreach(Tile t in tilesInPower)
        {
            t.Adjacent = true;
        }

        //chosen.Selected = true;
    }

    //Resolve an attack against a unit
    private void ResolveAttack(Tile chosen)
    {
        //Get if there is an attackable unit on the tile
        if (selectedUnit.GetComponent<UnitAttack>().Attack(chosen))
        {
            selectedUnit.EndTurn();
            e_playerState = PlayerState.Default;
        }
        else
        {
            CancelAction();
        }
    }

    //Resolve a move action
    private void ResolveMove(Tile chosen)
    {
        if (selectedUnit.CurrentTile)
        {
            bool validMove = false;

            UnitMove u_move = selectedUnit.gameObject.GetComponent<UnitMove>();

            if (chosen != selectedUnit.CurrentTile) validMove = u_move.TileInMove(chosen);

            if (validMove)
            {
                u_move.MoveTo(chosen);
                DisplayMove();
            }
            else
            {
                CancelAction();
            }
        }
        else
        {
            CancelAction();
        }
    }

    //Resolve the use of a power
    private void ResolvePower(Tile chosen)
    {
        unitsInPower.Clear();
        foreach(Tile t in tilesInPower)
        {
            if(t.Occupied)
            {
                unitsInPower.Add(t.OccupiedBy);
            }
        }
        selectedPower.UsePower(tilesInPower, unitsInPower);

        //Unit's turn ends after using a power
        selectedUnit.EndTurn();

        CancelAction();
    }

    //Clear the currently chosen tiles and units
    public void ClearSelections()
    {
        selectedUnit.Selected = false;
        selectedUnit = null;
        m_tiles.ClearSelectedTiles();
    }

    //Cancel the currently displayed action
    public void CancelAction()
    {
        selectedUnit = null;
        m_tiles.ClearSelectedTiles();
        m_hud.HideUnitInfo();
        e_playerState = PlayerState.Default;
    }

    //Start the player's turn
    public void StartTurn()
    {
        selectedUnit = null;
        e_playerState = PlayerState.Default;
    }

    //End the player's turn
    public void EndTurn()
    {
        selectedUnit = null;
        m_tiles.ClearSelectedTiles();
        e_playerState = PlayerState.Default;
    }
}