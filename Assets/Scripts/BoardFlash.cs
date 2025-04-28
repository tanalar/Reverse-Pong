using UnityEngine;

public class BoardFlash : MonoBehaviour
{
    private Renderer renderer;
    private Material material;
    private int emissionColorId;
    private Color emissionColor;
    private float intensity;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        emissionColorId = Shader.PropertyToID("_EmissionColor");
        emissionColor = material.GetColor(emissionColorId);
    }
    private void OnEnable()
    {
        Ball.onWallTouch += StartFlash;
    }
    private void OnDisable()
    {
        Ball.onWallTouch -= StartFlash;
    }

    private void Update()
    {
        if (intensity > 1)
        {
            intensity -= 5000f * Time.deltaTime;
            if (intensity < 1)
            {
                intensity = 1;
            }
            material.SetColor(emissionColorId, emissionColor * intensity);
        }
    }

    private void StartFlash()
    {
        intensity = 500f;
        material.SetColor(emissionColorId, emissionColor * intensity);
    }
}
