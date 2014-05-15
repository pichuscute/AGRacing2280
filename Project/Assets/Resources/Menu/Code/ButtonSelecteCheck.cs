using UnityEngine;
using System.Collections;

public class ButtonSelecteCheck : MonoBehaviour {

	// Use this for initialization
	public Vector3 thisScale;
    public Vector3 selectScale;
    Vector3 currentScale;
	public bool isSelected;
	void Start () 
	{
		thisScale = transform.localScale;
        selectScale = new Vector3(transform.localScale.x + 0.05f, transform.localScale.y + 0.03f, transform.localScale.z + 0.03f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isSelected)
        {
            currentScale = Vector3.Lerp(currentScale, selectScale, Time.fixedDeltaTime * 4);
        } else
        {
            currentScale = Vector3.Lerp(currentScale, thisScale, Time.fixedDeltaTime * 4);
        }
        transform.localScale = currentScale;
	}
}
