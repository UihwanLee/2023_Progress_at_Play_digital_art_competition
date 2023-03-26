using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectPicture : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Pitcure pictuer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.pictuer != null)
        {
            this.gameObject.transform.GetChild(1).GetComponent<Text>().text = pictuer.GetPictureTitle() + "?";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.pictuer != null)
        {
            this.transform.GetChild(1).GetComponent<Text>().text = "";
        }
    }

    public void SetCurPicture(Pitcure _picture)
    {
        this.pictuer = _picture;
    }

    public Pitcure GetCurPicture() { return pictuer; }
}
