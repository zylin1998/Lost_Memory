using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
	public static void SaveData<T> (T data, string path, string fileName)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		
		if(!Directory.Exists(path)) { Directory.CreateDirectory(path); }

		path = Path.Combine(path, fileName);
	
		FileStream stream = new FileStream(path, FileMode.Create);

		formatter.Serialize(stream, data);

		stream.Close();
	}

	public static T LoadData<T>(string fileName)
	{
		string path = fileName;
	
		if (!File.Exists(path)) { return default(T);}
	
		BinaryFormatter formatter = new BinaryFormatter();
		
		FileStream stream = new FileStream(path, FileMode.Open);

		T data = (T)formatter.Deserialize(stream);

		stream.Close();

		return data;
	}

	public static void SaveJsonData<T>(T data, string path, string fileName) 
	{
		if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

		path = Path.Combine(path, fileName + ".json");

		string json = JsonUtility.ToJson(data, true);

		Debug.Log(json);

		if (!File.Exists(path)) { File.Create(path).Dispose(); }

		File.WriteAllText(path, json);

		Debug.Log(json);
	}

	public static T LoadJsonData<T>(string fileName)
	{
		string path = fileName + ".json";

		if (!File.Exists(path)) { return default(T); }

		string json = File.ReadAllText(path);

		Debug.Log(json);

		T data = JsonUtility.FromJson<T>(json);

		return data;
	}
}