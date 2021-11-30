using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

class SwitchSceneRenderPass : ScriptableRenderPass
{
    Material material;
    RenderTargetIdentifier source;
    RenderTargetHandle tmpTexture;
    public SwitchSceneRenderPass(Material material) : base()
    {
        this.material = material;
        tmpTexture.Init("_TempSwitchSceneTexture");
    }

    public void SetSource(RenderTargetIdentifier source)
    {
        this.source = source;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get("SwitchSceneRendererFeature");

        int w = renderingData.cameraData.camera.scaledPixelWidth >> 3;
        int h = renderingData.cameraData.camera.scaledPixelHeight >> 3;

        cmd.GetTemporaryRT(tmpTexture.id, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
        cmd.Blit(source, source, material, 0);
        // Blit(cmd, source, tmpTexture.Identifier(), material, 0);
        // Blit(cmd, tmpTexture.Identifier(), source);
        cmd.ReleaseTemporaryRT(tmpTexture.id);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    /* public override void FrameCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(tmpTexture.id);
        base.FrameCleanup(cmd);
    } */
}
