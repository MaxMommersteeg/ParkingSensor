﻿using System;
using App.Core.Messages.Commands;
using MediatR;

namespace App.Core.DomainServices
{
    public interface IBuzzerService :
        IRequestHandler<StartSoundEffect>,
        IRequestHandler<StopSoundEffect>,
        IDisposable
    {
    }
}
