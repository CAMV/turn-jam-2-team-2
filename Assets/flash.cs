using UnityEngine;

public class flash : MonoBehaviour
{
    [SerializeField]
    private Light _light;

    [SerializeField]
    private MeshRenderer[] blocks;

    private MaterialPropertyBlock _block;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _block = new MaterialPropertyBlock();
        _block.SetVector("_light_pos", _light.transform.position);
        _block.SetFloat("_light_intensity", _light.intensity);
        _block.SetFloat("_light_range", _light.range);

    }

    // Update is called once per frame
    void Update()
    {
        _block.SetVector("_light_pos", _light.transform.position);
        _block.SetFloat("_light_intensity", _light.intensity);
        _block.SetFloat("_light_range", _light.range);

        foreach (var b in blocks)
            b.SetPropertyBlock(_block);
    }
}
