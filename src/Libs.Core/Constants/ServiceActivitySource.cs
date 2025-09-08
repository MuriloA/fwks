using System.Diagnostics;
using System.Threading;
using FwksLabs.Libs.Core.Exceptions;
using Humanizer;

namespace FwksLabs.Libs.Core.Constants;

public static class ServiceActivitySource
{
    private static ActivitySource? _activitySource;
    private static readonly Lock Lock = new();

    public static ActivitySource Instance
    {
        get { return _activitySource ?? throw new NotInitializedException(nameof(ActivitySource)); }
    }

    public static void Initialize(string serviceName)
    {
        if (_activitySource is not null)
            return;

        lock (Lock)
        {
            if (_activitySource is not null)
                return;

            _activitySource = new ActivitySource(serviceName.Kebaberize());
        }
    }
}