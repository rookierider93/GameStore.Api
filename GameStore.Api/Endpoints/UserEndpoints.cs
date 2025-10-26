using System;
using GameStore.Api.Data;

namespace GameStore.Api.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users");

        group.Map("/", async (GameStoreContext dbContext) =>
            await dbContext.Users.ToListAsync()
        );
        // Define user-related endpoints here

        return group;
    }
}
