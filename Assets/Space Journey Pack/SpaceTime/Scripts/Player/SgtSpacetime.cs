using UnityEngine;
using System.Collections.Generic;

public enum SgtSpacetimeEffect
{
	Pinch,
	Offset
}

[ExecuteInEditMode]
//[AddComponentMenu(SgtHelper.ComponentMenuPrefix + "Spacetime")]
public class SgtSpacetime : MonoBehaviour
{
	public static List<SgtSpacetime> AllSpacetimes = new List<SgtSpacetime>();
	
	public Texture2D MainTex;
	
	public Color Color = Color.white;
	
	public float Brightness = 1.0f;
	
	public int Tile = 1;
	
	public SgtSpacetimeEffect Effect = SgtSpacetimeEffect.Pinch;
	
	public float Power = 3.0f;
	
	public Vector3 Offset;
	
	public List<SgtSpacetimeWell> Wells = new List<SgtSpacetimeWell>();
	
	public List<MeshRenderer> Renderers = new List<MeshRenderer>();
	
	[System.NonSerialized]
	protected Material material;
	
	protected static List<string> keywords = new List<string>();
	
	public void UpdateState()
	{
		UpdateMaterial();
		UpdateRenderers();
	}
	
	[ContextMenu("Add Well")]
	public SgtSpacetimeWell AddWell()
	{
		var well = SgtSpacetimeWell.Create(this);
#if UNITY_EDITOR
		Helper.SelectAndPing(well);
#endif
		return well;
	}
	
#if UNITY_EDITOR
	protected virtual void OnDrawGizmos()
	{
		UpdateState();
	}
#endif
	
	protected virtual void Reset()
	{
		var meshRenderer = GetComponent<MeshRenderer>();
		
		if (meshRenderer != null)
		{
			Renderers.Clear();
			Renderers.Add(meshRenderer);
		}
	}
	
	protected virtual void OnEnable()
	{
		AllSpacetimes.Add(this);
	}
	
	protected virtual void OnDisable()
	{
		AllSpacetimes.Remove(this);
		
		for (var i = Renderers.Count - 1; i >= 0; i--)
		{
			var renderer = Renderers[i];
			
			Helper.RemoveMaterial(renderer, material);
		}
	}
	
	protected virtual void OnDestroy()
	{
		for (var i = Renderers.Count - 1; i >= 0; i--)
		{
			var renderer = Renderers[i];
			
			Helper.RemoveMaterial(renderer, material);
		}
		
		Helper.Destroy(material);
	}
	
	protected virtual void Update()
	{
		UpdateState();
	}
	
	protected virtual void UpdateMaterial()
	{
		if (material == null) material = Helper.CreateTempMaterial(Helper.ShaderNamePrefix + "Spacetime");
		
		var color       = Helper.Brighten(Color, Brightness);
		//var renderQueue = (int)RenderQueue + RenderQueueOffset;
		var wellCount   = WriteWells(7);
		
		//material.renderQueue = renderQueue;
		material.SetTexture("_MainTex", MainTex);
		material.SetColor("_Color", color);
		material.SetFloat("_Tile", Tile);
		
		switch (Effect)
		{
			case SgtSpacetimeEffect.Pinch:
			{
				material.SetFloat("_Power", Power);
			}
			break;
			
			case SgtSpacetimeEffect.Offset:
			{
				keywords.Add("SGT_A");
				material.SetVector("_Offset", Offset);
			}
			break;
		}
		
		if ((wellCount & 1 << 0) != 0)
		{
			keywords.Add("SGT_B");
		}
		
		if ((wellCount & 1 << 1) != 0)
		{
			keywords.Add("SGT_C");
		}
		
		if ((wellCount & 1 << 2) != 0)
		{
			keywords.Add("SGT_D");
		}
		
		Helper.SetKeywords(material, keywords); keywords.Clear();
	}
	
	private void UpdateRenderers()
	{
		for (var i = Renderers.Count - 1; i >= 0; i--)
		{
			var renderer = Renderers[i];
			
			if (renderer != null && renderer.sharedMaterial != material)
			{
				Helper.BeginStealthSet(renderer);
				{
					renderer.sharedMaterial = material;
				}
				Helper.EndStealthSet();
			}
		}
	}
	
	private int WriteWells(int maxWells)
	{
		var wellCount = 0;
		
		for (var i = Wells.Count - 1; i >= 0; i--)
		{
			var well = Wells[i];
			
			if (Helper.Enabled(well) == true && well.Radius > 0.0f && wellCount < maxWells)
			{
				var prefix   = "_Well" + (++wellCount);
				
				material.SetVector(prefix + "_Pos", well.transform.position);
				material.SetVector(prefix + "_Dat", new Vector4(well.Radius, well.Age, well.Strength, 0.0f));
			}
		}
		
		return wellCount;
	}
}