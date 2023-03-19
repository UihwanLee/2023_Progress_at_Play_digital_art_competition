using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D hook;
    [SerializeField]
    private GameObject[] prefabRopeSegs;
    [SerializeField]
    private int numLinks;

    [SerializeField]
    private HingeJoint2D top;

    [SerializeField]
    private PlayerController player;

    public void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for(int i=0; i<numLinks; ++i)
        {
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();

            if (i == 0) top = hj;
        }
    }

    private void addLink()
    {
        int index = Random.Range(0, prefabRopeSegs.Length);
        GameObject newLink = Instantiate(prefabRopeSegs[index]);
        newLink.transform.parent = transform;
        newLink.transform.position = transform.position;
        HingeJoint2D hj = newLink.GetComponent<HingeJoint2D>();
        hj.connectedBody = hook;
        newLink.GetComponent<RopeSegment>().connectedBelow = top.gameObject;
        top.connectedBody = newLink.GetComponent<Rigidbody2D>();
        top.GetComponent<RopeSegment>().ResetAnchor();
        top = hj;

    }

    private void removeLink()
    {
        if(top.gameObject.GetComponent<RopeSegment>().isPlayerAttached)
        {
            //Slide(-1);
        }
        HingeJoint2D newTop = top.gameObject.GetComponent<RopeSegment>().connectedBelow.GetComponent<HingeJoint2D>();
        newTop.connectedBody = hook;
        newTop.gameObject.transform.position = hook.gameObject.transform.position;
        newTop.GetComponent<RopeSegment>().ResetAnchor();
        Destroy(top.gameObject);
        top = newTop;
    }
}
