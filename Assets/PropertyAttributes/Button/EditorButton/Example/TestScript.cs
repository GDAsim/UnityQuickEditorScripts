using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public Color TestColor;

	public Vector4 TestPosition;

	[EditorButton]
	public void SetScale(float scale) {
		transform.localScale = new Vector3 (scale, scale, scale);
	}

	[EditorButton]
	public void SetScaleX(float scale = 5.0f) {
		transform.localScale = new Vector3 (scale, transform.localScale.y, transform.localScale.z);
	}

	[EditorButton]
	public void FloatIntDefault(float floatAsInt = 5) {
		Debug.Log ("floatAsInt " + floatAsInt);
	}

	[EditorButton]
	public void EmptyMethod() {
	}

	[EditorButton]
	public void PrintStuff(float floatVal, int intVal, string stringVal, bool boolVal) {
		Debug.Log("floatVal "+floatVal);
		Debug.Log("intVal "+intVal);
		Debug.Log("stringVal "+stringVal);
		Debug.Log("boolVal "+boolVal);
	}

	[EditorButton]
	public void SetMaterialColor(Color color) {
		GetComponent<MeshRenderer> ().sharedMaterial.color = color;
	}

	[EditorButton]
	public IEnumerator CountTo(int max=6) {
		int current = 0;
		while (current < max) {
			Debug.Log (current++);
			yield return new WaitForSeconds (1.0f);
		}
	}

	[EditorButton]
	public void PrintNameOf(GameObject go) {
		Debug.Log (go.name);
	}

	
}