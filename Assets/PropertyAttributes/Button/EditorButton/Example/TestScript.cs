using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour 
{
	public Color TestColor;
	public Vector4 TestPosition;

    [Button]
    public void EmptyMethod(GameObject go)
    {
    }

	[Button]
	public void SetScaleX(float scale = 5.0f) 
	{
		transform.localScale = new Vector3 (scale, transform.localScale.y, transform.localScale.z);
	}

	[Button]
	public void PrintStuff(float floatVal, int intVal, string stringVal, bool boolVal) 
	{
		Debug.Log("floatVal "+floatVal);
		Debug.Log("intVal "+intVal);
		Debug.Log("stringVal "+stringVal);
		Debug.Log("boolVal "+boolVal);
	}

	//Run in edit mode
	[Button]
	public IEnumerator CountTo(int max=6) 
	{
		int current = 0;
		while (current < max) 
		{
			Debug.Log (current++);
			yield return new WaitForSeconds (1.0f);
		}
	}
}