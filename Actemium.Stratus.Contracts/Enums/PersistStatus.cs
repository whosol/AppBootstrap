﻿namespace Actemium.Stratus.Contracts.Enums
{
    public enum PersistStatus
    {
        OK,
        Duplicate,
        InvalidFormat,
        DatabaseError,
        CorruptData,
        NoPersistanceMethod,
        Timeout,
        NoConnection,
        WebApiReject
    }
}