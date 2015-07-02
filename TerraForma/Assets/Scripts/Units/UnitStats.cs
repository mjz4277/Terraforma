using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

    [SerializeField]
    private int _move;
    [SerializeField]
    protected float _attack;
    [SerializeField]
    protected float _physicalDefense;
    [SerializeField]
    protected float _magicDefense;
    [SerializeField]
    protected int _range;
    [SerializeField]
    protected float _health;
    [SerializeField]
    protected float _mana;

    public int Move
    {
        get { return _move; }
        set { if (value < 0) _move = 0; else _move = value; }
    }

    public float Attack
    {
        get { return _attack; }
        set { if (value < 0) _attack = 0; else _attack = value; }
    }

    public float PhysicalDefense
    {
        get { return _physicalDefense; }
        set { if (value < 0) _physicalDefense = 0; else _physicalDefense = value; }
    }

    public float MagicDefense
    {
        get { return _magicDefense; }
        set { if (value < 0) _magicDefense = 0; else _magicDefense = value; }
    }

    public int Range
    {
        get { return _range; }
        set { if (value < 0) _range = 0; else _range = value; }
    }

    public float Health
    {
        get { return _health; }
        set { if (value < 0) _health = 0; else _health = value; }
    }

    public float Mana
    {
        get { return _mana; }
        set { if (value < 0) _mana = 0;  else _mana = value; }
    }


}
