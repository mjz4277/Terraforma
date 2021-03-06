﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Element
{
    Earth,
    Water,
    Fire,
    Air,
    Nature,
    Mech,
    Electric,
    Ice,
    Dark,
    Light
}

public class Player : MonoBehaviour {

    private PlayerInput input;
    private PlayerController controller;

    string _name;
    float _manaRegen;
    float _maxMana;
    float _mana;
    int _level;
    int _ap_total;
    int _ap_current;

    int _team;

    Element[] elements = new Element[4];

    List<Unit> units = new List<Unit>();

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public float ManaRegen
    {
        get { return _manaRegen; }
        set { if (value < 0) _manaRegen = value; }
    }

    public float Mana
    {
        get { return _mana; }
        set { _mana = value; }
    }

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public int TotalActionPoints
    {
        get { return _ap_total; }
        set { if (value < 0) _ap_total = 0; else _ap_total = value; }
    }

    public int CurrentActionPoints
    {
        get { return _ap_current; }
        set { if (value < 0) _ap_current = 0; else _ap_current = value; }
    }

    public int Team
    {
        get { return _team; }
        set { _team = value; }
    }

    public Element[] Elements
    {
        get { return elements; }
    }

    public Element Element0
    {
        get { return elements[0]; }
        set { elements[0] = value; }
    }

    public Element Element1
    {
        get { return elements[1]; }
        set { elements[1] = value; }
    }

    public Element Element2
    {
        get { return elements[2]; }
        set { elements[2] = value; }
    }

    public Element Element3
    {
        get { return elements[3]; }
        set { elements[3] = value; }
    }

    public List<Unit> Units
    {
        get { return units; }
    }

    public void AddUnit(Unit u)
    {
        units.Add(u);
    }

    public void Init()
    {
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();
        _maxMana = 10;
        _manaRegen = 1;
        _mana = 10;
        _level = 1;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartTurn()
    {
        input.IsTurn = true;
        controller.StartTurn();
        RegenMana();
        ResetAP();
    }

    public void EndTurn()
    {
        input.IsTurn = false;
        controller.EndTurn();
    }

    public void RegenMana()
    {
        _mana += _manaRegen;
        if (_mana > _maxMana) _mana = _maxMana;
    }

    public void ResetAP()
    {
        _ap_current = _ap_total;
    }
}
