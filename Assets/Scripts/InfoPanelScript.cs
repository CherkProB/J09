using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelScript : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;

    public void SetInfo(string title, string info) 
    {
        titleText.text = title;
        infoText.text = info;
    }

    public void Enable(bool flag, Vector3 pos) 
    {
        gameObject.SetActive(flag);
        gameObject.transform.position = pos += Vector3.down * gameObject.GetComponent<RectTransform>().sizeDelta.y / 3;
    }
}
