using UnityEngine;

namespace TurnJam2.Interfaces
{
    public interface ILightInteractable
    {
        GameObject gameObject { get; }
        void TurnOn(Flashlight flashLight);
        void TurnOff();
    }
}