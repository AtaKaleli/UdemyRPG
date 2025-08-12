using UnityEngine;

public class FlipController : MonoBehaviour
{

    private bool isFacingRight = true;

    public void HandleFlip(float xVelocity)
    {
        if (xVelocity < 0 && isFacingRight)
            Flip();
        else if (xVelocity > 0 && !isFacingRight)
            Flip();

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.parent.Rotate(0f, 180f, 0f);
    }
}
