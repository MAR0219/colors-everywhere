﻿using System;
using UnityEngine;

using BestHTTP;

public class PaintingPersistence : MonoBehaviour {
	// private static string ROUTE = "http://localhost:3000/paintings";
	private static string ROUTE = "https://kenmgrimm-graffiti.herokuapp.com/paintings";

	private Painting painting;

	// Needs auth
	// https://docs.unity3d.com/ScriptReference/WWWForm-headers.html
	void Awake () {
		Debug.Log("Starting PM");

		painting = GameObject.Find("Painting").GetComponent<Painting>();

		LoadPaintingData(5);
	}

	private void OnLoadRequestFinished(HTTPRequest request, HTTPResponse response) {
		Debug.Log("Load Request Finished! Text received: " + response.DataAsText);

		painting.Load(response.DataAsText);
		Debug.Log(painting.paintingData);
		Debug.Log(painting.paintingData.Id());
	}

	private void LoadPaintingData(int paintingId) {
		HTTPRequest request = 
			new HTTPRequest(new Uri(ROUTE + "/" + paintingId + ".json"), OnLoadRequestFinished);
		request.Send();
	}

	private void OnUpdateRequestFinished(HTTPRequest request, HTTPResponse response) {
		Debug.Log("Update Request Finished! Text received: ");
		Debug.Log(response.DataAsText);
	}

	public void SavePainting() {
		Debug.Log("PaintingPersistence: SavePainting(): id: " + painting.Id());
		if(painting.Dirty) {
			painting.Dirty = false;
		}
		else {
			return;
		}

		Uri updateRoute = new Uri(IsNew() ? ROUTE : ROUTE + "/" + painting.Id());

		string paintingJsonStr = painting.ToJsonStr();

		byte[] compressed = Compress.Zip(paintingJsonStr);

		HTTPRequest request = new HTTPRequest(updateRoute, HTTPMethods.Post, OnUpdateRequestFinished);

		string compressedBase64 = System.Convert.ToBase64String(compressed);

		request.AddField("painting", compressedBase64);

		request.Send();
	}

	private bool IsNew() {
		return painting.Id() < 0;
	}
}
