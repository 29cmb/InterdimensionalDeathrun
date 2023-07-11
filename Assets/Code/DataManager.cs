using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        // Set the save file path to InterdimensionalSave.dat in the persistent data path
        saveFilePath = Path.Combine(Application.persistentDataPath, "InterdimensionalSave.dat");
    }

    // Save data with a key-value pair
    public void SaveData(string key, string value)
    {
        // Create a new or overwrite existing save file
        using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Append))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, key);
            binaryFormatter.Serialize(fileStream, value);
        }
    }

    // Load data using the key
    public string LoadData(string key)
    {
        if (File.Exists(saveFilePath))
        {
            using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                while (fileStream.Position < fileStream.Length)
                {
                    string loadedKey = (string)binaryFormatter.Deserialize(fileStream);
                    string loadedValue = (string)binaryFormatter.Deserialize(fileStream);
                    if (loadedKey == key)
                    {
                        return loadedValue;
                    }
                }
            }
        }

        return null;
    }

    // Delete data using the key
    public void DeleteData(string key)
    {
        if (File.Exists(saveFilePath))
        {
            // Create a temporary file
            string tempFilePath = Path.Combine(Application.persistentDataPath, "temp.dat");

            using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Open))
            using (FileStream tempFileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                while (fileStream.Position < fileStream.Length)
                {
                    string loadedKey = (string)binaryFormatter.Deserialize(fileStream);
                    string loadedValue = (string)binaryFormatter.Deserialize(fileStream);
                    if (loadedKey != key)
                    {
                        binaryFormatter.Serialize(tempFileStream, loadedKey);
                        binaryFormatter.Serialize(tempFileStream, loadedValue);
                    }
                }
            }

            // Replace the original save file with the temporary file
            File.Delete(saveFilePath);
            File.Move(tempFilePath, saveFilePath);
        }
    }
}
