using Microsoft.EntityFrameworkCore;

using TourOperator.Models.Entities;

namespace TourOperator.Contexts;

public static class DbModelCreator
{
    public static void CreateModels(ModelBuilder modelBuilder) {
		modelBuilder.Entity<Tour>().HasData(
			new Tour{Id = 1, Name = "Best of Britain", Description = "a tour", Length = 12, Price = 290000, Spaces = 30},
			new Tour{Id = 2, Name = "Real Britain", Description = "a tour", Length = 6, Price = 120000, Spaces = 30},
			new Tour{Id = 3, Name = "Britain and Ireland Explorer", Description = "a tour", Length = 16, Price = 200000, Spaces = 40}
		);
		
		Operator Marriott = new Operator{ Id = 1, Name = "Marriott" };
		Operator Leonardo = new Operator{ Id = 2, Name = "Leonardo" };
		Operator Kings = new Operator{ Id = 3, Name = "Kings" };
		Operator Travelodge = new Operator{ Id = 4, Name = "Travelodge" };
		Operator Hilton = new Operator{ Id = 5, Name = "Hilton" };
		Operator Independent = new Operator{ Id = 6, Name = "Independent" };
		
		
		Hotel LondonMarriottHotel = new Hotel{Id = 1, Name = "London Marriott Hotel", Description = "a hotel", OperatorId = Marriott.Id};
		Hotel LeonardoHotelBrighton = new Hotel{Id = 2, Name = "Leonardo Hotel Brighton", Description = "a hotel", OperatorId = Leonardo.Id};
		Hotel KingsHotelBrighton = new Hotel{Id = 3, Name = "Kings Hotel Brighton", Description = "a hotel", OperatorId = Kings.Id};
		Hotel TravelodgeBrightonSeafront = new Hotel{Id = 4, Name = "Travelodge Brighton Seafront", Description = "a hotel", OperatorId = Travelodge.Id};
		Hotel HiltonLondonHotel = new Hotel{Id = 5, Name = "Hilton London Hotel", Description = "a hotel", OperatorId = Hilton.Id};
		Hotel NevisBankInnFortWilliam = new Hotel{Id = 6, Name = "Nevis Bank Inn, Fort William", Description = "a hotel", OperatorId = Independent.Id};
		
		
		modelBuilder.Entity<Operator>().HasData(
			Marriott,
			Leonardo,
			Kings,
			Travelodge,
			Hilton,
			Independent
		);
		
		modelBuilder.Entity<Hotel>().HasData(
			LondonMarriottHotel,
			LeonardoHotelBrighton,
			KingsHotelBrighton,
			TravelodgeBrightonSeafront,
			HiltonLondonHotel,
			NevisBankInnFortWilliam
		);
		
		modelBuilder.Entity<Room>().HasData(
		
			new Room{Id = 1, Name = "Single Bed", HotelId = LondonMarriottHotel.Id, Price = 30000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 2, Name = "Double Bed", HotelId = LondonMarriottHotel.Id, Price = 50000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 3, Name = "Family Suite", HotelId = LondonMarriottHotel.Id, Price = 90000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 4, Name = "Single Bed", HotelId = LeonardoHotelBrighton.Id, Price = 18000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 5, Name = "Double Bed", HotelId = LeonardoHotelBrighton.Id, Price = 40000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 6, Name = "Family Suite", HotelId = LeonardoHotelBrighton.Id, Price = 52000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 7, Name = "Single Bed", HotelId = KingsHotelBrighton.Id, Price = 18000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 8, Name = "Double Bed", HotelId = KingsHotelBrighton.Id, Price = 40000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 9, Name = "Family Suite", HotelId = KingsHotelBrighton.Id, Price = 52000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 10, Name = "Single Bed", HotelId = TravelodgeBrightonSeafront.Id, Price = 8000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 11, Name = "Double Bed", HotelId = TravelodgeBrightonSeafront.Id, Price = 12000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 12, Name = "Family Suite", HotelId = TravelodgeBrightonSeafront.Id, Price = 15000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 13, Name = "Single Bed", HotelId = HiltonLondonHotel.Id, Price = 37500, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 14, Name = "Double Bed", HotelId = HiltonLondonHotel.Id, Price = 77500, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 15, Name = "Family Suite", HotelId = HiltonLondonHotel.Id, Price = 95000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 16, Name = "Single Bed", HotelId = NevisBankInnFortWilliam.Id, Price = 9000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 17, Name = "Double Bed", HotelId = NevisBankInnFortWilliam.Id, Price = 10000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 18, Name = "Family Suite", HotelId = NevisBankInnFortWilliam.Id, Price = 15500, Spaces = 20, PackageDiscount = 40}
		);
    }
}
