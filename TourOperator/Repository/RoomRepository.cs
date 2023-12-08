using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class RoomRepository: Repository
{
    public RoomRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Room', N'U') IS NULL BEGIN
            CREATE TABLE Room (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                HotelId INTEGER REFERENCES Hotel(Id),
                Name VARCHAR(32),
                Price INTEGER,
                Spaces INTEGER
            );

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Hilton London Hotel'), 375, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Hilton London Hotel'), 775, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Hilton London Hotel'), 950, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'London Marriott Hotel'), 300, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'London Marriott Hotel'), 500, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'London Marriott Hotel'), 900, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Travelodge Brighton Seafront'), 80, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Travelodge Brighton Seafront'), 120, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Travelodge Brighton Seafront'), 150, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Kings Hotel Brighton'), 180, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Kings Hotel Brighton'), 400, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Kings Hotel Brighton'), 520, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Leonardo Hotel Brighton'), 180, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Leonardo Hotel Brighton'), 400, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Leonardo Hotel Brighton'), 520, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Nevis Bank Inn, Fort William'), 90, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Nevis Bank Inn, Fort William'), 100, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Nevis Bank Inn, Fort William'), 155, 20);
        END;
        ")
    { 
        InitTable();   
    }
}