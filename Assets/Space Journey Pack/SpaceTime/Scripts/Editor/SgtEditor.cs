using UnityEngine;
using UnityEditor;
using System.Linq;

public abstract class SgtEditor<T> : Editor
	where T : MonoBehaviour
{
	protected T Target;
	
	protected T[] Targets;
	
	public override void OnInspectorGUI()
	{
		Helper.BaseRect    = Helper.Reserve(0.0f);
		Helper.BaseRectSet = true;
		
		Helper.BeginUndo("Changes to " + typeof(T).Name, targets);
		
		EditorGUI.BeginChangeCheck();
		{
			serializedObject.UpdateIfDirtyOrScript();
			
			Target  = (T)target;
			Targets = targets.Select(t => (T)t).ToArray();
			
			Separator();
			
			OnInspector();
			
			Separator();
			
			serializedObject.ApplyModifiedProperties();
		}
		if (EditorGUI.EndChangeCheck() == true)
		{
			GUI.changed = true; Repaint();
			
			foreach (var t in Targets)
			{
				Helper.SetDirty(t);
			}
		}
		
		Helper.BaseRectSet = false;
	}
	
	public virtual void OnSceneGUI()
	{
		Target = (T)target;
		
		OnScene();
		
		if (GUI.changed == true)
		{
			Helper.SetDirty(target);
		}
	}
	
	protected void Each(System.Action<T> update)
	{
		foreach (var t in Targets)
		{
			update(t);
		}
	}
	
	protected bool Any(System.Func<T, bool> check)
	{
		foreach (var t in Targets)
		{
			if (check(t) == true)
			{
				return true;
			}
		}
		
		return false;
	}
	
	protected bool All(System.Func<T, bool> check)
	{
		foreach (var t in Targets)
		{
			if (check(t) == false)
			{
				return false;
			}
		}
		
		return true;
	}
	
	protected bool Button(string text)
	{
		var rect = Helper.Reserve();
		
		return GUI.Button(rect, text);
	}
	
	protected bool HelpButton(string helpText, UnityEditor.MessageType type, string buttonText, float buttonWidth)
	{
		var clicked = false;
		
		EditorGUILayout.BeginHorizontal();
		{
			EditorGUILayout.HelpBox(helpText, type);
			
			var style = new GUIStyle(GUI.skin.button); style.wordWrap = true;
			
			clicked = GUILayout.Button(buttonText, style, GUILayout.ExpandHeight(true), GUILayout.Width(buttonWidth));
		}
		EditorGUILayout.EndHorizontal();
		
		return clicked;
	}
	
	protected void BeginError(bool error = true)
	{
		EditorGUILayout.BeginVertical(error == true ? Helper.Error : Helper.NoError);
	}
	
	protected void EndError()
	{
		EditorGUILayout.EndVertical();
	}
	
	protected void DrawDefault(string proeprtyPath)
	{
		EditorGUILayout.BeginVertical(Helper.NoError);
		{
			EditorGUILayout.PropertyField(serializedObject.FindProperty(proeprtyPath), true);
		}
		EditorGUILayout.EndVertical();
	}
	
	protected void Separator()
	{
		EditorGUILayout.Separator();
	}
	
	protected void BeginIndent()
	{
		EditorGUI.indentLevel += 1;
	}
	
	protected void EndIndent()
	{
		EditorGUI.indentLevel -= 1;
	}
	
	protected virtual void OnInspector()
	{
	}
	
	protected virtual void OnScene()
	{
	}
}