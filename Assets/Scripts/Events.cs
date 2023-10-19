using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events 
{
    public static Action<bool,GameObject> EnemiesAdd;
    public static Action<bool,GameObject> EnemiesRemove;
    public static Action<float> BarSet;
    public static Action GameOver;
    public static Action GameWin;
    public static Action<int> EnemyAmount;
    public static Action<int> EnemyDeadCounter;
}
