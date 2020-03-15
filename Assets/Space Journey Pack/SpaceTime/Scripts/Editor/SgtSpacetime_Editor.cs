using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SgtSpacetime))]
public class SgtSpacetime_Editor : SgtEditor<SgtSpacetime>
{
	protected override void OnInspector()
	{
		DrawDefault("Color");
		
		BeginError(Any(t => t.Brightness < 0.0f));
		{
			DrawDefault("Brightness");
		}
		EndError();
		
		Separator();
		
		BeginError(Any(t => t.MainTex == null));
		{
			DrawDefault("MainTex");
		}
		EndError();
		
		BeginError(Any(t => t.Tile <= 0));
		{
			DrawDefault("Tile");
		}
		EndError();
		
		DrawDefault("Effect");
		
		if (Any(t => t.Effect == SgtSpacetimeEffect.Pinch))
		{
			BeginIndent();
			{
				BeginError(Any(t => t.Power < 0.0f));
				{
					DrawDefault("Power");
				}
				EndError();
			}
			EndIndent();
		}
		
		if (Any(t => t.Effect == SgtSpacetimeEffect.Offset))
		{
			BeginIndent();
			{
				DrawDefault("Offset");
			}
			EndIndent();
		}
		
		Separator();
		
		BeginError(Any(t => t.Renderers.Count == 0 || t.Renderers.Exists(r => r == null) == true));
		{
			DrawDefault("Renderers");
		}
		EndError();
		
		Separator();
		
		BeginError(Any(t => t.Wells.Count == 0 || t.Wells.Exists(r => r == null) == true));
		{
			DrawDefault("Wells");
		}
		EndError();
	}
}