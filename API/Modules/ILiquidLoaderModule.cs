#region License
// Copyright (C) 2021 Tomat and Contributors, MIT License
#endregion

using System.Collections.Generic;

namespace Fluent.API.Modules
{
    public interface ILiquidLoaderModule
    {
        void Load();

        void PostSetup(Dictionary<byte, ModLiquid> modLiquids);

        void Unload();
    }
}