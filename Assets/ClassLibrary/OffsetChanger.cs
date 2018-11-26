using System;
using UnityEngine;
using Random = System.Random;

namespace ClassLibrary
{
	public class OffsetChanger : MonoBehaviour
	{
		private Renderer renderer;
		private float scrollSpeed;
		private float offset;
		private Random random = new Random(DateTime.Now.Millisecond);
        
		void Start()
		{
			renderer = GetComponent<Renderer>();
		}

		void Update()
		{
			scrollSpeed = Convert.ToSingle(random.NextDouble());
			offset += Time.deltaTime * scrollSpeed;
			if (offset>1)
			{
				offset--;
			}

			renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
		}
	}
}