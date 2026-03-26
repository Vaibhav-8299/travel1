-- ===============================
-- CREATE DATABASE
-- ===============================

CREATE DATABASE TravelManagement;
USE TravelManagement;


-- ===============================
-- ROLES TABLE
-- ===============================

CREATE TABLE Roles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);


-- ===============================
-- USERS TABLE
-- ===============================

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    RoleId INT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);


-- ===============================
-- TRAVEL ROUTES TABLE
-- ===============================

CREATE TABLE TravelRoutes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Source VARCHAR(100) NOT NULL,
    Destination VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL
);


-- ===============================
-- BOOKINGS TABLE
-- ===============================

CREATE TABLE Bookings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    RouteId INT NOT NULL,
    BookingDate DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RouteId) REFERENCES TravelRoutes(Id)
);


-- ===============================
-- INSERT ROLES
-- ===============================

INSERT INTO Roles (Name) VALUES
('Admin'),
('User');


-- ===============================
-- INSERT USERS
-- ===============================

INSERT INTO Users (Name, Email, Password, RoleId) VALUES
('Admin', 'admin@travel.com', 'admin123', 1),
('Rahul', 'rahul@email.com', 'rahul123', 2),
('Priya', 'priya@email.com', 'priya123', 2),
('Amit', 'amit@email.com', 'amit123', 2);


-- ===============================
-- INSERT TRAVEL ROUTES
-- ===============================

INSERT INTO TravelRoutes (Source, Destination, Price) VALUES
('Delhi', 'Lucknow', 800),
('Delhi', 'Jaipur', 900),
('Mumbai', 'Pune', 600),
('Bangalore', 'Hyderabad', 700);


-- ===============================
-- INSERT BOOKINGS
-- ===============================

INSERT INTO Bookings (UserId, RouteId) VALUES
(2,1),
(3,2),
(4,3),
(2,4);


-- ===============================
-- SAMPLE QUERY TO VIEW BOOKINGS
-- ===============================

SELECT 
    b.Id AS BookingId,
    u.Name AS User,
    r.Source,
    r.Destination,
    r.Price,
    b.BookingDate
FROM Bookings b
JOIN Users u ON b.UserId = u.Id
JOIN TravelRoutes r ON b.RouteId = r.Id;