
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiverBackground : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Sprite> backgrounds;
    [SerializeField] CDF backgroundsCDF;
    [SerializeField] Material mat;

    Texture2D tex1, tex2;

    private void Awake()
    {
        backgroundsCDF.CalculateCDF();
        tex1 = GetRandomTex();
        tex2 = GetRandomTex();
        SetMatTex();
    }

    private void Start()
    {
        StartCoroutine(Flow());
    }

    IEnumerator Flow()
    {
        float shift = 0.0f;
        while (true)
        {
            shift += Time.deltaTime * speed;
            shift = Mathf.Clamp01(shift);
            if (Utils.Fuzzy.CloseFloat(shift, 1.0f))
            {
                UpdateTex();
                SetMatTex();
                shift = 0.0f;
            }
            mat.SetFloat("_Shift", shift);
            yield return null;
        }
    }

    void SetMatTex()
    {
        mat.SetTexture("_Tex1", tex1);
        mat.SetTexture("_Tex2", tex2);
    }

    void UpdateTex()
    {
        tex1 = tex2;
        tex2 = GetRandomTex();
    }

    Texture2D GetRandomTex()
    {
        return backgrounds[backgroundsCDF.GetRandomID()].texture;
    }

}