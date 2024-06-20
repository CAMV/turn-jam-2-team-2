using UnityEngine;

public class GlassPanel : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    
    [SerializeField] private Light _spotLight;
    [SerializeField] private Color _color;
    [SerializeField] private float _rangeMultiplier;
    [SerializeField] private MeshRenderer _glassMR;

    private Flashlight _lightSource;

    private void Update()
    {
        if (!_spotLight.gameObject.activeSelf) return;
        
        UpdateLight(_lightSource);
    }
    
    public void UpdateLight(Flashlight lightSource)
    {
        _lightSource = lightSource;
        
        _spotLight.gameObject.SetActive(true);
        
        var distanceToPlayer = Vector3.Distance(_lightSource.transform.position, transform.position);
        var lightDir = _lightSource.transform.forward;
        var lghtDotFwrd = Mathf.Abs(Vector3.Dot(lightDir, transform.forward));
        var intensity = (_lightSource.MaxIntensity / Mathf.Pow(Mathf.Clamp(distanceToPlayer, 1, int.MaxValue), 2)) *
                        lghtDotFwrd;

        if (_lightSource.ClosestGlassPanel == this)
        {
            _lightSource.Light.intensity = _lightSource.MaxIntensity - intensity;
        }
        
        if (_glassMR != null)
        {
            var propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor(BaseColor, _color);
            propertyBlock.SetColor(EmissionColor, _color * intensity / lightSource.Light.intensity);
            _glassMR.SetPropertyBlock(propertyBlock);
        }

        _spotLight.intensity = intensity * (_rangeMultiplier * _rangeMultiplier);
        _spotLight.range = (lightSource.Light.range - distanceToPlayer) * _rangeMultiplier;
        _spotLight.color = lightSource.Light.color * _color;

        _spotLight.spotAngle = 128 / Mathf.Clamp(_rangeMultiplier, 1, 4);
        _spotLight.innerSpotAngle = Mathf.Lerp(0, _spotLight.spotAngle,
            (1 - (distanceToPlayer / lightSource.Light.range)) * lghtDotFwrd);

        var escalar = Vector3.Dot(lightDir, transform.right) > 0 ? 1 : -1;
        var angle = Vector3.Angle(transform.forward, lightDir) * escalar;

        if (Mathf.Abs(angle) > 90)
        {
            angle = Vector3.Angle(-transform.forward, lightDir) * -escalar;
            _spotLight.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(angle + 180, 140, 220), 0);
        }
        else
        {
            _spotLight.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(angle, -40, 40), 0);
        }
    }
    
    public void DisableSpotlight()
    {
        _spotLight.gameObject.SetActive(false);
    }
}