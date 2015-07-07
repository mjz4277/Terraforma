using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PowerType
{
    Burst,
    Area,
    Cone,
    Line,
    Biome
}

public enum PowerTarget
{
    Unit,
    Tile,
    Both
}

public class Power : MonoBehaviour {

    protected PowerType e_type;
    protected PowerTarget e_target;

    protected Unit _owner;

    [SerializeField]
    protected string _name;
    [SerializeField]
    protected int _cooldown;
    protected int _currCooldown;
    [SerializeField]
    protected float _manaCost;
    [SerializeField]
    protected float _damage;
    //private int slow;
    //private float reduceAtt;
    //private float reduceDef;
    //private bool canAttack;
    //private bool canUsePower;
    //private bool stunned;
    //private GameObject target;
    [SerializeField]
    protected int _range;
    [SerializeField]
    protected int _aoe;

    public Unit Owner
    {
        get { return _owner; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Cooldown
    {
        get { return _cooldown; }
        set { if (value < 0) _cooldown = 0; else _cooldown = value; }
    }

    public int CurrentCooldown
    {
        get { return _currCooldown; }
        set { if (value < 0) _currCooldown = 0; else _currCooldown = value; }
    }

    public float ManaCost
    {
        get { return _manaCost; }
        set { if (value < 0) _manaCost = 0; else _manaCost = value; }
    }

    public float Damage
    {
        get { return _damage; }
        set { if (value < 0) _damage = 0; else _damage = value; }
    }

    public int Range
    {
        get { return _range; }
        set { if (value < 0) _range = 0; else _range = value; }
    }

    public int AOE
    {
        get { return _aoe; }
        set { if (value < 0) _aoe = 0; else _aoe = value; }
    }

    public PowerTarget Target
    {
        get { return e_target; }
        set { e_target = value; }
    }

    public PowerType Type
    {
        get { return e_type; }
        set { e_type = value; }
    }

    public void Start()
    {
        _owner = GetComponent<Unit>();
    }

    public virtual void UsePower(List<Tile> tiles, List<Unit> units)
    {
        Debug.Log("Power [" + _name + "] used");
        _owner.Mana -= _manaCost;
        _currCooldown = _cooldown;
    }

    public void IncrementCooldown()
    {
        _currCooldown--;
        if (_currCooldown < 0) _currCooldown = 0;
    }
}
