using System.Collections;
using UnityEngine;

// this class helps the audiomanager to let sound fade in and out or just change the volume in a fading manner
public static class AudioHelper
{

	public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
			yield return null;
		}
		audioSource.Stop();
	}

	public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float targetVolume)
	{
		audioSource.Play();
		audioSource.volume = 0f;
		while (audioSource.volume < targetVolume)
		{
			audioSource.volume += Time.deltaTime / FadeTime;
			yield return null;
		}
	}

	public static IEnumerator FadeInVolume(AudioSource audioSource, float FadeTime, float targetVolume)
	{
		while (audioSource.volume < targetVolume)
		{
			audioSource.volume += Time.deltaTime / FadeTime;
			yield return null;
		}
	}

	public static IEnumerator FadeOutVolume(AudioSource audioSource, float FadeTime, float targetVolume)
	{
		while (audioSource.volume > targetVolume)
		{
			audioSource.volume -= Time.deltaTime / FadeTime;
			yield return null;
		}
	}
}