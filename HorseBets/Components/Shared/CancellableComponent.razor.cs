using Microsoft.AspNetCore.Components;

namespace HorseBets.Components.Shared
{
    public partial class CancellableComponent : ComponentBase, IDisposable
    {
        private CancellationTokenSource? cancellationTokenSource;

        protected CancellationToken CancellationToken => (cancellationTokenSource ??= new()).Token;

        public virtual void Dispose()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }
    }
}
