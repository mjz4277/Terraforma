using UnityEngine;
using System.Collections;

public class StatusEffect {

    protected string _from;
    protected int _duration;

    public string From
    {
        get { return _from; }
        set { _from = value; }
    }

    public int Duration
    {
        get { return _duration; }
        set { if (value < 0) _duration = 0; else _duration = value; }
    }

    public StatusEffect(string from, int duration)
    {
        _from = from;
        this.Duration = duration;
    }

}
