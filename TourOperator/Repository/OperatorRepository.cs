using System.Data;
using Microsoft.Data.SqlClient;

namespace TourOperator.Repository;

public class OperatorRepository: Repository
{
    public OperatorRepository(string? connectionString)
        : base(connectionString, @"
        IF OBJECT_ID(N'dbo.Operator', N'U') IS NULL BEGIN
            CREATE TABLE Operator (
                Id INTEGER NOT NULL IDENTITY(1,1) PRIMARY KEY,
                Name VARCHAR(32)
            );

            INSERT INTO Operator (Name) VALUES ('Hilton');
            INSERT INTO Operator (Name) VALUES ('Marriott');
            INSERT INTO Operator (Name) VALUES ('Travelodge');
            INSERT INTO Operator (Name) VALUES ('Kings');
            INSERT INTO Operator (Name) VALUES ('Leonardo');
        END;
        ")
    {
        InitTable();
    }
}