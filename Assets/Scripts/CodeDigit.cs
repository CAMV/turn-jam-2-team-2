using TMPro;
using UnityEngine;

namespace TurnJam2
{
    public class CodeDigit : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private int _maxIndex;

        
        private CodePanel _codePanel;
        private int _currentIndex;

        private void Awake()
        {
            _codePanel = GetComponentInParent<CodePanel>();
        }

        public void ChangeDigit()
        {
            _currentIndex++;
            if (_currentIndex == _maxIndex)
            {
                _currentIndex = 0;
            }

            _text.text = _currentIndex.ToString();
            
            _codePanel.SetDigit(transform.GetSiblingIndex(), _currentIndex);
        }
    }
}

