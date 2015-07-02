using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SEManager : MonoBehaviour {

    private List<StatusEffect> movementEffects = new List<StatusEffect>();
    private List<StatusEffect> attackEffects = new List<StatusEffect>();
    private List<StatusEffect> buffEffects = new List<StatusEffect>();
    private List<StatusEffect> debuffEffects = new List<StatusEffect>();

    public List<StatusEffect> MovementEffects
    {
        get { return movementEffects; }
    }

    public List<StatusEffect> AttackEffects
    {
        get { return attackEffects; }
    }

    public List<StatusEffect> BuffEffects
    {
        get { return buffEffects; }
    }

    public List<StatusEffect> DebuffEffects
    {
        get { return debuffEffects; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddStatusEffect(StatusEffect se)
    {
        if (se is se_Slow || se is se_Immobilize)
        {
            movementEffects.Add(se);
        }
        if (se is se_Silence || se is se_Disarm)
        {
            attackEffects.Add(se);
        }
        if (se is se_DOT)
        {
            debuffEffects.Add(se);
        }
    }

    public void IncrementEffects()
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
    }
}
