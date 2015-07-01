using UnityEngine;
using System.Collections;

public class se_DOT : StatusEffect {

    private float _damage;

    public float Damage
    {
        get { return _damage; }
        set { if (value < 0) _damage = 0; else _damage = value; }
    }

    public se_DOT(string from, int duration, float damage) :
        base(from, duration)
    {
        this.Damage = damage;
    }
}
