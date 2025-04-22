using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/LifeEventSO")]
public class LifeEventSO : ScriptableObject
{
    public Action<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        OnEventRaised?.Invoke(value);
    }
}