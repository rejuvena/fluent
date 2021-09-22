#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System;

namespace Fluent.Core.Cecil
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
    public class TypeMimickerAttribute : Attribute
    {
        public string Namespace { get; }

        public string Type { get; }

        public TypeMimickerAttribute(string @namespace, string type)
        {
            Namespace = @namespace;
            Type = type;
        }
    }
}