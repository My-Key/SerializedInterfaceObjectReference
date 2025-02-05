using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class SerializedInterfaceObjectReferenceDrawer<T, TInterface, TObject> : OdinValueDrawer<T>
	where T : SerializedInterfaceObjectReference<TInterface, TObject>
	where TInterface : class
	where TObject : Object
{
	private InspectorProperty m_object;
	private InspectorProperty m_value;
    
	protected override void Initialize()
	{
		base.Initialize();
        
		m_object = Property.Children["m_object"];
		m_value = Property.Children["m_value"];
	}

	protected override void DrawPropertyLayout(GUIContent label)
	{
		EditorGUI.BeginChangeCheck();
		
		var objectValue = SirenixEditorFields.UnityObjectField(label,
			(TObject)m_object.ValueEntry.WeakSmartValue, typeof(TInterface), true);
		
		if (EditorGUI.EndChangeCheck())
		{
			m_object.ValueEntry.WeakSmartValue = objectValue;

			m_value.ValueEntry.WeakSmartValue = m_object.ValueEntry.WeakSmartValue as TInterface;
		}
	}
}