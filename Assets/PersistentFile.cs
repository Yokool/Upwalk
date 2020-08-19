using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public abstract class PersistentFile
{
    private string filePath;
    public string FilePath => filePath;
    
    /// <summary>
    /// FOR SERIALIZATION DO NOT USE
    /// </summary>
    public PersistentFile()
    {

    }


    public PersistentFile(string filePath)
    {
        this.filePath = filePath;
    }
    public void Load()
    {
        XmlSerializer serializer = new XmlSerializer(GetType());

        if (FileExists())
        {
            FileStream stream = GetStream(FileMode.Open);
            object deserializedObject = serializer.Deserialize(stream);
            PopulateFields(deserializedObject);
            stream.Close();
        }

    }

    public void Save()
    {
        XmlSerializer serializer = new XmlSerializer(GetType());

        FileStream stream = GetStream(FileMode.OpenOrCreate);
        StreamWriter writer = GetStreamWriter(stream);
        serializer.Serialize(writer, this);
        writer.Close();
        stream.Close();

    }

    public abstract void PopulateFields(object deserializedObject);

    protected FileStream GetStream(FileMode fileMode)
    {
        return File.Open(Application.persistentDataPath + "\\" + filePath, fileMode);
    }

    protected StreamWriter GetStreamWriter(FileStream stream)
    {
        return new StreamWriter(stream);
    }

    protected bool FileExists()
    {
        return File.Exists(Application.persistentDataPath + "\\" + filePath);
    }

}
