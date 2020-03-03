using UnityEngine;

[System.Serializable]
public class EnumObject
{
    public int enumAsInt;
    public Object _object;
}

public class EnumType : PropertyAttribute
{
    public System.Type type;

    public EnumType ( System.Type type )
    {
        this.type = type;
    }
}

//[CreateAssetMenu( fileName = "EnumAssetInstance" , menuName = "Assets/Custom/Enum Asset" , order = 1 )]
//public class EnumAsset : ScriptableObject
//{
//    pub

//}