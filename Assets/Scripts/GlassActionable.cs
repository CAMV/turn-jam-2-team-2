using TurnJam2.Interfaces;
using UnityEngine;

namespace TurnJam2
{
    public class GlassActionable : MonoBehaviour, ILightInteractable
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    
        [SerializeField] private Light _light;
        [SerializeField] private Color _color;
        [SerializeField] private float _rangeMultiplier;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private Flashlight _lightSource;
        private MaterialPropertyBlock _materialPropertyBlock;
        
        private void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            SetColor(_color, Color.black);
            
            _light.gameObject.SetActive(false);
        }
        
        private void SetColor(Color baseColor, Color emissionColor)
        {
            _materialPropertyBlock.SetColor(BaseColor, baseColor);
            _materialPropertyBlock.SetColor(EmissionColor, emissionColor);
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
        
        public void TurnOn(Flashlight flashLight)
        {
            _light.gameObject.SetActive(true);
            SetColor(Color.blue, _color * flashLight.Light.intensity);
        }

        public void TurnOff()
        {
            _light.gameObject.SetActive(false);
        }
    }
}