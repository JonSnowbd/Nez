﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Ash
{
	public class TwistEffect : Effect
	{
		public static readonly byte[] EffectBytes = EffectResource.GetFileResourceBytes("Content/nez/effects/Twist.mgfxo");
		[Range(0, 2)]
		public float Radius
		{
			get => _radius;
			set
			{
				if (_radius != value)
				{
					_radius = value;
					_radiusParam.SetValue(_radius);
				}
			}
		}

		[Range(-50, 50)]
		public float Angle
		{
			get => _angle;
			set
			{
				if (_angle != value)
				{
					_angle = value;
					_angleParam.SetValue(_angle);
				}
			}
		}

		public Vector2 Offset
		{
			get => _offset;
			set
			{
				if (_offset != value)
				{
					_offset = value;
					_offsetParam.SetValue(_offset);
				}
			}
		}

		float _radius = 0.5f;
		float _angle = 5f;
		Vector2 _offset = Vector2Ext.HalfVector();

		EffectParameter _radiusParam;
		EffectParameter _angleParam;
		EffectParameter _offsetParam;


		public TwistEffect() : base(Core.GraphicsDevice, EffectBytes)
		{
			_radiusParam = Parameters["radius"];
			_angleParam = Parameters["angle"];
			_offsetParam = Parameters["offset"];

			_radiusParam.SetValue(_radius);
			_angleParam.SetValue(_angle);
			_offsetParam.SetValue(_offset);
		}
	}
}