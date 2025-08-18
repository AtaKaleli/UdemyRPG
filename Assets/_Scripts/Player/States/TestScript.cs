using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float lastAttackTime;
    public float gameTime;
    public float comboResetTime = 5f;

    private void Update()
    {
        gameTime = Time.time;

        if (gameTime > lastAttackTime + comboResetTime)
        {
            Debug.Log("combo resetted");
        }
    }

    [ContextMenu("Set")]
    public void SetLastAttackTime()
    {
        lastAttackTime = Time.time;


        
    }

}
