using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlatformsColor : BasePlatform
{
    [SerializeField] Material _blackMat;

    private List<Material> storedMaterials = new List<Material>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if(player != null)
        {
            StartCoroutine(ChangePlatformColors());
        }
    }

    IEnumerator ChangePlatformColors()
    {
        GameObject[] platformsMesh = GameObject.FindGameObjectsWithTag("Platform");

        foreach(GameObject item in platformsMesh)
        {
            storedMaterials.Add(item.GetComponent<MeshRenderer>().material);
            item.GetComponent<MeshRenderer>().material = _blackMat;
        }

        yield return new WaitForSeconds(_blackPlatformChangeTime);
        for (int i = 0; i < storedMaterials.Count; i++)
        {
            platformsMesh[i].GetComponent<MeshRenderer>().material = storedMaterials[i];
        }
    }

}
