using System;
using TurnJam2.Interfaces;
using UnityEngine;

namespace TurnJam2
{
    public class Flash : MonoBehaviour, ILightInteractable
    {
        [SerializeField] private Light _light;
        [SerializeField] private MeshRenderer[] blocks;

        private MaterialPropertyBlock _block;
        private Animator _animator;
        private bool _isActive;

        private static readonly int LightPos = Shader.PropertyToID("_light_pos");
        private static readonly int LightIntensity = Shader.PropertyToID("_light_intensity");
        private static readonly int LightRange = Shader.PropertyToID("_light_range");
        private static readonly int FlashAnimParam = Animator.StringToHash("Flash");

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            _block = new MaterialPropertyBlock();
            Reset();

            _animator = GetComponentInChildren<Animator>();
        }

        private void Reset()
        {
            _light.intensity = 0;
            _light.range = 0;
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            _block.SetVector(LightPos, _light.transform.position);
            _block.SetFloat(LightIntensity, _light.intensity);
            _block.SetFloat(LightRange, _light.range);
            
            foreach (var b in blocks)
                b.SetPropertyBlock(_block);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_isActive) return;
            
            UpdateProperties();
        }

        private void UpdateAnimation()
        {
            _animator.SetBool(FlashAnimParam, _isActive);
        }

        public void TurnOn(Flashlight flashLight)
        {
            _isActive = true;
            UpdateAnimation();
        }

        public void TurnOff()
        {
            _isActive = false;
            UpdateAnimation();
            Reset();
        }
    }
}