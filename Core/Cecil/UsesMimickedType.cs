#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System;

namespace Fluent.Core.Cecil
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UsesMimickedType : Attribute
    {
    }
}