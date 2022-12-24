using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMacros : MonoBehaviour
{
	#region self
	#region Postion:
	public void lerpSomethingPositionSelf(Vector3 min, Vector3 max, float time, bool flipFLop, float time2)
	{
		StartCoroutine(lerpSomethingIEPositionSelf(min, max, time, flipFLop, time2));
	}
	private IEnumerator lerpSomethingIEPositionSelf(Vector3 min, Vector3 max, float time, bool flipFLop, float time2)
	{
		float localTTime = 0;
		while (localTTime < 1)
		{
			this.transform.position = Vector3.Lerp(min, max, localTTime);
			localTTime += Time.deltaTime / time;
			yield return null;
		}
		if(flipFLop)
		{
			localTTime = 0;
			while (localTTime < 1)
			{
				this.transform.position = Vector3.Lerp(max, min, localTTime);
				localTTime += Time.deltaTime / time2;
				yield return null;
			}
		}
	}
	//Destroy Obj:
	public void lerpSomethingPositionSelf(Vector3 min, Vector3 max, float time, GameObject obj)
	{
		StartCoroutine(lerpSomethingIEPositionSelf(min, max, time, obj));
	}
	private IEnumerator lerpSomethingIEPositionSelf(Vector3 min, Vector3 max, float time, GameObject obj)
	{
		float localTTime = 0;
		while (localTTime < 1)
		{
			this.transform.position = Vector3.Lerp(min, max, localTTime);
			localTTime += Time.deltaTime / time;
			yield return null;
		}
		Destroy(obj);
	}
	#endregion
	#region localScale
	public void lerpSomethingScaleSelf(Vector3 min, Vector3 max, float time, GameObject obj)
	{
		StartCoroutine(lerpSomethingIEScaleSelf(min, max, time, obj));
	}
	private IEnumerator lerpSomethingIEScaleSelf(Vector3 min, Vector3 max, float time, GameObject obj)
	{
		float localTTime = 0;
		while (localTTime < 1)
		{
			this.transform.localScale = Vector3.Lerp(min, max, localTTime);
			localTTime += Time.deltaTime / time;
			yield return null;
		}
		Destroy(obj);
	}
	//Destroy Obj:
	public void lerpSomethingScale(Vector3 min, Vector3 max, float time)
	{
		StartCoroutine(lerpSomethingIEScale(min, max, time));
	}
	private IEnumerator lerpSomethingIEScale(Vector3 min, Vector3 max, float time)
	{
		float localTTime = 0;
		while (localTTime < 1)
		{
			this.transform.localScale = Vector3.Lerp(min, max, localTTime);
			localTTime += Time.deltaTime / time;
			yield return null;
		}
	}
	public void lerpSomethingColor(Color32 min, Color32 max, float time, GameObject obj, SpriteRenderer spriteRenderer)
	{
		StartCoroutine(lerpSomethingIEColorSelf(min, max, time, obj, spriteRenderer));
	}
	private IEnumerator lerpSomethingIEColorSelf(Color32 min, Color32 max, float time, GameObject obj, SpriteRenderer spriteRenderer)
	{
		float localTTime = 0;
		while (localTTime < 1)
		{
			spriteRenderer.color = Color32.Lerp(min, max, localTTime);
			localTTime += Time.deltaTime / time;
			yield return null;
		}
		Destroy(obj);
	}
	#endregion
	#endregion
	#region Parents
	#region Postion:
	public void lerpSomethingPositionParent(Vector3 min, Vector3 max, float time)
		{
			StartCoroutine(lerpSomethingIEPositionParent(min, max, time));
		}
		private IEnumerator lerpSomethingIEPositionParent(Vector3 min, Vector3 max, float time)
		{
			float localTTime = 0;
			while (localTTime < 1)
			{
				this.transform.parent.position = Vector3.Lerp(min, max, localTTime);
				localTTime += Time.deltaTime / time;
				yield return null;
			}
		}
	//Destroy Obj:
		public void lerpSomethingPositionParent(Vector3 min, Vector3 max, float time, GameObject obj)
		{
			StartCoroutine(lerpSomethingIEPositionParent(min, max, time, obj));
		}
		private IEnumerator lerpSomethingIEPositionParent(Vector3 min, Vector3 max, float time, GameObject obj)
		{
			float localTTime = 0;
			while (localTTime < 1)
			{
				this.transform.parent.position = Vector3.Lerp(min, max, localTTime);
				localTTime += Time.deltaTime / time;
				yield return null;
			}
			Destroy(obj);
		}
	#endregion
	#region localScale
	public void lerpSomethingScaleParent(Vector3 min, Vector3 max, float time, GameObject obj)
		{
			StartCoroutine(lerpSomethingIEScaleParent(min, max, time, obj));
		}
		private IEnumerator lerpSomethingIEScaleParent(Vector3 min, Vector3 max, float time, GameObject obj)
		{
			float localTTime = 0;
			while (localTTime < 1)
			{
				this.transform.parent.localScale = Vector3.Lerp(min, max, localTTime);
				localTTime += Time.deltaTime / time;
				yield return null;
			}
			Destroy(obj);
		}
	//Destroy Obj:
		public void lerpSomethingScaleParent(Vector3 min, Vector3 max, float time)
		{
			StartCoroutine(lerpSomethingIEScaleParent(min, max, time));
		}
		private IEnumerator lerpSomethingIEScaleParent(Vector3 min, Vector3 max, float time)
		{
			float localTTime = 0;
			while (localTTime < 1)
			{
				this.transform.parent.localScale = Vector3.Lerp(min, max, localTTime);
				localTTime += Time.deltaTime / time;
				yield return null;
			}
		}
	#endregion
	#endregion
}