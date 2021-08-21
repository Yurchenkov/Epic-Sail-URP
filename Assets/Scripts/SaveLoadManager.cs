using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
	//public static Dictionary<string, object> savedGames = new Dictionary<string,object>();

	//it's static so we can call it from anywhere
	public static void Save(object savedObject) {
		
		//savedGames.Add(savedObject.GetType().ToString(), savedObject.GetType());
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create(Application.persistentDataPath + "/" + savedObject.GetType().ToString() + ".sd"); //you can call it anything you want
		bf.Serialize(file,savedObject);
		file.Close();
	}

	public static object Load<T>(object loadedObject) {
		if (File.Exists(Application.persistentDataPath + "/" + loadedObject.GetType().ToString() + ".sd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + loadedObject.GetType().ToString() + ".sd", FileMode.Open);
			T returnedObject = (T)bf.Deserialize(file);
			file.Close();
			return returnedObject;
		}
		return null;
	}
}
