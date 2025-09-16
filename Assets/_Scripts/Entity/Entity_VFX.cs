using System.Collections;
using UnityEditor.Splines;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;


    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVFXDuration = 0.1f;

    private Material defaultMaterial;
    private Coroutine onDamageVFXCoroutine;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = sr.material;
    }

    public void OnDamageFlashMaterial()
    {
        if(onDamageVFXCoroutine != null)
        {
            StopCoroutine(onDamageVFXCoroutine);
        }

        onDamageVFXCoroutine = StartCoroutine(ChangeMaterialCoroutine());
    }

    private IEnumerator ChangeMaterialCoroutine()
    {
        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVFXDuration);
        sr.material = defaultMaterial;
    }

}
