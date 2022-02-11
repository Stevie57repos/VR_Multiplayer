using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script1 : MonoBehaviour
{
    public int Health = 1;

    private int _privateNumber = 2;

    public int GiveHealthNumber()
    {
        return Health;
    }
}
