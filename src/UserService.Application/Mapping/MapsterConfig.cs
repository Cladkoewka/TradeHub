using Mapster;
using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<User, UserGetDto>.NewConfig();

        TypeAdapterConfig<UserCreateDto, User>.NewConfig()
            .Ignore(dest => dest.Id) 
            .Ignore(dest => dest.PasswordHash) 
            .Ignore(dest => dest.RoleId) 
            .Ignore(dest => dest.Role); 

        TypeAdapterConfig<UserUpdateDto, User>.NewConfig()
            .Ignore(dest => dest.Id) 
            .Ignore(dest => dest.PasswordHash) 
            .Ignore(dest => dest.RoleId) 
            .Ignore(dest => dest.Role) 
            .IgnoreNullValues(true); 

        TypeAdapterConfig<Role, string>.NewConfig().MapWith(src => src.Name);
    }
}