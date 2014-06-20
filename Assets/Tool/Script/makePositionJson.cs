using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Collections.Generic;

public class makePositionJson : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}

	void Make() {
		string fileName;
		if (Application.platform == RuntimePlatform.WindowsEditor)
			fileName = Directory.GetCurrentDirectory () + "\\Assets\\Data\\positions.txt";
		else
			fileName = Directory.GetCurrentDirectory () + "/Assets/Data/positions.txt";
		
		System.IO.File.WriteAllText (fileName, "");				//초기화.

		using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true)) {
			file.WriteLine("{");

	
			List<Dictionary<string, string>> L_treepeople = new List<Dictionary<string, string>>();
			foreach (GameObject position in GameObject.FindGameObjectsWithTag("TreePeople")) {
				Dictionary<string, string> D_treepeople = new Dictionary<string, string> ();
				D_treepeople.Add ("x", position.transform.position.x.ToString ("0.##"));
				D_treepeople.Add ("y", position.transform.position.y.ToString ("0.##"));
				L_treepeople.Add(D_treepeople);
			}
			file.WriteLine("\t\"treepeople_positions\" : "+ MiniJSON.Json.Serialize(L_treepeople) + ",");


			List<Dictionary<string, string>> L_hero = new List<Dictionary<string, string>>();
			foreach (GameObject position in GameObject.FindGameObjectsWithTag("Hero")) {
				Dictionary<string, string> D_hero = new Dictionary<string, string> ();
				D_hero.Add ("x", position.transform.position.x.ToString ("0.##"));
				D_hero.Add ("y", position.transform.position.y.ToString ("0.##"));
				L_hero.Add(D_hero);
			}
			file.WriteLine("\t\"hero_positions\" : "+ MiniJSON.Json.Serialize(L_hero) + ",");

			List<Dictionary<string, string>> L_buff = new List<Dictionary<string, string>>();
			foreach (GameObject position in GameObject.FindGameObjectsWithTag("Buff")) {
				Dictionary<string, string> D_buff = new Dictionary<string, string> ();
				D_buff.Add ("name", position.name);
				D_buff.Add ("x", position.transform.position.x.ToString ("0.##"));
				D_buff.Add ("y", position.transform.position.y.ToString ("0.##"));
				L_buff.Add(D_buff);
			}
			file.WriteLine("\t\"monster_buff_spot_positions\" : "+ MiniJSON.Json.Serialize(L_buff) + ",");

			List<Dictionary<string, string>> L_empty = new List<Dictionary<string, string>>();
			foreach (GameObject position in GameObject.FindGameObjectsWithTag("Empty")) {
				Dictionary<string, string> D_empty = new Dictionary<string, string> ();
				D_empty.Add ("name", position.name);
				D_empty.Add ("x", position.transform.position.x.ToString ("0.##"));
				D_empty.Add ("y", position.transform.position.y.ToString ("0.##"));
				L_empty.Add(D_empty);
			}
			file.WriteLine("\t\"empty_positions\" : "+ MiniJSON.Json.Serialize(L_empty) + ",");

			Dictionary<string, string> D_babytree = new Dictionary<string, string> ();
			foreach (GameObject position in GameObject.FindGameObjectsWithTag("BabyTree")) {
				D_babytree.Add ("x", position.transform.position.x.ToString ("0.##"));
				D_babytree.Add ("y", position.transform.position.y.ToString ("0.##"));
			}
			file.WriteLine("\t\"babytree_position\" : "+ MiniJSON.Json.Serialize(D_babytree) + ",");


			Dictionary<string, string> D_Fence = new Dictionary<string, string> ();
			foreach (GameObject position in GameObject.FindGameObjectsWithTag("Fence")) {
				D_Fence.Add ("x", position.transform.position.x.ToString ("0.##"));
				D_Fence.Add ("y", position.transform.position.y.ToString ("0.##"));
			}
			file.WriteLine("\t\"fence_position\" : "+ MiniJSON.Json.Serialize(D_Fence) + ",");


			List<Dictionary<string, string>> L_animal = new List<Dictionary<string, string>>();
			foreach (GameObject position in GameObject.FindGameObjectsWithTag("Animal")) {
				Dictionary<string, string> D_animal = new Dictionary<string, string> ();
				D_animal.Add ("x", position.transform.position.x.ToString ("0.##"));
				D_animal.Add ("y", position.transform.position.y.ToString ("0.##"));
				L_animal.Add(D_animal);
			}
			file.WriteLine("\t\"mission_animal_spot_positions\" : "+ MiniJSON.Json.Serialize(L_animal) + ",");



			List<Dictionary<string, string>> L_alert = new List<Dictionary<string, string>>();
			GameObject[] alertArray = GameObject.FindGameObjectsWithTag("Alert");
			for(int i =1; i<alertArray.Length + 1; i++) {
				foreach(GameObject position in alertArray) {
					if(position.GetComponent<Alert>().pathGroupNumber == i) {
						Dictionary<string, string> D_alert = new Dictionary<string, string> ();
						D_alert.Add ("x", position.transform.position.x.ToString ("0.##"));
						D_alert.Add ("y", position.transform.position.y.ToString ("0.##"));
						L_alert.Add(D_alert);
					}
				}

			}
			file.WriteLine("\t\"siren_spot_positions\" : "+ MiniJSON.Json.Serialize(L_alert));






			file.WriteLine("}");

		}



		Debug.Log("Complete making position file");
	}


	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 70, 80, 50),"Position")) {
			Make ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
