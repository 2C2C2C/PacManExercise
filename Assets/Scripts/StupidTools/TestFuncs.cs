using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestFuncs : MonoBehaviour
{

    // just for test

    public struct Kemono
    {
        public int health;
        public int mana;
    }

    [SerializeField]
    List<int> intListA;
    [SerializeField]
    List<bool> rst;

    public void TestFunc1()
    {
        intListA = new List<int>();

        intListA.Add(1);
        intListA.Add(2);
        intListA.Add(3);
        intListA.Add(4);

        var Kemono = new List<Kemono>()
        {

        };


        var rstA = intListA.Select(num => num).ToList();
        var rstB = intListA.OrderBy(num => num).ToList();

    }



    // class end    
}

