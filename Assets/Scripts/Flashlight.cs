using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _maxIntensity = 5f;
    
    public Light Light => _light;
    public float MaxIntensity => _maxIntensity;
    
    private readonly List<GlassPanel> _affectedGlassPanels = new();
    
    public GlassPanel ClosestGlassPanel { get; private set; }
    
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
        if (other.TryGetComponent<GlassPanel>(out var glassPanel))
        {
            _affectedGlassPanels.Add(glassPanel);
            UpdateClosestGlassPanel();
            
            glassPanel.UpdateLight(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<GlassPanel>(out var glassPanel))
        {
            _affectedGlassPanels.Remove(glassPanel);
            UpdateClosestGlassPanel();
            
            ResetIntensity();
            glassPanel.DisableSpotlight();
        }
    }

    private void UpdateClosestGlassPanel()
    {
        var minDistance = float.MaxValue;
        
        foreach (var panel in _affectedGlassPanels)
        {
            var distance = Vector3.Distance(panel.transform.position, transform.position);
            if (distance < minDistance)
            {
                ClosestGlassPanel = panel;
                minDistance = distance;
            }
        }
    }
}
