using System.Collections.Generic;
using ImGuiNET;
using Ash.ImGuiTools.TypeInspectors;


namespace Ash.ImGuiTools.ObjectInspectors
{
	public class PostProcessorInspector
	{
		public PostProcessor PostProcessor => _postProcessor;

		protected List<AbstractTypeInspector> _inspectors;
		protected int _scopeId = NezImGui.GetScopeId();

		PostProcessor _postProcessor;

		// Used to hold a reference to the Cast scene, if it derives from `Scene`
		ECScene CastScene;

		public PostProcessorInspector(PostProcessor postProcessor)
		{
			_postProcessor = postProcessor;
			_inspectors = TypeInspectorUtils.GetInspectableProperties(postProcessor);

			// if we are a Material<T>, we need to fix the duplicate Effect due to the "new T effect"
			if (ReflectionUtils.IsGenericTypeOrSubclassOfGenericType(_postProcessor.GetType()))
			{
				var didFindEffectInspector = false;
				for (var i = 0; i < _inspectors.Count; i++)
				{
					var isEffectInspector = _inspectors[i] is Ash.ImGuiTools.TypeInspectors.EffectInspector;
					if (isEffectInspector)
					{
						if (didFindEffectInspector)
						{
							_inspectors.RemoveAt(i);
							break;
						}

						didFindEffectInspector = true;
					}
				}
			}

			for (var i = 0; i < _inspectors.Count; i++)
			{
				var effectInspector = _inspectors[i] as Ash.ImGuiTools.TypeInspectors.EffectInspector;
				if (effectInspector != null)
					effectInspector.AllowsEffectRemoval = false;
			}
		}

		public void Draw()
		{
			ImGui.PushID(_scopeId);
			var isOpen = ImGui.CollapsingHeader(_postProcessor.GetType().Name.Replace("PostProcessor", string.Empty));

			NezImGui.ShowContextMenuTooltip();

			if (ImGui.BeginPopupContextItem())
			{
				if (ImGui.Selectable("Remove PostProcessor"))
				{
					if(Core.Scene is ECScene s)
					{
						isOpen = false;
						s.RemovePostProcessor(_postProcessor);
						ImGui.CloseCurrentPopup();
					}
					
				}

				ImGui.EndPopup();
			}

			if (isOpen)
			{
				ImGui.Indent();
				foreach (var inspector in _inspectors)
					inspector.Draw();
				ImGui.Unindent();
			}

			ImGui.PopID();
		}
	}
}