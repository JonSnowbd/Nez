﻿using System.Collections.Generic;

namespace Ash
{
	/// <summary>
	/// Represents a virtual button, axis or joystick whose state is determined by the state of its VirtualInputNodes
	/// </summary>
	public abstract class VirtualInput
	{
		public enum OverlapBehavior
		{
			/// <summary>
			/// duplicate input will result in canceling each other out and no input will be recorded. Example: press left arrow key and while
			/// holding it down press right arrow. This will result in canceling each other out.
			/// </summary>
			CancelOut,

			/// <summary>
			/// the first input found will be used
			/// </summary>
			TakeOlder,

			/// <summary>
			/// the last input found will be used
			/// </summary>
			TakeNewer
		};

		// Guard related items.
		public delegate bool GuardDelegate();
		/// <summary>
		/// Guards is a chain of boolean checks that can halt a virtual input
		/// from taking place before even checking if it actually happened.
		/// This is useful for stopping input when a UI is focused or creating
		/// complex control layouts where an input is only on when another input is held.
		/// </summary>
		public List<GuardDelegate> Guards;
		protected bool GuardCache;

		protected VirtualInput()
		{
			Guards = new List<GuardDelegate>();
			Input._virtualInputs.Add(this);
		}

		/// <summary>
		/// This will calculate and assign `GuardCache`. Do this once per frame in your
		/// virtual inputs.
		/// </summary>
		protected void CalculateGuardCache()
		{
			GuardCache = true;
			if (Guards.Count == 0)
				return;
			else
				foreach (var guard in Guards)
				{
					if (guard() == false)
					{
						GuardCache = false;
						return;
					}
				}
		}

		/// <summary>
		/// deregisters the VirtualInput from the Input system. Call this when you are done polling the VirtualInput
		/// </summary>
		public void Deregister()
		{
			Input._virtualInputs.Remove(this);
		}

		/// <summary>
		/// This will consume the input. For this frame/update, every check after Consumption will return false/0.
		/// Useful for stopping input from a specific virtual input to prevent it from activating multiple
		/// actions.
		/// </summary>
		public void Consume()
		{
			GuardCache = false;
		}

		public abstract void Update();
	}


	/// <summary>
	/// Add these to your VirtualInput to define how it determines its current input state. 
	/// For example, if you want to check whether a keyboard key is pressed, create a VirtualButton and add to it a VirtualButton.KeyboardKey
	/// </summary>
	public abstract class VirtualInputNode
	{
		public virtual void Update()
		{
		}
	}
}