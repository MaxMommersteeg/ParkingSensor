using System;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using MediatR;

namespace App.Core.DomainServices
{
    public interface IMotionDetectionService :
        IRequestHandler<StartMotionDetection>,
        INotificationHandler<MotionDetected>,
        INotificationHandler<MotionStopped>,
        IDisposable
    {
        bool Started { get; }
    }
}
