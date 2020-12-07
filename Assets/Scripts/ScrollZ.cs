using UnityEngine;
using System.Collections;

public class ScrollZ : MonoBehaviour
{

	public float scrollSpeed = 40;
	public float showTime = 30;
	public float currentTime = 0;
	public bool activarScroll = false;

	public Menu m;

    private void OnEnable()
    {
		Debug.Log("enabling...");
		transform.localPosition = new Vector3(0, -370, 69);
		currentTime = 0;
	}

    void Update()
	{
		if(activarScroll == true)
        {
			Vector3 pos = transform.position;

			Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);

			pos += localVectorUp * scrollSpeed * Time.deltaTime;
			transform.position = pos;

		}
		currentTime += Time.deltaTime;
		

		if (currentTime >= showTime)
        {
			m.show_start();
        }

	}

}