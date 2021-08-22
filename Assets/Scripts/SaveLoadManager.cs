using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager{

	public static void Save(object savedObject) {
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + savedObject.GetType().ToString() + ".sd");
		bf.Serialize(file,savedObject);
		file.Close();
	}

	public static object Load<T>(string ojectType) {
		if (File.Exists(Application.persistentDataPath + "/" + ojectType + ".sd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + ojectType + ".sd", FileMode.Open);
			T returnedObject = (T)bf.Deserialize(file);
			file.Close();
			return returnedObject;
		}
		return null;
	}
}
