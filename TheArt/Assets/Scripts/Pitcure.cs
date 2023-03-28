using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Picture", menuName = "New Picture/picture")]
public class Pitcure : ScriptableObject
{
    [SerializeField]
    private string pictureTitle;

    // 1) �ر׸�
    // 2) ���� ģ �׸�(���յ���)

    [SerializeField]
    private Sprite pirctureDefault;
    [SerializeField]
    private Sprite pirctureColor;

    public string GetPictureTitle() { return pictureTitle; }
    public Sprite GetPictureDraw() { return pirctureDefault; }
    public Sprite GetPictureColor() { return pirctureColor; }
}
