using UnityEngine;

public class JumpIncreasePlatform : BasePlatform
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            if (!player.EnteredGreenPlatform)
            {
                Debug.Log("Increase jump amount");
                player.JumpForce *= _greenPlatformMultiplier;
                player.EnteredGreenPlatform = true;
                player.DisplayJumpForceAmount(_greenPlatformMultiplier, true);
            }

            if(player.EnteredRedPlatform)
            {
                player.EnteredRedPlatform = false;
            }
        }
    }

}
