using UnityEngine;

public class JumpDecreasePlatform : BasePlatform
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (!player.EnteredRedPlatform)
            {
                Debug.Log("Reduce jump amount");
                player.JumpForce -= _redPlatformMultiplier;
                player.EnteredRedPlatform = true;
                player.DisplayJumpForceAmount(_redPlatformMultiplier, false);
            }

            if (player.EnteredGreenPlatform)
            {
                player.EnteredGreenPlatform = false;
            }
        }
    }
}
