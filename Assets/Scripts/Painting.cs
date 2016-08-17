﻿using UnityEngine;
using System.Collections.Generic;

public class Painting : MonoBehaviour {
	private static string STROKE_PREFAB = "Stroke";
	
	public bool Dirty { get; set; }

	private List<Stroke> strokes;

	public PaintingData paintingData;

	private GameObject strokePrefab;

	void Start () {
		strokes = new List<Stroke>();

		// lat, long, direction hard-coded
		paintingData = new PaintingData(latitude: 2.1f, longitude: 2.1f, directionDegrees: 10);

		strokePrefab = Util.LoadPrefab(STROKE_PREFAB);
	}
	
	public void StartStroke(string color, int brushType, float brushWidth) {
		Stroke stroke = Instantiate(strokePrefab).GetComponent<Stroke>();
		stroke.transform.parent = transform;

		stroke.Initialize(color, brushType, brushWidth);
		strokes.Add(stroke);

		paintingData.StartStroke(stroke);

		Dirty = true;
	}

	public Stroke CurrentStroke() {
		return strokes[strokes.Count - 1];
	}

	public void Load(string jsonData) {
		strokes.Clear();
		paintingData.Load(jsonData);

		foreach(StrokeData data in paintingData.strokeDatas) {
			Stroke stroke = Instantiate(strokePrefab).GetComponent<Stroke>();
			stroke.transform.parent = transform;
			
			stroke.Initialize(data);
			strokes.Add(stroke);
		}
	}

	public void AddPoint(Vector3 point) {
		CurrentStroke().AddPoint(point);
		Dirty = true;
	}

	public PaintingData PaintingData() {
		return paintingData;
	}

	public string ToJsonStr() {
		return paintingData.ToJsonStr();
	}

	public bool IsNew() {
		return paintingData.IsNew();
	}

	public int Id() {
		return paintingData.Id();
	}
}
