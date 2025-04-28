using System.Collections;
using UnityEngine;

public class FlashController : MonoBehaviour
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

        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        intensity = 0.1f;
        material.SetColor(emissionColorId, emissionColor * intensity);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));

        intensity = 2.5f;
        material.SetColor(emissionColorId, emissionColor * intensity);

        yield return new WaitForSeconds(Random.Range(0.5f, 0.75f));

        StartCoroutine(Flash());
    }
}
