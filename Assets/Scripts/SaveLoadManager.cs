using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

    public static void Save(object savedObject) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetPath(savedObject.GetType().ToString()));
        bf.Serialize(file, savedObject);
        file.Close();
    }

    public static object Load<T>(string objectType) {
        if (File.Exists(GetPath(objectType))) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(GetPath(objectType), FileMode.Open);
            T returnedObject = (T)bf.Deserialize(file);
            file.Close();
            return returnedObject;
        }

        return null;
    }

    private static string GetPath(string objectType) {
        return Application.persistentDataPath + "/" + objectType + ".sd";
    }
}
