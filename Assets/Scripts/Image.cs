using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator DownloadImage(string url){
		WWW www = new WWW (url);
		yield return www;
		GetComponent<MeshRenderer> ().material.mainTexture = www.texture;
	}

	public void LlamarCorrutina(string url){
		StartCoroutine (DownloadImage (url));
	}

}
