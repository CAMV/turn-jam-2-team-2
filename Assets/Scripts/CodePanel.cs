using System;
using UnityEngine;
using UnityEngine.Events;

namespace TurnJam2
{
    public class CodePanel : MonoBehaviour, IInteractable
    {
        [SerializeField] private int[] _digits;
        [SerializeField] private UnityEvent _onSolved;

        private int[] _answer;
        private CodeDigit[] _codeDigits;

        private CodeDigit _closestCodeDigit;
        
        private void Awake()
        {
            _answer = new int[_digits.Length];
            _codeDigits = GetComponentsInChildren<CodeDigit>();
        }

        public void SetDigit(int index, int value)
        {
            _answer[index] = value;

            if (IsSolutionCorrect())
            {
                _onSolved?.Invoke();
            }
        }

        private bool IsSolutionCorrect()
        {
            for (var i = 0; i < _digits.Length; i++)
            {
                if (_digits[i] != _answer[i]) return false;
            }

            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            AttemptToAttachCodeDigit(other);
        }

        private void OnTriggerStay(Collider other)
        {
            AttemptToAttachCodeDigit(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Player>(out var player)) return;
            
            player.RemoveInteractable(this);
        }

        private void AttemptToAttachCodeDigit(Collider other)
        {
            if (!other.TryGetComponent<Player>(out var player)) return;

            var minDistance = float.MaxValue;
            _closestCodeDigit = null;

            foreach (var codeDigit in _codeDigits)
            {
                var distance = Vector3.Distance(codeDigit.transform.position, player.transform.position);
                if (distance > minDistance) continue;
                
                minDistance = distance;
                _closestCodeDigit = codeDigit;
            }
            
            player.SetInteractable(this);
        }

        public void Interact()
        {
            _closestCodeDigit.ChangeDigit();
        }
    }
}
