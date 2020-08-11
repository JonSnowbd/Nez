﻿using System;


namespace Nez
{
	/// <summary>
	/// Renderer that renders using its own Camera which doesnt move.
	/// </summary>
	public class ScreenSpaceRenderer : Renderer
	{
		public int[] RenderLayers;


		public ScreenSpaceRenderer(int renderOrder, params int[] renderLayers) : base(renderOrder, null)
		{
			Array.Sort(renderLayers);
			Array.Reverse(renderLayers);
			RenderLayers = renderLayers;
			WantsToRenderAfterPostProcessors = true;
		}

		public override void Render(ECScene scene)
		{
			BeginRender(Camera);

			for (var i = 0; i < RenderLayers.Length; i++)
			{
				var renderables = scene.RenderableComponents.ComponentsWithRenderLayer(RenderLayers[i]);
				for (var j = 0; j < renderables.Length; j++)
				{
					var renderable = renderables.Buffer[j];
					if (renderable.Enabled && renderable.IsVisibleFromCamera(Camera))
						RenderAfterStateCheck(renderable, Camera);
				}
			}

			if (ShouldDebugRender && ECScene.DebugRenderEnabled)
				DebugRender(scene, Camera);

			EndRender();
		}

		protected override void DebugRender(ECScene scene, Camera cam)
		{
			Graphics.Instance.Batcher.End();
			Graphics.Instance.Batcher.Begin(cam.TransformMatrix);

			for (var i = 0; i < RenderLayers.Length; i++)
			{
				var renderables = scene.RenderableComponents.ComponentsWithRenderLayer(RenderLayers[i]);
				for (var j = 0; j < renderables.Length; j++)
				{
					var entity = renderables.Buffer[j];
					if (entity.Enabled)
						entity.DebugRender(Graphics.Instance.Batcher);
				}
			}
		}

		public override void OnSceneBackBufferSizeChanged(int newWidth, int newHeight)
		{
			base.OnSceneBackBufferSizeChanged(newWidth, newHeight);

			if(Core.Scene is ECScene scene && Camera == null)
			{
				Camera = scene.CreateEntity("screenspace camera").AddComponent<Camera>();
			}
		}
	}
}