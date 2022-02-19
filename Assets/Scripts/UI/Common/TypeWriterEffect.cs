using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Common
{
	public class TypeWriterEffect : MonoBehaviour
	{
		private string fullText;
		private string curText = "";
		private static float TIME_DELAY = 0.1f;
		private bool isStart = false;

		private void Update()
		{
			if (isStart && Input.GetMouseButtonDown(0))
			{
				StopAllCoroutines();
				GetComponent<Text>().text = fullText;
				SendMessageUpwards("TypeFinish", SendMessageOptions.DontRequireReceiver);
				isStart = false;
			}
		}

		public void StartType(string text)
		{
			fullText = text;
			isStart = true;
			StartCoroutine(TypeText());
		}

		public void ClearContent()
		{
			fullText = "";
			curText = "";
			GetComponent<Text>().text = "";
		}

		IEnumerator TypeText()
		{
			for (int i = 0; i <= fullText.Length; i++)
			{
				curText = fullText.Substring(0, i);
				GetComponent<Text>().text = curText;
				yield return new WaitForSeconds(TIME_DELAY);
				if (i != fullText.Length) continue;
				yield return new WaitForSeconds(TIME_DELAY);
				SendMessageUpwards("TypeFinish", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}