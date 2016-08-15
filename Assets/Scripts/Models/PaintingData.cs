using UnityEngine;
using System.Collections.Generic;

// API for communication with Server

[System.Serializable]
public class PaintingData {
  [SerializeField] private int id = -1;
  [SerializeField] private float latitude;
  [SerializeField] private float longitude;
  [SerializeField] private float direction_degrees;

  [SerializeField] public List<StrokeData> strokeDatas;
  
  public PaintingData(float latitude, float longitude, float directionDegrees) {
    this.latitude = latitude;
    this.longitude = longitude;
    this.direction_degrees = directionDegrees;

    strokeDatas = new List<StrokeData>();
  }

  public void StartStroke(Stroke stroke) {
    strokeDatas.Add(stroke.StrokeData());
  }

  public void AddPoint(Vector3 point) {
    CurrentStrokeData().AddPoint(point);
  }

  public void Load(string jsonData) {
    strokeDatas = null;
    JsonUtility.FromJsonOverwrite(jsonData, this);
		Debug.Log(strokeDatas[0].color);
  }

  public string ToJsonStr() {
    return JsonUtility.ToJson(this);
  }

  private StrokeData CurrentStrokeData() {
    return strokeDatas[strokeDatas.Count - 1];
  }

  public bool IsNew() {
    return id >= 0;
  }

  public int Id() {
    return id;
  }
}
