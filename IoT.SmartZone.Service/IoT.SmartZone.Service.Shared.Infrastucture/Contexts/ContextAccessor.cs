using IoT.SmartZone.Service.Shared.Abstractions.Contexts;

namespace IoT.SmartZone.Service.Shared.Infrastucture.Contexts;

public sealed class ContextAccessor
{
    private static readonly AsyncLocal<ContextHolder> _holder = new();

    public IContext Context
    {
        get => _holder.Value?.Context;
        set
        {
            var holder = _holder.Value;
            if (holder != null)
            {
                holder.Context = null;
            }

            if (value != null)
            {
                _holder.Value = new ContextHolder { Context = value };
            }
        }
    }

    private class ContextHolder
    {
        public IContext Context;
    }
}