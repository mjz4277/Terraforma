using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMovable
{
    List<Tile> DisplayMove();
    void MoveTo(Tile t);
}

public interface ICombat
{
    List<Tile> DisplayAttack();


}

public interface IDamageable
{
    void Damage(float taken);
}

public interface IKillable
{
    void Kill();
}
