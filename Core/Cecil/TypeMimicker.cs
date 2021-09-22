﻿#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ModLoader;
using TomatoLib.Common.Utilities.Extensions;
using TypeAttributes = Mono.Cecil.TypeAttributes;

namespace Fluent.Core.Cecil
{
    public static class TypeMimicker
    {
        public static List<Type> MimickingTypes = new();

        public static IEnumerable<MethodInfo> FindMethodsWithMimics(Assembly assembly) =>
            from type in assembly.GetTypes()
            from method in type.GetMethods()
            where method.GetCustomAttribute<UsesMimickedType>() != null
            select method;

        public static IEnumerable<Type> FindMimickingTypes(Assembly assembly) => assembly.GetTypes()
            .Where(x => x.GetCustomAttribute<TypeMimickerAttribute>() != null);

        public static void MimicTypesIn(MethodInfo methodInfo)
        {
            ModContent.GetInstance<Fluent>().CreateEdit(methodInfo, 
                typeof(TypeMimicker), nameof(ReplaceMimickedTypes));
        }

        private static void ReplaceMimickedTypes(ILContext il)
        {
            ILCursor c = new(il);

            foreach (Instruction instr in c.Instrs)
            {
                if (instr.Operand is null)
                    continue;
                
                foreach (Type mimicType in MimickingTypes)
                {
                    if (instr.Operand.GetType().GetCachedField("DeclaringType") is not null)
                        instr.Operand.SetFieldValue("DeclaringType", ToDefinition(il, mimicType));
                    else if (instr.Operand.GetType().GetCachedProperty("DeclaringType") is not null)
                        instr.Operand.GetType().GetCachedProperty("DeclaringType").SetValue(instr.Operand, ToDefinition(il, mimicType));

                    //switch (instr.Operand)
                    //{
                        /*case MethodDefinition member:
                            if (AreRefsEqual(member.DeclaringType, mimicType))
                                member.DeclaringType = ToDefinition(mimicType);

                            // Not needed... yet.
                            // il.Import / il.Module.ImportReference - our lord and savior lol ecs dee
                            // if (AreRefsEqual(method.ReturnType, mimicType))
                            break;*/
                        
                        /*case TypeDefinition type:
                            if (AreRefsEqual(type, mimicType))
                                instr.Operand = ToDefinition(mimicType);
                            break;

                        // TODO: Is this needed?
                        case FieldDefinition field:
                            if (AreRefsEqual(field.DeclaringType, mimicType))
                                field.DeclaringType = ToDefinition(mimicType);
                            break;*/

                        // Properties and events internally used methods.
                        // Parameters can't be modified at runtime in an already-loaded assembly.
                        //case PropertyDefinition property:
                        //    break;

                        //case EventDefinition evt:
                        //    break;

                        //case ParameterDefinition param:
                        //    break;
                    //}
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool AreRefsEqual(MemberReference definition, Type loaded)
        {
            (string ns, string name) = GetMimicData(loaded);

            return definition.Name == ns && definition.Name == name;
        }

        public static TypeReference ToDefinition(ILContext context, Type type)
        {
            (string ns, string name) = GetMimicData(type);

            return context.Import(Type.GetType(ns + '.' + name));
        }

        public static (string ns, string name) GetMimicData(Type type)
        {
            TypeMimickerAttribute mimic = type.GetCustomAttribute<TypeMimickerAttribute>();

            return mimic is null ? ("", "") : (mimic.Namespace, mimic.Type);
        }
    }
}