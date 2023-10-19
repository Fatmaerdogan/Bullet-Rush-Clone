
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigController : EnemyController
{
    public override void GetShot(GameObject Other)
    {
        if (transform.localScale.y > 2)
        {
            Other.SetActive(false);
            transform.localScale -= Vector3.one;
            transform.position -= Vector3.up;
        }
        else
        {
            base.GetShot(Other);
        }

    }
}
