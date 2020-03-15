using UnityEngine;

[ExecuteInEditMode]
public class Black_Hole_Effect: MonoBehaviour {
    public Shader  shader;
    
    public float   ratio = 1;  	//Отношение высоты к длине экрана, для правильного отображения шейдера
    public float   radius = 0; 	//Радиус черной дыры измеряемый в тех же единицах, что и остальные объекты на сцене

    public GameObject BH;  //Объект, позиция которого берется за позицию черной дыры

    private Material _material; //Материал на котором будет находится шейдер
    protected Material material {
        get {
            if (_material == null) {
                _material = new Material (shader);
                _material.hideFlags = HideFlags.HideAndDontSave;
            }
            return _material;
        } 
    }

    protected virtual void OnDisable() {
        if( _material ) {
            DestroyImmediate( _material );
        }
    }

    void OnRenderImage (RenderTexture source, RenderTexture destination) {
        if (shader && material) {
            //Находим позицию черной дыры в экранных координатах
            Vector2 pos = new Vector2(
                GetComponent<Camera>().WorldToScreenPoint (BH.transform.position).x / GetComponent<Camera>().pixelWidth,
                1-GetComponent<Camera>().WorldToScreenPoint (BH.transform.position).y / GetComponent<Camera>().pixelHeight);

            //Устанавливаем все необходимые для шейдера параметры
            material.SetVector("_Position", new Vector2(pos.x, pos.y));
            material.SetFloat("_Ratio", ratio);
            material.SetFloat("_Rad", radius);
            material.SetFloat("_Distance", Vector3.Distance(BH.transform.position, transform.position));
            //И применяем к полученному изображению.
            Graphics.Blit(source, destination, material);
        }
    }
}