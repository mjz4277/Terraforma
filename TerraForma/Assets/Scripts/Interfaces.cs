using UnityEngine;
using System.Collections;

public interface IMovable
{
    void DisplayMove();
    void MoveTo(Tile t);
}

public interface IDamageable
{
    void Damage(float taken);
}

public interface IKillable
{
    void Kill();
}
