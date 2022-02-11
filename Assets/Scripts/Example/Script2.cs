using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script2 : MonoBehaviour
{   
    private Script1 _script1;

    Transform _transform;

    private void Start()
    {
       _script1 = GetComponent<Script1>();
        _script1.GiveHealthNumber();
    }
}
