using System.Collections;
using System.Collections.Generic;

public class PocoComponent<T> where T : class, new()
{
    static T instance;
    public static T Instance => instance ?? (instance = new T());
}
