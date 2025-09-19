using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material defaultMat;
    private Coroutine onDamageCoroutine;

    [Header("Took Damage Data")]
    [SerializeField] private Material flashMat;
    [SerializeField] private float flashTime;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
    }

    public void PlayOnDamageVFX()
    {
        if(onDamageCoroutine != null)
        {
            StopCoroutine(onDamageCoroutine);
        }

        onDamageCoroutine = StartCoroutine(FlashMaterialCoroutine());
    }

    private IEnumerator FlashMaterialCoroutine()
    {
        sr.material = flashMat;
        yield return new WaitForSeconds(flashTime);
        sr.material = defaultMat;
    }

}
