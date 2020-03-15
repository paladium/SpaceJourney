using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

public static partial class Helper
{
	public const string ShaderNamePrefix = "Hidden/Sgt";
	
	public static Color Brighten(Color color, float brightness)
	{
		color.r *= brightness;
		color.g *= brightness;
		color.b *= brightness;
		
		return color;
	}
	
	public static bool Enabled(Behaviour b)
	{
		return b != null && b.enabled == true && b.gameObject.activeInHierarchy == true;
	}
	
	
	public static T Destroy<T>(T o, bool recordUndo = true)
		where T : Object
	{
		if (o != null)
		{
#if UNITY_EDITOR
			if (Application.isPlaying == true)
			{
				Object.Destroy(o);
			}
			else
			{
				if (recordUndo == true)
				{
					Helper.UpdateUndo(o);
					
					Undo.DestroyObjectImmediate(o);
				}
				else
				{
					Object.DestroyImmediate(o);
				}
			}
#else
			Object.Destroy(o);
#endif
		}
		
		return null;
	}
	
	private static Object stealthSetObject;
	
	private static HideFlags stealthSetFlags;
	
	public static void BeginStealthSet(Object o)
	{
		if (o != null)
		{
			stealthSetObject = o;
			stealthSetFlags  = o.hideFlags;
			
			o.hideFlags = HideFlags.DontSave;
		}
	}
	
	public static void EndStealthSet()
	{
		if (stealthSetObject != null)
		{
			stealthSetObject.hideFlags = stealthSetFlags;
		}
	}
	
	public static Material CreateTempMaterial(string shaderName)
	{
		var shader = Shader.Find(shaderName);
		
		if (shader == null)
		{
			Debug.LogError("Failed to find shader: " + shaderName); return null;
		}
		
		var material = new Material(shader);
		
		material.hideFlags = HideFlags.DontSave | HideFlags.HideInInspector;
		
		return material;
	}
	
	private static List<Material> materials = new List<Material>();
	
	
	public static void RemoveMaterial(Renderer r, Material m)
	{
		if (r != null)
		{
			var sms = r.sharedMaterials;
			
			foreach (var sm in sms)
			{
				if (sm != null && sm != m)
				{
					materials.Add(sm);
				}
			}
			
			r.sharedMaterials = materials.ToArray(); materials.Clear();
		}
	}
	public static void SetKeywords(Material m, List<string> keywords)
	{
		if (m != null && ArraysEqual(m.shaderKeywords, keywords) == false)
		{
			m.shaderKeywords = keywords.ToArray();
		}
	}
	
	public static bool ArraysEqual<T>(T[] a, List<T> b)
	{
		if (a != null && b == null) return false;
		if (a == null && b != null) return false;
		
		if (a != null && b != null)
		{
			if (a.Length != b.Count) return false;
			
			var comparer = System.Collections.Generic.EqualityComparer<T>.Default;
			
			for (var i = 0; i < a.Length; i++)
			{
				if (comparer.Equals(a[i], b[i]) == false)
				{
					return false;
				}
			}
		}
		
		return true;
	}
	public static GameObject CreateGameObject(string name = "", Transform parent = null, bool recordUndo = true)
	{
		return CreateGameObject(name, parent, Vector3.zero, Quaternion.identity, Vector3.one, recordUndo);
	}
	
	public static GameObject CreateGameObject(string name, Transform parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, bool recordUndo = true)
	{
		var gameObject = new GameObject(name);
		
		gameObject.transform.parent        = parent;
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localRotation = localRotation;
		gameObject.transform.localScale    = localScale;
		
#if UNITY_EDITOR
		if (recordUndo == true)
		{
			Undo.RegisterCreatedObjectUndo(gameObject, undoName);
		}
#endif
		
		return gameObject;
	}
}