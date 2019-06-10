using UnityEngine;

public static class ExtensionMethods
{

  public static Vector3 ToVector3(this Vector2 v, float z)
  {
    return new Vector3(v.x, v.y, z);
  }

  public static Vector2 ToVector2(this Vector3 v)
  {
    return new Vector2(v.x, v.y);
  }

  public static void XAxis(this Transform t, float x)
  {
    t.position = new Vector3(t.position.x + x, t.position.y, t.position.z);
  }
  
  public static bool isPositive(this float value){
		return value > 0;
	}

	// YAxis'i verilen değere eşitler
	public static void MoveToYAxis(this Transform transform,float yAxis){
		Vector3 position = transform.position;
		position.y = yAxis;
		// Debug.Log(yAxis);
		transform.position = position;
	}

	// YAxis e verilen değeri ekler
	public static void MoveFromYAxis(this Transform transform,float yAxis){
		Vector3 position = transform.position;
		position.y += yAxis;
		// Debug.Log(yAxis);
		transform.position = position;
	}

	//public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
 //   {
 //       MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
 //       return expressionBody.Member.Name;
 //   }


}