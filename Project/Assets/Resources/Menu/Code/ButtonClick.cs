using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {
	
	public bool isWIP;
    public bool isQuit;
	void OnMouseEnter()
	{
		GetComponent<ButtonSelecteCheck> ().isSelected = true;
	}

	void OnMouseExit()
	{
		GetComponent<ButtonSelecteCheck> ().isSelected = false;
	}

	void OnMouseDown()
	{
		if (isWIP)
        {
            GameObject.Find("Audio_Invalid").GetComponent<AudioSource>().Play();
        } else
        {
            if (isQuit)
            {
                GameObject.Find("Audio_Accept").GetComponent<AudioSource>().Play();
                Application.Quit();
            } else 
            {
                GameObject.Find("Audio_Accept").GetComponent<AudioSource>().Play();
                Application.LoadLevel(2);
            }
        }
	}
}
