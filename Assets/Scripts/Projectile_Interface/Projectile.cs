using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Projectile
{
    void Launch(Vector3 direction);
    void Damage();
}
