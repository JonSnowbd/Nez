﻿using Microsoft.Xna.Framework.Graphics;


namespace Nez
{
	public class GrayscaleEffect : Effect
	{
		public static readonly byte[] EffectBytes = EffectResource.GetFileResourceBytes("Content/nez/effects/Grayscale.mgfxo");
		public GrayscaleEffect() : base(Core.GraphicsDevice, EffectBytes)
		{
		}
	}
}