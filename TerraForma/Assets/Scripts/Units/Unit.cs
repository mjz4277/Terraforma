using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour {

    protected MeshRenderer meshRenderer;

    protected Material mat_default;
    protected Material mat_selected;
    protected Material mat_ended;

    protected Shader shader_selected;
    protected Shader shader_default;

    protected string _name;
    protected Element _element;
    protected int _move;
    protected float _attack;
    protected float _defense;
    protected int _range;
    protected float _maxHealth;
    protected float _health;
    protected float _maxMana;
    protected float _mana;

    protected Power[] powers = new Power[4];

    protected bool _isDead;

    protected bool _selected = false;
    protected bool _canAttack = true;
    protected bool _canUsePower = true;
    protected bool _turnOver = false;

    protected int _team;

    protected Tile _currentTile;
    protected int _possibleMove;

    protected List<StatusEffect> movementEffects = new List<StatusEffect>();
    protected List<StatusEffect> attackEffects = new List<StatusEffect>();
    protected List<StatusEffect> buffEffects = new List<StatusEffect>();
    protected List<StatusEffect> debuffEffects = new List<StatusEffect>();

    public string Name
    {
        get { return _name; }
    }

    public Element Element
    {
        get { return _element; }
    }
    public int Move
    {
        get { return _move; }
    }

    public float Attack
    {
        get { return _attack; }
    }

    public float Defense
    {
        get { return _defense; }
    }

    public int Range
    {
        get { return _range; }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
    }

    public float Health
    {
        get { return _health; }
        set { if (value < 0) _health = 0; else if (value > _maxHealth) _health = _maxHealth; else _health = value; }
    }

    public float MaxMana
    {
        get { return _maxMana; }
    }

    public float Mana
    {
        get { return _mana; }
        set { if (value < 0) _mana = 0; else if (value > _maxMana) _mana = _maxMana; else _mana = value; }
    }

    public Power[] Powers
    {
        get { return powers; }
    }

    public bool CanAttack
    {
        get { return _canAttack; }
        set { _canAttack = value; }
    }

    public bool CanUsePower
    {
        get { return _canUsePower; }
        set { _canUsePower = value; }
    }

    public Tile CurrentTile
    {
        get { return _currentTile; }
        set { _currentTile = value; }
    }

    public int PossibleMove
    {
        get { return _possibleMove; }
        set { _possibleMove = value; }
    }

    public int Team
    {
        get { return _team; }
        set { _team = value; }
    }

    public bool TurnOver
    {
        get { return _turnOver; }
        set 
        { 
            _turnOver = value;
            if (_turnOver) meshRenderer.material = mat_ended;
            else meshRenderer.material = mat_default;
        }
    }

    public bool Selected
    {
        get { return _selected; }
        set 
        { 
            _selected = value;
            if (_selected) meshRenderer.material.shader = shader_selected;
            else meshRenderer.material.shader = shader_default;
        }
    }

    public virtual void ResetUnit()
    {
        _possibleMove = _move;
        this.CanAttack = true;
        this.TurnOver = false;
    }

    protected virtual void Init()
    {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        mat_ended = Resources.Load<Material>("Materials/Unit_Turn_Over");
        LoadResources();
        SetStats();
        SetPowers();
    }

    protected abstract void SetPowers();

    public abstract void LoadResources();
    protected abstract void SetStats();

    public virtual void SnapToCurrent()
    {
        Transform[] trans = _currentTile.gameObject.GetComponentsInChildren<Transform>();
        Transform point = null;
        for (int i = 0; i < trans.Length; i++ )
        {
            if (trans[i].name == "p_piece_center")
                point = trans[i];
        }
        if (point)
        {
            Vector3 off = new Vector3(0.0f, 0.5f, 0.0f);
            gameObject.transform.position = point.position + off;
        }
    }

    public void AddStatusEffect(StatusEffect se)
    {
        if(se is se_Slow || se is se_Immobilize)
        {
            movementEffects.Add(se);
        }
        if(se is se_Silence || se is se_Disarm)
        {
            attackEffects.Add(se);
        }
        if(se is se_DOT)
        {
            debuffEffects.Add(se);
        }
    }

    public virtual void AttackUnit(Unit enemy)
    {
        float attack = (_attack + 1) * 10;
        float defense = enemy.Defense * 5;
        enemy.TakeDamage(attack - defense);

        //Unit's turn ends after attacking
        this.PossibleMove = 0;
        this.CanAttack = false;
        this.TurnOver = true;
    }

    public void UsePower(int index, List<Tile> tiles, List<Unit> units)
    {
        powers[index].UsePower(tiles, units);
    }

    public virtual void TakeDamage(float amount)
    {
        _health -= amount;
        if(_health <= 0)
        {
            _isDead = true;
        }
    }

    public void CalculateNextTurnStats()
    {
        _possibleMove = _move;
        foreach(StatusEffect se in movementEffects)
        {
            if(se is se_Immobilize)
            {
                _possibleMove = 0;
                break;
            }
            if(se is se_Slow)
            {
                se_Slow se_slow_cast = (se_Slow)se;
                _possibleMove -= se_slow_cast.Amount;

                //Cannot slow unit to 0
                if (_possibleMove < 1) _possibleMove = 1;
            }
        }

        _canAttack = true;
        _canUsePower = true;
        foreach(StatusEffect se in attackEffects)
        {
            if(se is se_Silence)
            {
                _canUsePower = false;
            }
            if(se is se_Disarm)
            {
                _canAttack = false;
            }
            if(se is se_Stun)
            {
                _canAttack = false;
                _canUsePower = false;
                _possibleMove = 0;
                break;
            }
        }

        foreach(StatusEffect se in debuffEffects)
        {
            if (se is se_DOT)
            {
                se_DOT se_dot_cast = (se_DOT)se;
                TakeDamage(se_dot_cast.Damage);
            }
        }

        //Get effects from tile
    }

    public void EndTurn()
    {
        List<StatusEffect> removalList = new List<StatusEffect>();
        foreach(StatusEffect se in movementEffects)
        {
            se.Duration--;
            if (se.Duration <= 0) removalList.Add(se);
        }

        foreach(StatusEffect se in removalList)
        {
            movementEffects.Remove(se);
        }

        removalList.Clear();
        foreach (StatusEffect se in attackEffects)
        {
            se.Duration--;
            if (se.Duration <= 0) removalList.Add(se);
        }

        foreach (StatusEffect se in removalList)
        {
            attackEffects.Remove(se);
        }

        removalList.Clear();
        foreach (StatusEffect se in debuffEffects)
        {
            se.Duration--;
            if (se.Duration <= 0) removalList.Add(se);
        }

        foreach (StatusEffect se in removalList)
        {
            debuffEffects.Remove(se);
        }

        this.TurnOver = false;

    }
}
