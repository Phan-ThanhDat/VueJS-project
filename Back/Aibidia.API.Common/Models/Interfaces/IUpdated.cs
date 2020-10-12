using System;

namespace Aibidia.API.Common.Models.Interfaces
{
    interface IUpdated
    {
        DateTimeOffset? LastUpdated { get; set; }
    }

    interface IUpdatedBy : IUpdated
    {
        string LastUpdatedBy { get; set; }
    }
}
