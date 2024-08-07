use HRMS_Test
Go
CREATE TABLE Tbl_Countries (
    Id int PRIMARY KEY,
    CountryCode varchar(255),
    CountryName varchar(255),
    Region varchar(100),
    IsActive bit,
    LangCode varchar(50),
    Zone varchar(50),
    GoldBv decimal(18,2) NULL,
    BronzeBv decimal(18,2) NULL,
    PromoMco decimal(18,2) NULL,
    Currency varchar(50),
    CurrencyCode varchar(50),
    CurrencySymbol varchar(50),
    IsDefault bit,
    Nationality varchar(100)
);
