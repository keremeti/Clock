using System;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent<DateTime> OnTimeUpdated =
        new UnityEvent<DateTime>();
    public static UnityEvent<string> OnDropdownItemSelected =
        new UnityEvent<string>();
    public static UnityEvent<HandsRotation> OnSelectHand =
        new UnityEvent<HandsRotation>();
    public static UnityEvent OnDeselectHand =
        new UnityEvent();
    public static UnityEvent OnDeselectInputField =
        new UnityEvent();
    public static UnityEvent OnChangeOrientation =
        new UnityEvent();

    public static void SendTimeUpdated(DateTime _usedTime)
    {
        OnTimeUpdated.Invoke(_usedTime);
    }

    public static void SendDropdownItemSelected(string dropdownItem)
    {
        OnDropdownItemSelected.Invoke(dropdownItem);
    }

    public static void SendSelectHand(HandsRotation handsRotation)
    {
        OnSelectHand.Invoke(handsRotation);
    }

    public static void SendDeselectHand()
    {
        OnDeselectHand.Invoke();
    }

    public static void SendDeselectInputField()
    {
        OnDeselectInputField.Invoke();
    }

    public static void SendChangeOrientation()
    {
        OnChangeOrientation.Invoke();
    }
}
