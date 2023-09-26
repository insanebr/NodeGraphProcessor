using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GraphProcessor
{
	[CustomPropertyDrawer(typeof(ExposedParameter.Settings))]
	public class ExposedParameterSettingsDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			return CreateHideInInspectorField(property);
		}

		protected VisualElement CreateHideInInspectorField(SerializedProperty settingsProperty)
		{
			var isHidden = settingsProperty.FindPropertyRelative(nameof(ExposedParameter.Settings.isHidden));
			var graph = GetGraph(settingsProperty);
			var param = GetParameter(settingsProperty);
			var p = new PropertyField(isHidden, "Hide in Inspector");

			p.RegisterValueChangeCallback(e =>
			{
				settingsProperty.serializedObject.ApplyModifiedProperties();
				graph.NotifyExposedParameterChanged(param);
			});

			return p;
		}

		protected static BaseGraph GetGraph(SerializedProperty property) =>
			property.serializedObject.FindProperty("graph").objectReferenceValue as BaseGraph;

		protected static ExposedParameter GetParameter(SerializedProperty settingsProperty)
		{
			var guid = settingsProperty.FindPropertyRelative(nameof(ExposedParameter.Settings.guid)).stringValue;
			return GetGraph(settingsProperty).GetExposedParameterFromGUID(guid);
		}

		protected static PropertyField CreateSettingsField(SerializedProperty settingsProperty, string fieldName,
			string displayName = null)
		{
			var prop = settingsProperty.FindPropertyRelative(fieldName);

			if (displayName == null)
				displayName = ObjectNames.NicifyVariableName(fieldName);

			var p = new PropertyField(prop, displayName);
			p.Bind(settingsProperty.serializedObject);
			p.RegisterValueChangeCallback(e =>
			{
				settingsProperty.serializedObject.ApplyModifiedProperties();
			});

			return p;
		}

		protected virtual void UpdateInspectorVisibility(ExposedParameter parameter, SerializedProperty settingsProperty,
			object value)
		{
			if (parameter.value.Equals(value))
			{
				return;
			}

			parameter.value = value;
			var graph = GetGraph(settingsProperty);
			graph.NotifyExposedParameterChanged(parameter);
		}
	}

	[CustomPropertyDrawer(typeof(BoolParameter.BoolSettings))]
	public class BoolSettingsDrawer : ExposedParameterSettingsDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
		{
			VisualElement settings = new VisualElement();
			settings.Bind(settingsProperty.serializedObject);

			settings.Add(CreateHideInInspectorField(settingsProperty));
			var parameter = GetParameter(settingsProperty);
			var settingsPropertyValue = settingsProperty.FindPropertyRelative(nameof(BoolParameter.BoolSettings.boolValue));
			settingsPropertyValue.boolValue = (bool)parameter.value;
			var settingsPropertyField = CreateSettingsField(settingsProperty, nameof(BoolParameter.BoolSettings.boolValue), "BoolValue");

			settingsPropertyField.RegisterValueChangeCallback(_ => UpdateInspectorVisibility(parameter, settingsPropertyValue, settingsPropertyValue.boolValue));

			settings.Add(settingsPropertyField);

			return settings;
		}
	}

	[CustomPropertyDrawer(typeof(IntParameter.IntSettings))]
	public class IntSettingsDrawer : ExposedParameterSettingsDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
		{
			VisualElement settings = new VisualElement();
			settings.Bind(settingsProperty.serializedObject);

			settings.Add(CreateHideInInspectorField(settingsProperty));
			var parameter = GetParameter(settingsProperty);
			var settingsPropertyValue = settingsProperty.FindPropertyRelative(nameof(IntParameter.IntSettings.intValue));
			settingsPropertyValue.intValue = (int)parameter.value;
			var settingsPropertyField = CreateSettingsField(settingsProperty, nameof(IntParameter.IntSettings.intValue), "IntValue");

			settingsPropertyField.RegisterValueChangeCallback(_ => UpdateInspectorVisibility(parameter, settingsPropertyValue,
				settingsPropertyValue.intValue));


			settings.Add(settingsPropertyField);

			return settings;
		}
	}

	[CustomPropertyDrawer(typeof(FloatParameter.FloatSettings))]
	public class FloatSettingsDrawer : ExposedParameterSettingsDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
		{
			VisualElement settings = new VisualElement();
			settings.Bind(settingsProperty.serializedObject);

			settings.Add(CreateHideInInspectorField(settingsProperty));
			var parameter = GetParameter(settingsProperty);
			var settingsPropertyValue = settingsProperty.FindPropertyRelative(nameof(FloatParameter.FloatSettings.floatValue));
			settingsPropertyValue.floatValue = (float)parameter.value;
			var settingsPropertyField = CreateSettingsField(settingsProperty, nameof(FloatParameter.FloatSettings.floatValue), "FloatValue");

			settingsPropertyField.RegisterValueChangeCallback(_ => UpdateInspectorVisibility(parameter, settingsPropertyValue,
				settingsPropertyValue.floatValue));


			settings.Add(settingsPropertyField);

			return settings;
		}
	}

	[CustomPropertyDrawer(typeof(LongParameter.LongSettings))]
	public class LongSettingsDrawer : ExposedParameterSettingsDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
		{
			VisualElement settings = new VisualElement();
			settings.Bind(settingsProperty.serializedObject);

			settings.Add(CreateHideInInspectorField(settingsProperty));
			var parameter = GetParameter(settingsProperty);
			var settingsPropertyValue = settingsProperty.FindPropertyRelative(nameof(LongParameter.LongSettings.longValue));
			settingsPropertyValue.longValue = (long)parameter.value;
			var settingsPropertyField = CreateSettingsField(settingsProperty, nameof(LongParameter.LongSettings.longValue), "LongValue");

			settingsPropertyField.RegisterValueChangeCallback(_ => UpdateInspectorVisibility(parameter, settingsPropertyValue,
				settingsPropertyValue.longValue));

			settings.Add(settingsPropertyField);

			return settings;
		}
	}

	[CustomPropertyDrawer(typeof(StringParameter.StringSettings))]
	public class StringSettingsDrawer : ExposedParameterSettingsDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
		{
			VisualElement settings = new VisualElement();
			settings.Bind(settingsProperty.serializedObject);

			settings.Add(CreateHideInInspectorField(settingsProperty));
			var parameter = GetParameter(settingsProperty);
			var settingsPropertyValue = settingsProperty.FindPropertyRelative(nameof(StringParameter.StringSettings.stringValue));
			settingsPropertyValue.stringValue = (string)parameter.value;
			var settingsPropertyField = CreateSettingsField(settingsProperty, nameof(StringParameter.StringSettings.stringValue), "StringValue");

			settingsPropertyField.RegisterValueChangeCallback(_ => UpdateInspectorVisibility(parameter, settingsPropertyValue,
				settingsPropertyValue.stringValue));

			settings.Add(settingsPropertyField);

			return settings;
		}
	}
}