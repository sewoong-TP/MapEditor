using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;

public class makePathJson : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTweenPath[] pathArray = gameObject.GetComponents<iTweenPath>();
		foreach(iTweenPath path in pathArray) {
			Debug.Log(path.pathName +" => "+ iTween.PathLength(path.nodes.ToArray()));
		}
	}

	void make(){
		string fileName;
		if (Application.platform == RuntimePlatform.WindowsEditor)
			fileName = Directory.GetCurrentDirectory () + "\\Assets\\Data\\paths.txt";
		else
			fileName = Directory.GetCurrentDirectory () + "/Assets/Data/paths.txt";
		
		System.IO.File.WriteAllText (fileName, "");				//초기화.

	
		using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true)) {
			file.Write("[\n");
			iTweenPath[] pathArray = gameObject.GetComponents<iTweenPath>();
			int i= 0;
			for (; i<pathArray.Length; i++){					//path 하나당 도는 포문.
				iTweenPath path = pathArray[i];
				Dictionary <string,object> D_path = new Dictionary <string,object>();
				List<Dictionary<string, string>> L_pathList = new List<Dictionary<string, string>>();


				if(path.path_type == 0)D_path.Add ("path_type",(object)"normal");					//path type.
				else if(path.path_type == 1)D_path.Add ("path_type",(object)"center");


				D_path.Add("path_group",(object)path.pathGroup.ToString());
				
				foreach (Vector3 position in path.nodes) {											//포지션 하나당 도는 포문.
					Dictionary<string, string> D_pathPosition = new Dictionary<string, string> ();
					D_pathPosition.Add ("x", position.x.ToString ("0.##"));
					D_pathPosition.Add ("y", position.y.ToString ("0.##"));
					L_pathList.Add (D_pathPosition);
				}
				D_path.Add ("path_list",L_pathList);
				Debug.Log(MiniJSON.Json.Serialize (D_path));
				if(i+1 != pathArray.Length) {
					file.WriteLine("\t" + MiniJSON.Json.Serialize (D_path) + ",");
				} else {
					file.WriteLine("\t" + MiniJSON.Json.Serialize (D_path));
				}
			}
			file.Write("]");
		}

		Debug.Log("Complete making path file");
	}

	
	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 80, 50),"Path")) {
			make ();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
