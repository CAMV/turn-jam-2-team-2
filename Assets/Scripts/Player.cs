using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Light _pointLight;
    public Light Light { get => _pointLight; }
    
}
