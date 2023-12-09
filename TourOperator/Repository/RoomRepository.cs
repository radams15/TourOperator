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

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Hilton London Hotel'), 'SINGLE/DOUBLE/TRIPLE', 375, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Hilton London Hotel'), 'SINGLE/DOUBLE/TRIPLE', 775, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Hilton London Hotel'), 'SINGLE/DOUBLE/TRIPLE', 950, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'London Marriott Hotel'), 'SINGLE/DOUBLE/TRIPLE', 300, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'London Marriott Hotel'), 'SINGLE/DOUBLE/TRIPLE', 500, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'London Marriott Hotel'), 'SINGLE/DOUBLE/TRIPLE', 900, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Travelodge Brighton Seafront'), 'SINGLE/DOUBLE/TRIPLE', 80, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Travelodge Brighton Seafront'), 'SINGLE/DOUBLE/TRIPLE', 120, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Travelodge Brighton Seafront'), 'SINGLE/DOUBLE/TRIPLE', 150, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Kings Hotel Brighton'), 'SINGLE/DOUBLE/TRIPLE', 180, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Kings Hotel Brighton'), 'SINGLE/DOUBLE/TRIPLE', 400, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Kings Hotel Brighton'), 'SINGLE/DOUBLE/TRIPLE', 520, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Leonardo Hotel Brighton'), 'SINGLE/DOUBLE/TRIPLE', 180, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Leonardo Hotel Brighton'), 'SINGLE/DOUBLE/TRIPLE', 400, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Leonardo Hotel Brighton'), 'SINGLE/DOUBLE/TRIPLE', 520, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Nevis Bank Inn, Fort William'), 'SINGLE/DOUBLE/TRIPLE', 90, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Nevis Bank Inn, Fort William'), 'SINGLE/DOUBLE/TRIPLE', 100, 20);

            INSERT INTO Room (HotelId, Name, Price, Spaces) VALUES ((SELECT Id FROM Hotel WHERE Name = 'Nevis Bank Inn, Fort William'), 'SINGLE/DOUBLE/TRIPLE', 155, 20);
        END;
        ")
    { 
        InitTable();   
    }
}