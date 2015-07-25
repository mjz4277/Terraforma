using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAttack : MonoBehaviour, ICombat
{

    Unit unit;
    TileManager m_tiles;

    public int range = 1;
    public float attack = 1.0f;

    private List<Tile> possibleRange = new List<Tile>();

    public List<Tile> PossibleRange
    {
        get { return possibleRange; }
    }

    // Use this for initialization
    void Start()
    {
        unit = GetComponent<Unit>();
        m_tiles = GameObject.FindGameObjectWithTag("GameController").GetComponent<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayAttack()
    {
        possibleRange.Clear();
        m_tiles.ClearSelectedTiles();
        
        possibleRange = m_tiles.GetPossibleTiles(unit.CurrentTile, unit.Range, true);

        foreach (Tile t in possibleRange)
        {
            t.Adjacent = true;

            if(t.Occupied)
            {
                if(t.OccupiedBy.Team != unit.Team)
                {
                    t.Adjacent = false;
                    t.Selected = true;
                }
            }
        }
    }

    public bool TileInRange(Tile t)
    {
        return possibleRange.Contains(t);
    }

    public bool Attack(Tile t)
    {
        if(TileInRange(t) && t.Occupied)
        {
            if(t.OccupiedBy.Team != unit.Team)
            {
                AttackUnit(t.OccupiedBy);
                return true;
            }
        }

        return false;
    }

    public bool Attack(Unit u)
    {
        if (TileInRange(u.CurrentTile))
        {
            if (u.Team != unit.Team)
            {
                AttackUnit(u);
                return true;
            }
        }

        return false;
    }

    public virtual void AttackUnit(Unit enemy)
    {
        float attackMod = (unit.Attack + 1) * 10;
        float defense = enemy.PhysicalDefense * 5;
        enemy.TakeDamage(attackMod - defense);
    }


}
