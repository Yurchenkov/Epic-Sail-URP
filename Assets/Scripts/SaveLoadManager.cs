using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

    private static BinaryFormatter _binaryFormatter = new BinaryFormatter();

    public static void Save(object savedObject) {
        FileStream file = File.Create(GetPath(savedObject.GetType().ToString()));
        _binaryFormatter.Serialize(file, savedObject);
        file.Close();
    }

    public static object Load<T>(string objectType) {
        if (File.Exists(GetPath(objectType))) {
            FileStream file = File.Open(GetPath(objectType), FileMode.Open);
            T returnedObject = (T)_binaryFormatter.Deserialize(file);
            file.Close();
            return returnedObject;
        }

        return null;
    }

    private static string GetPath(string objectType) {
        return $"{Application.persistentDataPath}/{objectType}.sd";
    }
}
