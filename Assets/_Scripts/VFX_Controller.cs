using UnityEngine;

public class VFX_Controller : MonoBehaviour
{
    [Header("Control Data")]
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1f;
    [Space]
    [SerializeField] private bool randomPosition = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Position Offset Data")]
    [SerializeField] private float xMinPosOffset;
    [SerializeField] private float xMaxPosOffset;
    [Space]
    [SerializeField] private float yMinPosOffset;
    [SerializeField] private float yMaxPosOffset;

    [Header("Rotation Offset Data")]
    [SerializeField] private float minRotation = 0;
    [SerializeField] private float maxRotation = 360;


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

        float xOffset = Random.Range(xMinPosOffset, xMaxPosOffset);
        float yOffset = Random.Range(yMinPosOffset, yMaxPosOffset);

        transform.position += new Vector3(xOffset, yOffset);
    }

    private void ApplyRandomRotation()
    {
        if (!randomRotation) return;

        float zRotation = Random.Range(minRotation, maxRotation);
        transform.Rotate(0, 0, zRotation);
    }
}
