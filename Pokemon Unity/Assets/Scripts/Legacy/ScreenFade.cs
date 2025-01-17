﻿//Original Scripts by IIColour (IIColour_Spectrum)

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PokemonUnity.Legacy
{
	public class ScreenFade : MonoBehaviour
	{
		public static ScreenFade main;
		public static float defaultSpeed = 0.4f;
		public static float slowedSpeed = 1.2f;

		private Image image;

		public UnityEngine.Sprite[] fadeCutouts;

		public Material fadeMat;


		void Awake()
		{
			if (main == null)
			{
				main = this;
			}
			image = GetComponent<Image>();
		}


		public IEnumerator Fade(bool fadeIn, float speed)
		{
			yield return StartCoroutine(Fade(fadeIn, speed, Color.black));
		}

		public IEnumerator Fade(bool fadeIn, float speed, Color color)
		{
			image.sprite = null;
			image.material = null;

			float alpha = (fadeIn) ? 1f : 0f;
			image.color = new Color(color.r, color.g, color.b, alpha);

		
			LeanTween.alpha(image.GetComponent<RectTransform>(), 1-alpha, 0.5f);
		
			yield return new WaitForSeconds(0.5f);
		}

		public IEnumerator FadeCutout(bool fadeIn, float speed, UnityEngine.Sprite cutout)
		{
			if (cutout == null)
			{
				cutout = fadeCutouts[Random.Range(0, fadeCutouts.Length)];
			}

			image.sprite = cutout;
			image.material = fadeMat;

			float increment = 0f;
			while (increment < 1)
			{
				increment += (1 / speed) * Time.deltaTime;
				if (increment > 1)
				{
					increment = 1;
				}

				float alpha = (!fadeIn) ? 1f - increment : increment;

				image.material.SetFloat("_Cutoff", alpha);
				yield return null;
			}
		}

		public void SetToFadedOut()
		{
			image.sprite = null;
			image.material = null;
			image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
		}

		public void SetToFadedIn()
		{
			image.sprite = null;
			image.material = null;
			image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
		}
	}
}