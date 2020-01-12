using UnityEngine;

namespace EboxGames
{
    public class Storage
    {
        public static bool Write(string key, object obj, bool encrypt = false) 
        {
            if(obj is string)
            {
                Debug.LogError( $"Store.Write() given object is string. It must be pure C# class. {obj}" );
                return false;
            }

            if (encrypt)
                PlayerPrefs.SetString(key, ToEncodedJSON(obj));
            else
                PlayerPrefs.SetString(key, ToEncodedJSON(obj));

            return true;
        }

        public static bool Read<T>(string key, ref T value, bool encrypt = false)// where T : new()
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.LogError("Storage : the given key's value pair does not exist! key:  " + key);
                return false;
            }

            if (encrypt)
                value = JsonUtility.FromJson<T>(FromEncodedJSON(PlayerPrefs.GetString(key)));
            else
                value = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));

            if (value == null)
            {
                Debug.LogError("Storage : operation failed while trying to read!");
                return false;
            }

            return true;
        }

        // Convert object to base64string
        internal static string ToEncodedJSON(object obj)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(JsonUtility.ToJson(obj)));
        }

        // Convert base64string to JSON string
        internal static string FromEncodedJSON(string source)
        {
            return System.Text.Encoding.Unicode.GetString(System.Convert.FromBase64String(source));
        }
    }
}
