﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ash.Textures;


namespace Ash
{
	/// <summary>
	/// used in conjunction with the ReflectionRenderer
	/// </summary>
	public class ReflectionMaterial : Material<ReflectionEffect>
	{
		public RenderTexture RenderTexture;

		RenderTarget2D _renderTarget;


		public ReflectionMaterial(ReflectionRenderer reflectionRenderer) : base(new ReflectionEffect())
		{
			RenderTexture = reflectionRenderer.RenderTexture;
		}


		public override void OnPreRender(Matrix viewProj)
		{
			// only update the Shader when the renderTarget changes. it will be swapped out whenever the GraphicsDevice resets.
			if (_renderTarget == null || _renderTarget != RenderTexture.RenderTarget)
			{
				_renderTarget = RenderTexture.RenderTarget;
				Effect.RenderTexture = RenderTexture.RenderTarget;
			}
			Effect.MatrixTransform = viewProj;
		}
	}
}