using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	struct Alumno
	{
		public string name;
		public int age;
		public string degree;
		public string desiredJob;
		public string url;
	};

	private List<Alumno> alumnos;
	private XmlDocument document;
	string pathIn;

	[SerializeField]
	InputField nameHudIn;
	[SerializeField]
	InputField ageHudIn;
	[SerializeField]
	InputField degreeHudIn;
	[SerializeField]
	InputField desiredJobHudIn;
	[SerializeField]
	InputField urlHudIn;

	[SerializeField]
	InputField nameHudDelete;

	public Transform[] tables = new Transform[12];
	[SerializeField]
	GameObject studentPref;
	// Use this for initialization
	void Start () {
		alumnos = new List<Alumno> ();
		pathIn = Application.dataPath +"/Resources/Alumnos.xml";
		document = new XmlDocument();

		if (System.IO.File.Exists (pathIn)) {
			document.LoadXml (System.IO.File.ReadAllText(pathIn));
			ReadXml ();
		} else {
			document.LoadXml ("<Alumnos></Alumnos>");
			document.Save (pathIn);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void ReadXml(){

		GameObject[] alumList = GameObject.FindGameObjectsWithTag ("Alumno");
		foreach (GameObject alum in alumList) {
			Destroy (alum);
		}

		int i = 0;
		foreach(XmlElement node in document.SelectNodes("Alumnos/Alumno")){
			Alumno newAlum = new Alumno ();
			newAlum.name = node.SelectSingleNode ("name").InnerText;
			newAlum.age = int.Parse(node.SelectSingleNode ("age").InnerText);
			newAlum.degree = node.SelectSingleNode ("degree").InnerText;
			newAlum.desiredJob = node.SelectSingleNode ("desiredJob").InnerText;
			newAlum.url = node.SelectSingleNode ("url").InnerText;
			alumnos.Add (newAlum);
			GameObject aux = Instantiate (studentPref, tables [i].position, Quaternion.identity) as GameObject;
			aux.transform.parent = tables [i++];
			aux.transform.GetChild(1).Find ("Name").GetComponent<TextMesh> ().text = newAlum.name;
			aux.transform.GetChild(1).Find ("Age").GetComponent<TextMesh> ().text = newAlum.age.ToString();
			aux.transform.GetChild(1).Find ("Degree").GetComponent<TextMesh> ().text = newAlum.degree;
			aux.transform.GetChild(1).Find ("Desired Job").GetComponent<TextMesh> ().text = newAlum.desiredJob;
			aux.transform.GetChild(1).Find ("Url").GetComponent<Image> ().LlamarCorrutina(newAlum.url);
		}
	}


	public void _ADDALUM(){
		string name = nameHudIn.text;
		int age = int.Parse(ageHudIn.text);
		string degree = degreeHudIn.text;
		string desiredJob = desiredJobHudIn.text;
		string url = urlHudIn.text;

		XmlNode nameNode = document.CreateElement ("name");
		nameNode.InnerText = name;
		XmlNode ageNode = document.CreateElement ("age");
		ageNode.InnerText = age.ToString();
		XmlNode degreeNode = document.CreateElement ("degree");
		degreeNode.InnerText = degree;
		XmlNode jobNode = document.CreateElement ("desiredJob");
		jobNode.InnerText = desiredJob;
		XmlNode urlNode = document.CreateElement ("url");
		urlNode.InnerText = url;

		XmlElement alumXml = document.CreateElement ("Alumno");
		alumXml.AppendChild (nameNode);
		alumXml.AppendChild (ageNode);
		alumXml.AppendChild (degreeNode);
		alumXml.AppendChild (jobNode);
		alumXml.AppendChild (urlNode);

		document.SelectSingleNode ("Alumnos").AppendChild(alumXml);
		document.Save(pathIn);

		alumnos.Clear ();
		ReadXml ();
		nameHudIn.text = "";
		ageHudIn.text = "";
		degreeHudIn.text = "";
		desiredJobHudIn.text = "";
		urlHudIn.text = "";
	}

	public void _DELETEALUM(){
		string name = nameHudDelete.text;
		foreach (XmlNode alum in document.SelectNodes("Alumnos/Alumno")) {
			if (alum.SelectSingleNode ("name").InnerText == name) {
				alum.ParentNode.RemoveChild (alum);
			}
		}
		document.Save (pathIn);
		ReadXml ();
		nameHudDelete.text = "";
	}

}
