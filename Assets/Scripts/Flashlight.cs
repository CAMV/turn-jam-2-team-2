using System.Collections.Generic;
using TurnJam2.Interfaces;
using UnityEngine;

namespace TurnJam2
{
    public class Flashlight : MonoBehaviour
    {
        [SerializeField] private Light _light;
        [SerializeField] private float _maxIntensity = 5f;
        
        public Light Light => _light;
        public float MaxIntensity => _maxIntensity;
        
        private readonly List<ILightInteractable> _affectedLightInteractables = new();
        
        public ILightInteractable ClosestLightInteractor { get; private set; }
        
        private void Awake()
        {
            ResetIntensity();
        }
    
        private void ResetIntensity()
        {
            _light.intensity = _maxIntensity;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ILightInteractable>(out var lightInteractable))
            {
                _affectedLightInteractables.Add(lightInteractable);
                UpdateClosestGlassPanel();
                lightInteractable.TurnOn(this);
            }
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ILightInteractable>(out var lightInteractable))
            {
                _affectedLightInteractables.Remove(lightInteractable);
                UpdateClosestGlassPanel();
                
                ResetIntensity();
                lightInteractable.TurnOff();
            }
        }
    
        private void UpdateClosestGlassPanel()
        {
            var minDistance = float.MaxValue;
            
            foreach (var lightInteractable in _affectedLightInteractables)
            {
                var distance = Vector3.Distance(lightInteractable.gameObject.transform.position, transform.position);
                if (!(distance < minDistance)) continue;
                ClosestLightInteractor = lightInteractable;
                minDistance = distance;
            }
        }
    }
}

