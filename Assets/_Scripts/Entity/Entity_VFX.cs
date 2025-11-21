using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage Data")]
    [SerializeField] private Material flashMat;
    [SerializeField] private float flashTime;
    private Material defaultMat;
    private Coroutine onTakeDamageCoroutine;


    [Header("On Doing Normal Hit Data")]
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private GameObject critHitVFX;
    [SerializeField] private Color hitVFXColor;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        entity = GetComponentInParent<Entity>();

        defaultMat = sr.material;
    }

    public void CreateOnHitVFX(Transform targetTransform, bool isCritPerformed)
    {
        GameObject hitPrefab = isCritPerformed ? critHitVFX : hitVFX;

        GameObject newHitVFX = Instantiate(hitPrefab, targetTransform.position, Quaternion.identity);
        newHitVFX.GetComponentInChildren<SpriteRenderer>().color = hitVFXColor;

        if(entity.FacingDirection == -1 && isCritPerformed)
        {
            newHitVFX.transform.Rotate(0, 180, 0);
        }
    }


    public void PlayOnTakeDamageTakenVFX()
    {
        if(onTakeDamageCoroutine != null)
        {
            StopCoroutine(onTakeDamageCoroutine);
        }

        onTakeDamageCoroutine = StartCoroutine(FlashMaterialCoroutine());
    }

    private IEnumerator FlashMaterialCoroutine()
    {
        sr.material = flashMat;
        yield return new WaitForSeconds(flashTime);
        sr.material = defaultMat;
    }

}
