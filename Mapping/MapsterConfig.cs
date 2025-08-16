// Mapster configuration for the project
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using Mapster;
using MapsterMapper;

namespace ApiEcommerce.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            // Category
            TypeAdapterConfig<Category, CategoryDto>.NewConfig();
            TypeAdapterConfig<CreateCategoryDto, Category>.NewConfig();
            // Product
            TypeAdapterConfig<Product, ProductDto>.NewConfig();
            TypeAdapterConfig<CreateProductDto, Product>.NewConfig();
            TypeAdapterConfig<UpdateProductDto, Product>.NewConfig();
            // User
            TypeAdapterConfig<User, UserDto>.NewConfig();
            TypeAdapterConfig<CreateUserDto, User>.NewConfig();
            TypeAdapterConfig<User, UserDataDto>.NewConfig();
            TypeAdapterConfig<ApplicationUser, UserDto>.NewConfig();
            TypeAdapterConfig<UserRegisterDto, ApplicationUser>.NewConfig();
            TypeAdapterConfig<UserLoginDto, ApplicationUser>.NewConfig();
            // Add more mappings as needed
        }
    }
}
