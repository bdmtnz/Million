using Mapster;
using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces;

namespace Million.BackEnd.Contracts.Properties
{
    public class PropertyMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Property, PropertyFilteredResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.CreatedOnUtc, src => src.CreatedOnUtc)
                .Map(dest => dest.Image, src => src.Image == null ? null : src.Image.File)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Price, src => src.Price);

            config.NewConfig<Property, PropertyResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.CreatedOnUtc, src => src.CreatedOnUtc)
                .Map(dest => dest.Image, src => src.Image == null ? null : src.Image.File)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Price, src => src.Price);

            config.NewConfig<PropertyOwner, PropertyOwnerResponse>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Photo, src => src.Photo)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.BornOnUtc, src => src.BornOnUtc);

            config.NewConfig<PropertyTrace, PropertyTraceResponse>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Value, src => src.Value)
                .Map(dest => dest.SaledOnUtc, src => src.SaledOnUtc);
        }
    }
}
