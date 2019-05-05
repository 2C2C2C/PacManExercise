using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MacManTools;

public class TuringAroundSystem : MonoBehaviour
{

    private void OnEnable()
    {
        MacManTools.Evently.Instance.Subscribe<TuringAroundEvent>(OnTuringAround);
    }

    private void OnDisable()
    {
        MacManTools.Evently.Instance.UnSubscribe<TuringAroundEvent>(OnTuringAround);
    }

    private void OnTuringAround(TuringAroundEvent evt)
    {
        Debug.Log($"{evt.go.name} is turing around");
    }

    // class end
}
