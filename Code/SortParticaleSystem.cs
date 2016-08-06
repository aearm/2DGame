using UnityEngine;
using System.Collections;

public class SortParticaleSystem : MonoBehaviour {

    public string LayerName = "Particles";
    public void Start()
    {
        particleSystem.renderer.sortingLayerName = LayerName;

    }
}
