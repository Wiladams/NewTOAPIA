namespace NewTOAPIA.Graphics.Processor
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class gpoutAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class gpinAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class gpinoutAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class gpuniformAttribute : Attribute
    {
    }
}