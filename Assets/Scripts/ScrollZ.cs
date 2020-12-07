using UnityEngine;
using System.Collections;

public class ScrollZ : MonoBehaviour
{

	public float scrollSpeed = 40;

	public bool activarScroll = false;

	public Menu m;

	void Update()
	{
		if(activarScroll == true)
        {
			Vector3 pos = transform.position;

			Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);

			pos += localVectorUp * scrollSpeed * Time.deltaTime;
			transform.position = pos;

		}

		float top = this.gameObject.GetComponent<RectTransform>().offsetMax.y;
		top = top * (-1);

		if (top < -953.3248)
        {
			m.show_start();
        }

	}

}