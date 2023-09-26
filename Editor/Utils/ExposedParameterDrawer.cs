using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphProcessor
{
    [CustomPropertyDrawer(typeof(ExposedParameter))]
    public class ExposedParameterDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();

            container.Add(CreateValProperty(property));

            return container;
        }

        protected VisualElement CreateValProperty(SerializedProperty property, string displayName = null)
        {
            if (displayName == null)
                displayName = GetNameProperty(property).stringValue;

            var p = new PropertyField(GetValProperty(property), displayName);
            p.RegisterValueChangeCallback(e => { ApplyModifiedProperties(property); });

            return p;
        }

        protected SerializedProperty GetSettingsProperty(SerializedProperty property) => property.FindPropertyRelative(nameof(ExposedParameter.settings));
        protected SerializedProperty GetValProperty(SerializedProperty property) => property.FindPropertyRelative("val");
        protected SerializedProperty GetNameProperty(SerializedProperty property) => property.FindPropertyRelative(nameof(ExposedParameter.name));

        protected void ApplyModifiedProperties(SerializedProperty property)
        {
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }

    [CustomPropertyDrawer(typeof(BoolParameter))]
    public class BoolParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var val = GetValProperty(property);
            var name = GetNameProperty(property);

            var settings = GetSettingsProperty(property);
            var settingsPropertyValue = settings.FindPropertyRelative(nameof(BoolParameter.BoolSettings.boolValue));
            container.Add(new IMGUIContainer(() =>
            {
                var newValue = EditorGUILayout.Toggle(name.stringValue, val.boolValue);
                if (newValue != val.boolValue)
                {
                    val.boolValue = newValue;
                    settingsPropertyValue.boolValue = newValue;
                    ApplyModifiedProperties(property);
                    ApplyModifiedProperties(settingsPropertyValue);
                }
            }));

            return container;
        }
    }

    [CustomPropertyDrawer(typeof(IntParameter))]
    public class IntParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var val = GetValProperty(property);
            var name = GetNameProperty(property);

            var settings = GetSettingsProperty(property);
            var settingsPropertyValue = settings.FindPropertyRelative(nameof(IntParameter.IntSettings.intValue));
            container.Add(new IMGUIContainer(() =>
            {
                var newValue = EditorGUILayout.IntField(name.stringValue, val.intValue);
                if (newValue != val.intValue)
                {
                    val.intValue = newValue;
                    settingsPropertyValue.intValue = newValue;
                    ApplyModifiedProperties(property);
                    ApplyModifiedProperties(settingsPropertyValue);
                }
            }));

            return container;
        }
    }

    [CustomPropertyDrawer(typeof(FloatParameter))]
    public class FloatParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var val = GetValProperty(property);
            var name = GetNameProperty(property);

            var settings = GetSettingsProperty(property);
            var settingsPropertyValue = settings.FindPropertyRelative(nameof(FloatParameter.FloatSettings.floatValue));
            container.Add(new IMGUIContainer(() =>
            {
                var newValue = EditorGUILayout.FloatField(name.stringValue, val.floatValue);
                if (newValue != val.floatValue)
                {
                    val.floatValue = newValue;
                    settingsPropertyValue.floatValue = newValue;
                    ApplyModifiedProperties(property);
                    ApplyModifiedProperties(settingsPropertyValue);
                }
            }));

            return container;
        }
    }

    [CustomPropertyDrawer(typeof(LongParameter))]
    public class LongParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var val = GetValProperty(property);
            var name = GetNameProperty(property);

            var settings = GetSettingsProperty(property);
            var settingsPropertyValue = settings.FindPropertyRelative(nameof(LongParameter.LongSettings.longValue));
            container.Add(new IMGUIContainer(() =>
            {
                var newValue = EditorGUILayout.LongField(name.stringValue, val.longValue);
                if (newValue != val.longValue)
                {
                    val.longValue = newValue;
                    settingsPropertyValue.longValue = newValue;
                    ApplyModifiedProperties(property);
                    ApplyModifiedProperties(settingsPropertyValue);
                }
            }));

            return container;
        }
    }

    [CustomPropertyDrawer(typeof(StringParameter))]
    public class StringParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var val = GetValProperty(property);
            var name = GetNameProperty(property);

            var settings = GetSettingsProperty(property);
            var settingsPropertyValue = settings.FindPropertyRelative(nameof(StringParameter.StringSettings.stringValue));
            container.Add(new IMGUIContainer(() =>
            {
                var newValue = EditorGUILayout.TextField(name.stringValue, val.stringValue);
                if (newValue != val.stringValue)
                {
                    val.stringValue = newValue;
                    settingsPropertyValue.stringValue = newValue;
                    ApplyModifiedProperties(property);
                    ApplyModifiedProperties(settingsPropertyValue);
                }
            }));

            return container;
        }
    }

    [CustomPropertyDrawer(typeof(Vector2Parameter))]
    public class Vector2ParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var val = GetValProperty(property);
            var name = GetNameProperty(property);

            var settings = GetSettingsProperty(property);
            var mode = settings.FindPropertyRelative(nameof(Vector2Parameter.Vector2Settings.mode));
            var min = settings.FindPropertyRelative(nameof(Vector2Parameter.Vector2Settings.min));
            var max = settings.FindPropertyRelative(nameof(Vector2Parameter.Vector2Settings.max));
            container.Add(new IMGUIContainer(() => {
                EditorGUIUtility.labelWidth = 150;
                EditorGUI.BeginChangeCheck();
                if ((Vector2Parameter.Vector2Mode)mode.intValue == Vector2Parameter.Vector2Mode.MinMaxSlider)
                {
                    float x = val.vector2Value.x;
                    float y = val.vector2Value.y;
                    EditorGUILayout.MinMaxSlider(name.stringValue, ref x, ref y, min.floatValue, max.floatValue);
                    val.vector2Value = new Vector2(x, y);
                }
                else
                    val.vector2Value = EditorGUILayout.Vector2Field(name.stringValue, val.vector2Value);
                if (EditorGUI.EndChangeCheck())
                    ApplyModifiedProperties(property);
                EditorGUIUtility.labelWidth = 0;
            }));

            return container;
        }
    }

    [CustomPropertyDrawer(typeof(GradientParameter))]
    public class GradientParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var name = GetNameProperty(property);
            var settings = GetSettingsProperty(property);
            var mode = (GradientParameter.GradientColorMode)settings.FindPropertyRelative(nameof(GradientParameter.GradientSettings.mode)).intValue;
            if (mode == GradientParameter.GradientColorMode.HDR)
                return new PropertyField(property.FindPropertyRelative("hdrVal"), name.stringValue);
            else
                return new PropertyField(property.FindPropertyRelative("val"), name.stringValue);
        }
    }

    [CustomPropertyDrawer(typeof(ColorParameter))]
    public class ColorParameterDrawer : ExposedParameterDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var name = GetNameProperty(property);
            var settings = GetSettingsProperty(property);
            var val = GetValProperty(property);
            var mode = (ColorParameter.ColorMode)settings.FindPropertyRelative(nameof(ColorParameter.ColorSettings.mode)).intValue;

            var colorField = new ColorField(name.stringValue) { value = val.colorValue, hdr = mode == ColorParameter.ColorMode.HDR };
            colorField.RegisterValueChangedCallback(e => {
                val.colorValue = e.newValue;
                ApplyModifiedProperties(property);
            });
            return colorField;
        }
    }

    [CustomPropertyDrawer(typeof(ColorParameter.ColorSettings))]
    public class ExposedColorSettingsDrawer : ExposedParameterSettingsDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
        {
            VisualElement settings = new VisualElement();

            settings.Add(CreateHideInInspectorField(settingsProperty));
            settings.Add(CreateSettingsField(settingsProperty, nameof(ColorParameter.ColorSettings.mode), "Mode"));

            return settings;
        }
    }

    [CustomPropertyDrawer(typeof(Vector2Parameter.Vector2Settings))]
    public class Vector2SettingsDrawer : ExposedParameterSettingsDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
        {
            VisualElement settings = new VisualElement();

            settings.Add(CreateHideInInspectorField(settingsProperty));
            settings.Add(CreateSettingsField(settingsProperty, nameof(Vector2Parameter.Vector2Settings.mode), "Mode"));
            settings.Add(CreateSettingsField(settingsProperty, nameof(Vector2Parameter.Vector2Settings.min), "Min"));
            settings.Add(CreateSettingsField(settingsProperty, nameof(Vector2Parameter.Vector2Settings.max), "Max"));

            return settings;
        }
    }

    [CustomPropertyDrawer(typeof(GradientParameter.GradientSettings))]
    public class GradientSettingsDrawer : ExposedParameterSettingsDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty settingsProperty)
        {
            VisualElement settings = new VisualElement();

            settings.Add(CreateHideInInspectorField(settingsProperty));
            settings.Add(CreateSettingsField(settingsProperty, nameof(GradientParameter.GradientSettings.mode), "Mode"));

            return settings;
        }
    }
}