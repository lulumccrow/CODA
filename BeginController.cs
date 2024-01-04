using UnityEngine;
using System.Collections;

public class BeginController : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(StartEverything());
	}

	private IEnumerator StartEverything()
	{
		yield return new WaitForSeconds(0.2f);
		FindObjectOfType<AudioController>().BeginMusic();
		FindObjectOfType<Animator>().Play("Base Layer.logoanimation");
	}
}

