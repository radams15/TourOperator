using Microsoft.EntityFrameworkCore;

using TourOperator.Models.Entities;

namespace TourOperator.Contexts;

public static class DbModelCreator
{
    public static void CreateModels(ModelBuilder modelBuilder) {
		Role managerRole = new Role{
		    Id = 1, Name = RoleName.Manager
		};
		
		modelBuilder.Entity<Role>().HasData(managerRole);
		Role customerRole = new Role{
		    Id = 2, Name = RoleName.Customer
		};
		
		modelBuilder.Entity<Role>().HasData(customerRole);
		modelBuilder.Entity<Customer>().HasData(
		    new Customer{Id = 1, RoleId = customerRole.Id, Username = "rhys", FullName = "Rhys Adams", Password = "d74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1", PassportNo = "241221421241", PhoneNo = "634644638643"}
		);
		modelBuilder.Entity<Customer>().HasData(
		    new Customer{Id = 2, RoleId = managerRole.Id, Username = "manager", FullName = "Manager", Password = "d74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1", PassportNo = "123123123", PhoneNo = "123123123"}
		);
		
		modelBuilder.Entity<Tour>().HasData(
			new Tour{Id = 1, Name = "Real Britain", Description = "a tour", Length = 6, Price = 120000, Spaces = 30},
			new Tour{Id = 2, Name = "Britain and Ireland Explorer", Description = "a tour", Length = 16, Price = 200000, Spaces = 40},
			new Tour{Id = 3, Name = "Best of Britain", Description = "a tour", Length = 12, Price = 290000, Spaces = 30}
		);
		
		Operator Kings = new Operator{ Id = 1, Name = "Kings" };
		Operator Hilton = new Operator{ Id = 2, Name = "Hilton" };
		Operator Marriott = new Operator{ Id = 3, Name = "Marriott" };
		Operator Independent = new Operator{ Id = 4, Name = "Independent" };
		Operator Travelodge = new Operator{ Id = 5, Name = "Travelodge" };
		Operator Leonardo = new Operator{ Id = 6, Name = "Leonardo" };
		
		
		Hotel KingsHotelBrighton = new Hotel{Id = 1, Name = "Kings Hotel Brighton", Description = "a hotel", OperatorId = Kings.Id};
		Hotel HiltonLondonHotel = new Hotel{Id = 2, Name = "Hilton London Hotel", Description = "a hotel", OperatorId = Hilton.Id};
		Hotel LondonMarriottHotel = new Hotel{Id = 3, Name = "London Marriott Hotel", Description = "a hotel", OperatorId = Marriott.Id};
		Hotel NevisBankInnFortWilliam = new Hotel{Id = 4, Name = "Nevis Bank Inn, Fort William", Description = "a hotel", OperatorId = Independent.Id};
		Hotel TravelodgeBrightonSeafront = new Hotel{Id = 5, Name = "Travelodge Brighton Seafront", Description = "a hotel", OperatorId = Travelodge.Id};
		Hotel LeonardoHotelBrighton = new Hotel{Id = 6, Name = "Leonardo Hotel Brighton", Description = "a hotel", OperatorId = Leonardo.Id};
		
		
		modelBuilder.Entity<Operator>().HasData(
			Kings,
			Hilton,
			Marriott,
			Independent,
			Travelodge,
			Leonardo
		);
		
		modelBuilder.Entity<Hotel>().HasData(
			KingsHotelBrighton,
			HiltonLondonHotel,
			LondonMarriottHotel,
			NevisBankInnFortWilliam,
			TravelodgeBrightonSeafront,
			LeonardoHotelBrighton
		);
		
		modelBuilder.Entity<Room>().HasData(
		
			new Room{Id = 1, Name = "Single Bed", HotelId = KingsHotelBrighton.Id, Price = 18000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 2, Name = "Double Bed", HotelId = KingsHotelBrighton.Id, Price = 40000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 3, Name = "Family Suite", HotelId = KingsHotelBrighton.Id, Price = 52000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 4, Name = "Single Bed", HotelId = HiltonLondonHotel.Id, Price = 37500, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 5, Name = "Double Bed", HotelId = HiltonLondonHotel.Id, Price = 77500, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 6, Name = "Family Suite", HotelId = HiltonLondonHotel.Id, Price = 95000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 7, Name = "Single Bed", HotelId = LondonMarriottHotel.Id, Price = 30000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 8, Name = "Double Bed", HotelId = LondonMarriottHotel.Id, Price = 50000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 9, Name = "Family Suite", HotelId = LondonMarriottHotel.Id, Price = 90000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 10, Name = "Single Bed", HotelId = NevisBankInnFortWilliam.Id, Price = 9000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 11, Name = "Double Bed", HotelId = NevisBankInnFortWilliam.Id, Price = 10000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 12, Name = "Family Suite", HotelId = NevisBankInnFortWilliam.Id, Price = 15500, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 13, Name = "Single Bed", HotelId = TravelodgeBrightonSeafront.Id, Price = 8000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 14, Name = "Double Bed", HotelId = TravelodgeBrightonSeafront.Id, Price = 12000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 15, Name = "Family Suite", HotelId = TravelodgeBrightonSeafront.Id, Price = 15000, Spaces = 20, PackageDiscount = 40},
			new Room{Id = 16, Name = "Single Bed", HotelId = LeonardoHotelBrighton.Id, Price = 18000, Spaces = 20, PackageDiscount = 10},
			new Room{Id = 17, Name = "Double Bed", HotelId = LeonardoHotelBrighton.Id, Price = 40000, Spaces = 20, PackageDiscount = 20},
			new Room{Id = 18, Name = "Family Suite", HotelId = LeonardoHotelBrighton.Id, Price = 52000, Spaces = 20, PackageDiscount = 40}
		);
    }
}
