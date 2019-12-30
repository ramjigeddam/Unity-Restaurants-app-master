using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemHandler : MonoBehaviour
{
    [SerializeField]
    private Transform selecteItem, removeItem;
    public GameObject selectBtn, removeBtn;

    private void Start()
    {
        selectBtn.SetActive(true);
        removeBtn.SetActive(false);
    }

    public void SelectedItem()
    {
        this.transform.parent = selecteItem;
        selectBtn.SetActive(false);
        removeBtn.SetActive(true);
        GameObject.FindObjectOfType<TrackingHandler>().selectedObject.Add(this.gameObject);

    }

    public void RemoveItem()
    {
        this.transform.parent = removeItem;
        GameObject.FindObjectOfType<TrackingHandler>().selectedObject.Remove(this.gameObject);
        selectBtn.SetActive(true);
        removeBtn.SetActive(false);
    }
}
