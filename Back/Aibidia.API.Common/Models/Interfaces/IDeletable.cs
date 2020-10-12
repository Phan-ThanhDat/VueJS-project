using System;

namespace Aibidia.API.Common.Models.Interfaces
{
    public interface IDeletable
    {
        bool Deleted { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
    }
}
