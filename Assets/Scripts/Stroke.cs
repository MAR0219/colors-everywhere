using UnityEngine;

public class Stroke : MonoBehaviour {
  private StrokeData strokeData;

	void Start () {}

  public void Initialize(string color, int brushType, float brushWidth) {
    strokeData = new StrokeData(color, brushType, brushWidth);
  }

  public void Initialize(StrokeData strokeData) {
    this.strokeData = strokeData;

    foreach(Vector3 point in strokeData.points) {
      StrokeRenderer().AddPoint(point);
    }
  }

  private StrokeRenderer StrokeRenderer() {
    return GetComponent<StrokeRenderer>();
  }

	public void AddPoint(Vector3 point) {
    StrokeRenderer().AddPoint(point);
    strokeData.AddPoint(point);
	}

  // Remove following testing
  public StrokeData StrokeData() {
    return strokeData;
  }
}