using UnityEngine;

public class VFX_Controller : MonoBehaviour
{
    [Header("Control Data")]
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1f;
    [Space]
    [SerializeField] private bool randomPosition = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Offset Data")]
    [SerializeField] private float xMinOffset;
    [SerializeField] private float xMaxOffset;
    [Space]
    [SerializeField] private float yMinOffset;
    [SerializeField] private float yMaxOffset;


    private void Start()
    {
        ApplyRandomPosition();
        ApplyRandomRotation();

        if (autoDestroy)
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private void ApplyRandomPosition()
    {
        if (!randomPosition) return;

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset, yMaxOffset);

        transform.position += new Vector3(xOffset, yOffset);
    }

    private void ApplyRandomRotation()
    {
        if (!randomRotation) return;

        float zRotation = Random.Range(0, 360);
        transform.Rotate(0, 0, zRotation);
    }
}
