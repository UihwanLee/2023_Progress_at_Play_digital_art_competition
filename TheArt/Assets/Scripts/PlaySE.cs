using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySE : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private string SE;

    // SE »ç¿îµå
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySE(SE);
    }
}
