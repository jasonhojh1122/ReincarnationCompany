using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class SwitchSceneRendererFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class FeatureSettings
    {
        public bool IsEnabled = true;
        public RenderPassEvent insertOn = RenderPassEvent.AfterRendering;
        public Material material;
    }

    [SerializeField] FeatureSettings settings = new FeatureSettings();

    RenderTargetHandle renderTargetHandle;
    SwitchSceneRenderPass renderPass;

    public override void Create()
    {
        renderPass = new SwitchSceneRenderPass(settings.material);
        renderPass.renderPassEvent = settings.insertOn;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (!settings.IsEnabled)
        {
            return;
        }
        renderPass.SetSource(renderer.cameraColorTarget);
        renderer.EnqueuePass(renderPass);
    }
}
