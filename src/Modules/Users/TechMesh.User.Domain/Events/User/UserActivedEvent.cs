﻿namespace TechMesh.User.Domain.Events.User;

public class UserActivedEvent : Event
{
    public EUserStatus Status { get; private set; }

    public UserActivedEvent(Guid aggregateId, EUserStatus status) : base(aggregateId)
        => Status = status;
}