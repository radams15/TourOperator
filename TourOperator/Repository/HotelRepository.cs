using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class HotelRepository: Repository
{
    public HotelRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Hotel', N'U') IS NULL BEGIN
            CREATE TABLE Hotel (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                OperatorId INTEGER REFERENCES Operator(Id),
                Name VARCHAR(32),
                Description VARCHAR(256)
            );

            INSERT INTO Hotel (OperatorId, Name, Description) VALUES (
                (SELECT Id FROM Operator WHERE Name = 'Hilton'), 'Hilton London Hotel', ''
            );
            INSERT INTO Hotel (OperatorId, Name, Description) VALUES (
                (SELECT Id FROM Operator WHERE Name = 'Marriott'), 'London Marriott Hotel', ''
            );
            INSERT INTO Hotel (OperatorId, Name, Description) VALUES (
                (SELECT Id FROM Operator WHERE Name = 'Travelodge'), 'Travelodge Brighton Seafront', ''
            );
            INSERT INTO Hotel (OperatorId, Name, Description) VALUES (
                (SELECT Id FROM Operator WHERE Name = 'Kings'), 'Kings Hotel Brighton', ''
            );
            INSERT INTO Hotel (OperatorId, Name, Description) VALUES (
                (SELECT Id FROM Operator WHERE Name = 'Leonardo'), 'Leonardo Hotel Brighton', ''
            );
            INSERT INTO Hotel (OperatorId, Name, Description) VALUES (
                NULL, 'Nevis Bank Inn, Fort William', ''
            );
        END;
        ")
    {
        InitTable();
    }
}