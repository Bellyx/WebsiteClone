/*

/// สินค้ารายการของ
CREATE TABLE Product(
    ProductId VARCHAR(10) PRIMARY KEY, --ID ของสินค้า
    Product_Name VARCHAR(255), --ชื่อสินค้า
    Description TEXT, --รายละเอียดสินค้า
    PricePerDay DECIMAL(10, 2), --ราคาเช่าต่อวัน(ตัวเลขทศนิยม 2 ตำแหน่ง)
    PointsRequired INT-- จำนวน point ที่ต้องใช้ในการเช่า
);

/// สำหรับวันหมดอายุ
CREATE TABLE Rental (
    Identifier VARCHAR(50) PRIMARY KEY,    -- รหัสการเช่าเป็น Primary Key
    RentalId VARCHAR(10),                  -- ID ของการเช่า
    ProductId INT,                         -- รหัสสินค้าที่เช่า
    StartDate DATETIME,                    -- วันที่เริ่มเช่า
    ExpiryDate DATETIME,                   -- วันที่หมดอายุ
    DaysRented INT,                        -- จำนวนวันที่เช่า
    PointsUsed INT,                        -- จำนวน point ที่ใช้
    IsExpired BIT,                         -- สถานะการหมดอายุ (ใช้ BIT เพื่อเก็บค่า TRUE/FALSE)
);
*/
