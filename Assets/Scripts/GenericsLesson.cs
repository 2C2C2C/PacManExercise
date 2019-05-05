using System.Collections.Generic;
using UnityEngine;



public class GenericsLesson : MonoBehaviour
{

    //public MuyDelegateA<>
    public System.Func<int, bool> tf1;

    private void Start()
    {
        //GetTheThing(5);
        //GetTheThing("string wa");
        //GetTheThing(5.5f);

        //del1 = FooStatic.FooT;

        //tf1 = NumLessThan5;
        //del1 = NumLessThan5;

        //Debug.Log(del1?.Invoke(6));
        //Debug.Log(del1?.Invoke(2));
        //Debug.Log(del1(2));
        //Debug.Log(tf1?.Invoke(3));

        List<int> ml1 = new List<int>()
        {
            1,5,7,8,2,3,9
        };

        Debug.Log("before");
        string str1 = "";
        foreach (var item in ml1)
        {
            str1 += item.ToString();
        }
        Debug.Log(str1);


        Debug.Log("after");
        string str2 = "";
        //ml1 = RunTestWa(ml1, NumLessThan5);
        ml1 = RunTestWa(ml1, i => i > 6);
        foreach (var item in ml1)
        {
            str2 += item.ToString();
        }
        Debug.Log(str2);
    }


    public bool NumLessThan5(int _num)
    {
        return _num < 5;
    }



    public void TestFunWa()
    {
        Debug.Log("test func run wa");
    }

    private void GetTheThing<T>(T _theThing)
    {
        Debug.Log($"the thing is {_theThing.GetType()}");
    }



    public static List<T> RunTestWa<T>(List<T> _nums, MuyDelegateA<T> muyDele)
    {
        Debug.Log($"{muyDele.Target} 's type is {muyDele.Target.GetType()} ");
        var newItems = new List<T>();
        foreach (var nt in _nums)
        {
            if (muyDele(nt))
                newItems.Add(nt);
        }
        return newItems;
    }

}

public delegate bool MuyDelegateA<T>(T _someThing);



public static class FooStatic
{
    public static void FooT()
    {
        Debug.Log("FooT");
    }
}


public class MuyPair<T, U> /*where T : System.Object*/
{
    public T p1;
    public U p2;

    public void ShowTheStuff()
    {
        Debug.Log($" P1 is {p1} and p2 is {p2} wa");
    }

}