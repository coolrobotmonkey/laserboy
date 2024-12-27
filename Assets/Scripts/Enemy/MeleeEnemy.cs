using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void Die()
    {
        Debug.Log("Melee enemy died.");
        Destroy(gameObject);
    }
}
