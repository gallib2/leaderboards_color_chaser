using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCalll : MonoBehaviour
{
    public void OnClick_DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }
}
