using ErrorOr;
using MediatR;
using Million.BackEnd.Domain.Common.Contracts.Persistence;
using Million.BackEnd.Domain.PropertyAggregate;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Images.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Owners.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.Entities.Traces.ValueObjects;
using Million.BackEnd.Domain.PropertyAggregate.ValueObjects;

namespace Million.BackEnd.Application.Seeders.Commands.Create
{
    public class CreateSeederCommandHandler(IUnitOfWork _unit) : IRequestHandler<CreateSeederCommand, ErrorOr<Success>>
    {
        private readonly IGenericRepository<Property> _property = _unit.GenericRepository<Property>();

        public async Task<ErrorOr<Success>> Handle(CreateSeederCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var seeder = new List<Property> {
                    Property.Create(PropertyId.CreateUnique(), "House 1", "Address 1", 100000, 2023)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 1", "Owner Address 1", "https://i.imgur.com/2Lv4K7u.jpeg", DateTime.UtcNow.AddYears(-35))
                        .SetImage(PropertyImageId.CreateUnique(), "https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?cs=srgb&dl=pexels-binyaminmellish-106399.jpg&fm=jpg")
                        .SetTrace(PropertyTraceId.CreateUnique(), "Comment", 110000, DateTime.UtcNow.AddMonths(-2)),
                    Property.Create(PropertyId.CreateUnique(), "House 2", "Address 2", 150000, 2020)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 2", "Owner Address 2", "https://i.imgur.com/l6fHBdF.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://media.istockphoto.com/id/1442148484/photo/3d-rendering-of-modern-suburban-house-in-the-garden.jpg?s=612x612&w=0&k=20&c=8Iu_h5cFOEnlPz4_n2nfSUtOyfM_a-hHx9rmlxMF2rI="),
                    Property.Create(PropertyId.CreateUnique(), "House 3", "Address 3", 900000, 2021)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 3", "Owner Address 3", "https://i.imgur.com/7862ujz.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://plus.unsplash.com/premium_photo-1689609950112-d66095626efb?fm=jpg&q=60&w=3000&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8aG91c2V8ZW58MHx8MHx8fDA%3D"),
                    Property.Create(PropertyId.CreateUnique(), "House 4", "Address 4", 1000000, 2003)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 4", "Owner Address 4", "https://i.imgur.com/HYUPOUX.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://media.istockphoto.com/id/1255835530/photo/modern-custom-suburban-home-exterior.jpg?s=612x612&w=0&k=20&c=0Dqjm3NunXjZtWVpsUvNKg2A4rK2gMvJ-827nb4AMU4="),
                    Property.Create(PropertyId.CreateUnique(), "House 5", "Address 5", 250000, 2021)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 5", "Owner Address 5", "https://i.imgur.com/KO2px5s.png", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://media.gettyimages.com/id/1453502204/es/foto/suburban-home-at-sunset-with-lawn-and-garden-visible.jpg?s=612x612&w=gi&k=20&c=6o90Sa9NJYflcWnxLI9VPvA_XbERxG9XDLC-1nm02do=")
                        .SetTrace(PropertyTraceId.CreateUnique(), "Comment", 210000, DateTime.UtcNow.AddMonths(-3)),
                    Property.Create(PropertyId.CreateUnique(), "House 6", "Address 6", 210000, 2003)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 6", "Owner Address 6", "https://i.imgur.com/9nUxWkm.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://img.freepik.com/premium-photo/average-residential-house-canada-cloudy-sky-background_769578-1249.jpg"),
                    Property.Create(PropertyId.CreateUnique(), "House 7", "Address 7", 300000, 2002)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 7", "Owner Address 7", "https://i.imgur.com/1LxAhVg.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://media.gettyimages.com/id/128502214/es/foto/classic-turn-of-the-century-american-house.jpg?s=612x612&w=gi&k=20&c=d-53uwDXwR5s2qtsmTAYOa150b40LXv9X4z9QJNcFUM="),
                    Property.Create(PropertyId.CreateUnique(), "House 8", "Address 8", 200000, 2000)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 8", "Owner Address 8", "https://i.imgur.com/NPBfm1E.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://t4.ftcdn.net/jpg/03/61/16/25/360_F_361162520_bgRKQmlB8lm2Z45NQ7GBaNT675tMOCGq.jpg"),
                    Property.Create(PropertyId.CreateUnique(), "House 9", "Address 9", 190000, 2024)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 9", "Owner Address 9", "https://i.imgur.com/2tcAFF6.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTWwZGWnpdqGGBcZm1jkl1v4KboQYjjNhb9Ag&s"),
                    Property.Create(PropertyId.CreateUnique(), "House 10", "Address 10", 180000, 2023)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 10", "Owner Address 10", "https://i.imgur.com/iXjog2d.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://images.unsplash.com/photo-1576941089067-2de3c901e126?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwcm9maWxlLWxpa2VkfDEzfHx8ZW58MHx8fHx8"),
                    Property.Create(PropertyId.CreateUnique(), "House 11", "Address 11", 100000, 2021)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 11", "Owner Address 11", "https://i.imgur.com/o9cFB2v.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://media.istockphoto.com/id/876864896/photo/luxurious-new-construction-home-in-bellevue-wa.jpg?s=612x612&w=0&k=20&c=Y5ZhzHSiyku1N4QGIF5FP4TkVhvEzTkEGSQ4FwZ7nlA="),
                    Property.Create(PropertyId.CreateUnique(), "House 12", "Address 12", 500000, 2010)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 12", "Owner Address 12", "https://i.imgur.com/5vhpnyD.png", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://images.pexels.com/photos/2581922/pexels-photo-2581922.jpeg?cs=srgb&dl=pexels-tomas-malik-793526-2581922.jpg&fm=jpg")
                        .SetTrace(PropertyTraceId.CreateUnique(), "Comment", 450000, DateTime.UtcNow.AddMonths(-30)),
                    Property.Create(PropertyId.CreateUnique(), "House 13", "Address 13", 130000, 2005)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 13", "Owner Address 13", "https://i.imgur.com/ym1aJFJ.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://t4.ftcdn.net/jpg/04/37/54/23/360_F_437542364_rUKRUDlOQ2ZmVrsijNFUfrhxZO7jyOFg.jpg"),
                    Property.Create(PropertyId.CreateUnique(), "House 14", "Address 14", 150000, 1999)
                        .SetOwner(PropertyOwnerId.CreateUnique(), "Owner 14", "Owner Address 14", "https://i.imgur.com/DH1ycze.jpeg", DateTime.UtcNow)
                        .SetImage(PropertyImageId.CreateUnique(), "https://i.pinimg.com/736x/79/81/24/7981245da4bf417b25c623c520356695.jpg"),
                };
                await _property.AddRange(seeder);
                return Result.Success;
            }
            catch (Exception e)
            {
                return Error.Failure(description: e.Message);
            }
        }
    }
}
