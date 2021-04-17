namespace Unistoty.SOVariables
{
    /// <summary>
    /// Start value type for a scriptable object that holds an integral or floating point value.
    /// <para>Default - start value will be set to the default value of the type that the variable represents.</para>
    /// <para>Min - start value will be set to the minimum value of the variable.</para>
    /// <para>Max - start value will be set to the maximum value of the variable.</para>
    /// <para>Custom - start value will be set to a custom value assigned in the inspector.</para>
    /// <para>EditorValue - start value will be set to whatever the value in the editor was before building.</para>
    /// </summary>    
    public enum VariableStartValueType
    {
        Default = 0,
        Min = 1,
        Max = 2,
        Custom = 3,
        EditorValue = 4,
    }
}