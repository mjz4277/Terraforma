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
    private HUDManager m_hud;
    private PowerManager m_power;

    private Unit selectedUnit;
    private Power selectedPower;

    private List<Tile> possibleTiles;
    private List<Unit> possibleUnits;

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
        m_hud = GameObject.FindGameObjectWithTag("GameController").GetComponent<HUDManager>();

        possibleTiles = new List<Tile>();
        possibleUnits = new List<Unit>();

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
        //If the selected piece is in attack mode
        if (selectedUnit && e_playerState == PlayerState.UnitAttack)
        {
            if (possibleUnits.Contains(chosen))
            {
                selectedUnit.AttackUnit(chosen);
                e_playerState = PlayerState.Default;
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
                if (selectedUnit.PossibleMove > 0)
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
                    if(selectedUnit.PossibleMove > 0)
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
        m_tiles.ClearSelectedTiles();
        possibleTiles.Clear();
        if (selectedUnit.PossibleMove > 0)
            possibleTiles = m_tiles.GetPossibleTiles(selectedUnit.CurrentTile, selectedUnit.PossibleMove, false);

        foreach (Tile t in possibleTiles)
        {
            t.Adjacent = true;
        }
    }

    //Display the current possible tiles and units the chosen unit can attack
    public void DisplayAttack()
    {
        if (selectedUnit.CanAttack)
        {
            e_playerState = PlayerState.UnitAttack;
            m_tiles.ClearSelectedTiles();
            possibleTiles.Clear();
            possibleUnits.Clear();
            possibleTiles = m_tiles.GetPossibleTiles(selectedUnit.CurrentTile, selectedUnit.Range, true);
            foreach (Tile t in possibleTiles)
            {
                t.Adjacent = true;
                if (t.Occupied)
                {
                    Unit enemy = t.OccupiedBy;
                    if (enemy.Team != selectedUnit.Team)
                    {
                        possibleUnits.Add(enemy);
                        t.Selected = true;
                    }
                }
            }
        }
    }

    //Display the current possible tiles and units the chosen unit can hit with a power
    public void DisplayPower(int pIndex)
    {
        e_playerState = PlayerState.UnitPower;

        //Check for 
        if(pIndex < selectedUnit.Powers.Length)
        {
            selectedPower = selectedUnit.Powers[pIndex];
            m_power.CurrentPower = selectedPower;
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
        if (chosen.Occupied)
        {
            Unit u = chosen.OccupiedBy;
            if (u.Team != player.Team)
            {
                selectedUnit.AttackUnit(u);
                e_playerState = PlayerState.Default;
            }
            else
            {
                CancelAction();
            }
        }
    }

    //Resolve a move action
    private void ResolveMove(Tile chosen)
    {
        if (selectedUnit.CurrentTile)
        {
            bool validMove = false;

            //Check if tile selected is within the range of possible moves
            foreach (Tile t in possibleTiles)
            {
                //Don't count clicking the current tile as a move action
                if (chosen == selectedUnit.CurrentTile) break;

                if (chosen == t)
                {
                    validMove = true;
                    break;
                }
            }
            if (validMove)
            {
                selectedUnit.MoveTo(chosen);
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