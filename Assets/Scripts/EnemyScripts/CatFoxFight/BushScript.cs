using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour
{
    public FoxCombat foxCombat;


    public void StartAttack()
    {
        foxCombat.StartAttack();
    }

    public void StopAttack()
    {
        foxCombat.StopAttack();
    }
}
