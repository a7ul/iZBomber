using UnityEngine;
 
public class Floater : MonoBehaviour {
    public float amplitude;
    public float frequency;
 
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    void Start () {
        posOffset = transform.position;
    }
     
    void Update () {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * frequency) * amplitude;
 
        transform.position = tempPos;
    }
}