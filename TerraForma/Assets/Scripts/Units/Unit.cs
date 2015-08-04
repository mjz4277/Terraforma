using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour
{

    protected UnitMove unitMove;

    protected MeshRenderer meshRenderer;

    protected Material mat_default;
    protected Material mat_selected;
    protected Material mat_ended;

    protected Shader shader_selected;
    protected Shader shader_default;

    protected string _name;
    protected Element _element;
    protected float _health;
    protected float _mana;

    protected UnitStats stats;

    protected Power[] powers = new Power[4];

    protected bool _isDead;

    protected bool _selected = false;
    protected bool _canAttack = true;
    protected bool _canUsePower = true;
    protected bool _turnOver = false;

    protected int _team;

    protected Tile _currentTile;
    protected int _possibleMove;

    protected SEManager m_statusEffects;

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
        get { return stats.Move; }
    }

    public float Attack
    {
        get { return stats.Attack; }
    }

    public float PhysicalDefense
    {
        get { return stats.PhysicalDefense; }
    }

    public float MagicDefense
    {
        get { return stats.MagicDefense; }
    }

    public int Range
    {
        get { return stats.Range; }
    }

    public float MaxHealth
    {
        get { return stats.Health; }
    }

    public float Health
    {
        get { return _health; }
        set { if (value < 0) _health = 0; else if (value > stats.Health) _health = stats.Health; else _health = value; }
    }

    public float MaxMana
    {
        get { return stats.Mana; }
    }

    public float Mana
    {
        get { return _mana; }
        set { if (value < 0) _mana = 0; else if (value > stats.Mana) _mana = stats.Mana; else _mana = value; }
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
        _possibleMove = stats.Move;
        this.CanAttack = true;
        this.TurnOver = false;
    }

    protected virtual void Init()
    {
        unitMove = GetComponent<UnitMove>();
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        mat_ended = Resources.Load<Material>("Materials/Unit_Turn_Over");
        stats = gameObject.GetComponent<UnitStats>();
        m_statusEffects = gameObject.GetComponent<SEManager>();
        LoadResources();
        SetStats();
        SetPowers();
    }

    protected void SetPowers()
    {
        Power[] possiblePowers = GetComponents<Power>();
        for(int i = 0; i < 4; i++)
        {
            if(i < possiblePowers.Length)
            {
                powers[i] = possiblePowers[i];
            }
        }
    }

    public abstract void LoadResources();

    protected abstract void SetStats();

    public virtual void SnapToCurrent()
    {
        Transform[] trans = _currentTile.gameObject.GetComponentsInChildren<Transform>();
        Transform point = null;
        for (int i = 0; i < trans.Length; i++)
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
        m_statusEffects.AddStatusEffect(se);
    }

    public virtual void AttackUnit(Unit enemy)
    {
        float attack = (stats.Attack + 1) * 10;
        float defense = enemy.PhysicalDefense * 5;
        enemy.TakeDamage(attack - defense);

        //Unit's turn ends after attacking
        EndTurn();
    }

    public virtual void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        _isDead = true;
        //Destroy(this.gameObject);
    }

    public void CalculateNextTurnStats()
    {
        _possibleMove = stats.Move;
        foreach (StatusEffect se in m_statusEffects.MovementEffects)
        {
            if (se is se_Immobilize)
            {
                _possibleMove = 0;
                break;
            }
            if (se is se_Slow)
            {
                se_Slow se_slow_cast = (se_Slow)se;
                _possibleMove -= se_slow_cast.Amount;

                //Cannot slow unit to 0
                if (_possibleMove < 1) _possibleMove = 1;
            }
        }

        _canAttack = true;
        _canUsePower = true;
        foreach (StatusEffect se in m_statusEffects.AttackEffects)
        {
            if (se is se_Silence)
            {
                _canUsePower = false;
            }
            if (se is se_Disarm)
            {
                _canAttack = false;
            }
            if (se is se_Stun)
            {
                EndTurn();
                break;
            }
        }

        foreach (StatusEffect se in m_statusEffects.DebuffEffects)
        {
            if (se is se_DOT)
            {
                se_DOT se_dot_cast = (se_DOT)se;
                TakeDamage(se_dot_cast.Damage);
            }
        }

        //TODO: Get effects from tile
    }

    public void EndTurn()
    {
        this.PossibleMove = 0;
        this.CanAttack = false;
        this.TurnOver = true;
    }

    public void ChangeTurn()
    {
        m_statusEffects.IncrementEffects();

        this.TurnOver = false;
    }
}
