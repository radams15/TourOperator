using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class BookingRepository: Repository
{
    public BookingRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Booking', N'U') IS NULL BEGIN
            CREATE TABLE Booking (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                Username VARCHAR(32) REFERENCES Customer(Username),
                RoomId INTEGER REFERENCES Room(Id),
                TourId INTEGER REFERENCES Tour(Id),
                DateBooked DATE,
                DateFrom DATE,
                DateTo DATE,
                TotalCost INTEGER,
                PackageDiscount INTEGER,
                DepositPaid BOOLEAN,
                Due INTEGER
            );
        END;
        ")
    {
        InitTable();
    }
}