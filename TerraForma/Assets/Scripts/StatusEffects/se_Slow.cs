using UnityEngine;
using System.Collections;

public class se_Slow : StatusEffect {

    protected int _amount;

    public int Amount
    {
        get { return _amount; }
        set { if (value < 0) _amount = 0; else _amount = value; }
    }

    public se_Slow(string from, int duration, int amount) :
        base(from, duration)
    {
        this.Amount = amount;
    }
}
