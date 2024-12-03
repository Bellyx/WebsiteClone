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
// สำหรับ หน้า Login member

//CREATE TABLE members(
//    member_id INT IDENTITY(1, 1) PRIMARY KEY, --ใช้ IDENTITY แทน AUTO_INCREMENT
//	indentifier VARCHAR(50),
//    UID VARCHAR(50),
//    username VARCHAR(50) NOT NULL UNIQUE,
//    password_hash VARCHAR(255) NOT NULL,
//    email VARCHAR(100) NOT NULL UNIQUE,
//    full_name VARCHAR(100) NOT NULL,
//    created_at DATETIME DEFAULT GETDATE(), --ใช้ GETDATE() สำหรับวันที่ปัจจุบัน
//    updated_at DATETIME DEFAULT GETDATE()-- ใช้ GETDATE() สำหรับวันที่ปัจจุบัน
//);

//Log login ไว้สำหรับดูการ เข้าสู่ระบบ
//CREATE TABLE login_history(
//    login_id INT IDENTITY(1, 1) PRIMARY KEY, --รหัสการเข้าสู่ระบบ
//    member_id INT NOT NULL, --รหัสผู้ใช้(เชื่อมโยงกับตาราง members)
//    login_timestamp DATETIME DEFAULT GETDATE(), --เวลาที่เข้าสู่ระบบ(ใช้ GETDATE() สำหรับเวลา)
//    ip_address VARCHAR(45), --ที่อยู่ IP ของผู้ใช้
//    FOREIGN KEY(member_id) REFERENCES members(member_id)-- การเชื่อมโยงกับตาราง members
//);

// Reset password
//CREATE TABLE password_resets(
//    reset_id INT IDENTITY(1, 1) PRIMARY KEY, --รหัสการรีเซ็ตรหัสผ่าน(เพิ่มอัตโนมัติ)
//    member_id INT NOT NULL, --รหัสผู้ใช้(เชื่อมโยงกับตาราง users)
//    reset_token VARCHAR(255) NOT NULL, --โทเค็นสำหรับการรีเซ็ตรหัสผ่าน
//    created_at DATETIME DEFAULT GETDATE(), --เวลาที่ขอรีเซ็ตรหัสผ่าน(ใช้ GETDATE() สำหรับเวลา)
//    FOREIGN KEY(member_id) REFERENCES members(member_id)-- การเชื่อมโยงกับตาราง users
//);

/// Discord permiss
//CREATE TABLE Discordpermiss(
//    UserId BIGINT PRIMARY KEY,
//    Username NVARCHAR(255),
//    Role NVARCHAR(50)
//);