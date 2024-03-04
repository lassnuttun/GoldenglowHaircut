using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProperty<T> where T : class
{
    T Get();
    void Set(T obj);
}
