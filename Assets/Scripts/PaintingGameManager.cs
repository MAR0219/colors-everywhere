﻿using UnityEngine;

public class PaintingGameManager : MonoBehaviour {
	public static PaintingGameManager instance = null;

	private GameObject paintingPrefab;

	void Awake() {
		SetupSingletion();

		paintingPrefab = Util.LoadPrefab("Painting");
	}

	public Painting Painting() {
		GameObject paintingObj = GameObject.FindWithTag("Painting");
		if(!paintingObj) {
			return null;
		} 
		
		return paintingObj.GetComponent<Painting>();
	}

	public void CreatePainting(float latitude, float longitude, int directionDegrees) {
		Painting painting = InstantiatePainting();

		painting.Initialize(latitude, longitude, directionDegrees);
	}

	public void LoadPainting(int paintingId) {
		Painting painting = InstantiatePainting();
		painting.Load(paintingId);
	}

	private Painting InstantiatePainting() {
		if(Painting()) {
			Destroy(Painting().gameObject);
		}

		Painting painting = Instantiate(paintingPrefab).GetComponent<Painting>();
		DontDestroyOnLoad(painting);

		return painting;
	}

	private void SetupSingletion() {
		//Check if instance already exists
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			//  This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		}

		DontDestroyOnLoad(gameObject);
	}
}
