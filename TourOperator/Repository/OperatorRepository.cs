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
        END;
        ")
    {
        
    }
}