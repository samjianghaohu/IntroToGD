using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Utilities : MonoBehaviour {
//	public Color color0;
//	public Color match0;
//
//	public Color color1;
//	public Color match1;
//
//	public Color color2;
//	public Color match2;
//
//	public Color color3;
//	public Color match3;

	//static Dictionary<Color, Color> colorMatch;
	// Use this for initialization
	void Start () {
//		colorMatch = new Dictionary<Color, Color>();
//		InitializeDict ();
	}
	
	// Update is called once per frame
	void Update () {
	}

//	void InitializeDict() {
//		colorMatch.Add (color0, match0);
//		colorMatch.Add (color1, match1);
//		colorMatch.Add (color2, match2);
//		colorMatch.Add (color3, match3);
//	}


	public static bool compareColorsLoose(Color color1, Color color2) {
		if (Mathf.Abs (color1.r - color2.r) < 0.01f && Mathf.Abs (color1.g - color2.g) < 0.01f && Mathf.Abs (color1.b - color2.b) < 0.01f) {
			return true;
		}
		return false;
	}

//	public static Dictionary<Color, Color> GetColorDict(){
//		return colorMatch;
//	}
}
