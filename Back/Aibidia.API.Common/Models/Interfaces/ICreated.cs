using System;

namespace Aibidia.API.Common.Models.Interfaces
{
    interface ICreatedAt
    {
        DateTimeOffset CreatedAt { get; set; }
    }

    interface ICreatedBy : ICreatedAt
    {
        string CreatedBy { get; set; }
    }
}
