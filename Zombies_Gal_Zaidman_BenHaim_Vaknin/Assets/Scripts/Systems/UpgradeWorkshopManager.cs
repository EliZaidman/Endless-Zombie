using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeWorkshopManager : MonoBehaviour
{
    [SerializeField]
    //private GameObject _inventory;

    //[SerializeField]
    //private List<Image> _allItemSprites;

    //[SerializeField]
    private List<RectTransform> _allItemTr;

    Ray ray;
    RaycastHit hit;

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        foreach (RectTransform item in _allItemTr)
        {
            if (Physics.Raycast(ray, out hit) && hit.transform == item)
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
