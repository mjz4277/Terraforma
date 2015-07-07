using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour {

    private bool _isTurn = false;

    private PlayerController controller;

    public bool IsTurn
    {
        get { return _isTurn; }
        set { _isTurn = value; }
    }

	// Use this for initialization
	void Start () 
    {
        controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_isTurn)
        {
            if (controller.E_PlayerState == PlayerController.PlayerState.UnitPower)
            {
                GameObject powerSelection = GetSelectedObject();
                if (powerSelection && powerSelection.GetComponent<Tile>())
                {
                    Tile powerTile = powerSelection.GetComponent<Tile>();
                    controller.ProcessPowerType(powerTile);
                }
            }
            ProcessInput();
        }
	}

    private void ProcessInput()
    {
        GameObject selected;
        if (Input.GetMouseButtonDown(0))
        {
            selected = GetSelectedObject();
            if (selected != null)
            {
                ProcessSelection(selected);
            }
        }
        //Switch between move and attack mode
        if (Input.GetKeyDown(KeyCode.E))
        {
            controller.CycleAction();
        }

        //Choosing a power to use
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controller.DisplayPower(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controller.DisplayPower(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controller.DisplayPower(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            controller.DisplayPower(3);
        }

        //Cancel current action
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controller.CancelAction();
        }

        //End Turn
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controller.EndTurn();
        }
    }

    void ProcessSelection(GameObject selected)
    {
        //If a unit is selected
        if (selected.GetComponent<Unit>())
        {
            Unit u_chosen = selected.GetComponent<Unit>();
            controller.ResolveUnitSelection(u_chosen);
        }
        //If a tile is selected
        else if (selected.GetComponent<Tile>())
        {
            Tile t_chosen = selected.GetComponent<Tile>();
            controller.ResolveTileSelection(t_chosen);
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
