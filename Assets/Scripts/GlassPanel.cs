using UnityEngine;

public class GlassPanel : MonoBehaviour
{
    [SerializeField]
    private Light _spotLight;
    [SerializeField]
    private Color _color;
    [SerializeField]
    private float _rangeMultiplier;

    [SerializeField]
    private MeshRenderer _glassMR;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Locator.GameManager.SubscribeGlassLight(UpdateLight);
    }

    // Update is called once per frame
    void UpdateLight(Vector3 playerPos, Light playerLight)
    {
        var distanceToPlayer = Vector3.Distance(playerPos, transform.position);
        var lightDir = (transform.position - playerPos).normalized;
        var lghtDotFwrd = Mathf.Abs(Vector3.Dot(lightDir, transform.forward));
        var intensity = (playerLight.intensity / Mathf.Pow(Mathf.Clamp(distanceToPlayer, 1, int.MaxValue), 2)) * lghtDotFwrd;

        _spotLight.gameObject.SetActive(distanceToPlayer < playerLight.range);

        if(_glassMR != null)
        {
            var propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_BaseColor", _color);
            propertyBlock.SetColor("_EmissionColor",  _color * intensity / playerLight.intensity);
            _glassMR.SetPropertyBlock(propertyBlock);
        }

        _spotLight.intensity = intensity * (_rangeMultiplier * _rangeMultiplier);
        _spotLight.range = (playerLight.range - distanceToPlayer) * _rangeMultiplier;
        _spotLight.color = playerLight.color * _color;

        _spotLight.spotAngle = 128 / Mathf.Clamp(_rangeMultiplier, 1, 4);
        _spotLight.innerSpotAngle = Mathf.Lerp(0, _spotLight.spotAngle, (1 - (distanceToPlayer / playerLight.range)) * lghtDotFwrd);

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
}
