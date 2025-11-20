using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Material defaultMat;
    private Coroutine onDamageCoroutine;

    [Header("On Taking Damage Data")]
    [SerializeField] private Material flashMat;
    [SerializeField] private float flashTime;


    [Header("On Doing Damage Data")]
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private Color hitVFXColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
    }

    public void CreateOnHitVFX(Transform targetTransform)
    {
        GameObject newHitVFX = Instantiate(hitVFX, targetTransform.position, Quaternion.identity);
        newHitVFX.GetComponentInChildren<SpriteRenderer>().color = hitVFXColor;
    }

    public void PlayOnDamageTakenVFX()
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
