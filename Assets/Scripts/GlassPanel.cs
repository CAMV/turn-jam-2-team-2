using System;
using TurnJam2.Interfaces;
using UnityEngine;

namespace TurnJam2
{
    public class GlassPanel : MonoBehaviour, ILightInteractable
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        [SerializeField] private Light _spotLight;
        [SerializeField] private Color _color;
        [SerializeField] private float _rangeMultiplier;
        [SerializeField] private MeshRenderer _glassMR;

        private Flashlight _lightSource;
        private MaterialPropertyBlock _materialPropertyBlock;

        private void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();

            SetColor(_color, Color.black);
        }

        private void Update()
        {
            if (!_spotLight.gameObject.activeSelf) return;

            UpdateLight(_lightSource);
        }

        private void UpdateLight(Flashlight lightSource)
        {
            _lightSource = lightSource;

            _spotLight.gameObject.SetActive(true);

            var distanceToPlayer = Vector3.Distance(_lightSource.transform.position, transform.position);
            var lightDir = _lightSource.transform.forward;
            var lghtDotFwrd = Mathf.Abs(Vector3.Dot(lightDir, transform.forward));
            var intensity = (_lightSource.MaxIntensity / Mathf.Pow(Mathf.Clamp(distanceToPlayer, 1, int.MaxValue), 2)) *
                            lghtDotFwrd;

            if (ReferenceEquals(_lightSource.ClosestLightInteractor, this))
            {
                _lightSource.Light.intensity = _lightSource.MaxIntensity - intensity;
            }

            SetColor(_color, _color * intensity / lightSource.Light.intensity);

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

        private void SetColor(Color baseColor, Color emissionColor)
        {
            _materialPropertyBlock.SetColor(BaseColor, baseColor);
            _materialPropertyBlock.SetColor(EmissionColor, emissionColor);
            _glassMR.SetPropertyBlock(_materialPropertyBlock);
        }

        private void DisableSpotlight()
        {
            _spotLight.gameObject.SetActive(false);
        }

        public void TurnOn(Flashlight flashLight)
        {
            UpdateLight(flashLight);
        }

        public void TurnOff()
        {
            DisableSpotlight();
        }
    }
}