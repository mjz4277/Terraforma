using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMovable
{
    void DisplayMove();
    bool MoveTo(Tile t);
}

public interface ICombat
{
    void DisplayAttack();


}

public interface IDamageable
{
    void Damage(float taken);
}

public interface IKillable
{
    void Kill();
}
