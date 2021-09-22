using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fluent.API;
using Fluent.Core.Cecil;
using Fluent.Core.Cecil.Tests;
using JetBrains.Annotations;
using TomatoLib;

namespace Fluent
{
    [UsedImplicitly]
    public class Fluent : TomatoMod
    {
        public override void Load()
        {
            base.Load();

            TypeMimicker.MimickingTypes = TypeMimicker.FindMimickingTypes(Code).ToList();
            IEnumerable<MethodInfo> methods = TypeMimicker.FindMethodsWithMimics(Code);
            ModLogger.Debug($"Found types to mimic: {string.Join(", ", TypeMimicker.MimickingTypes)}");

            foreach (MethodInfo method in methods)
            {
                ModLogger.Debug($"Mimicking types in: {method.DeclaringType?.FullName}::{method.Name}");
                TypeMimicker.MimicTypesIn(method);
            }
        }

        public override void PostAddRecipes()
        {
            base.PostAddRecipes();

            ModLogger.Debug($"Test reassurance: {TestMimic.MimicTest()}");

            LiquidLoader.Instance.PostSetup();
        }
    }
}
