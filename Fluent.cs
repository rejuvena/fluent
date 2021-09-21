using Fluent.API;
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

            LiquidLoader.Load();
        }

        public override void Unload()
        {
            base.Unload();

            LiquidLoader.Unload();
        }
    }
}
