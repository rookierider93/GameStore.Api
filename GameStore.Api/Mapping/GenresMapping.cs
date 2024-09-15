using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GenresMapping
{
    public static GenresDto ToDto(this Genre genre)
    {
        return new(
                genre.Id,
                genre.Name
            );
    }
}
