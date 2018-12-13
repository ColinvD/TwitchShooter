using UnityEngine;
using UnityEditor;
using System.IO;

public class HandleTextFile : MonoBehaviour
{
    private void Start()
    {
        string testing = ReadString("Assets/Resources/test.txt");
        //Debug.Log(testing);
    }

    [MenuItem("Tools/Read file")]
    public string ReadString(string path)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string result = reader.ReadToEnd();
        reader.Close();
        return result;
    }
}