IF EXISTS (SELECT * FROM sys.databases WHERE name = 'LibraryManager')
BEGIN
    USE master;
    ALTER DATABASE LibraryManager SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE LibraryManager;
END

-- Create Database
CREATE DATABASE LibraryManager;
GO

USE LibraryManager;
GO

-- Create Borrower Table
CREATE TABLE Borrower (
    BorrowerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(15) NOT NULL,
    LastName NVARCHAR(15) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(10) NOT NULL,
    DoB DATE NOT NULL
);
GO

-- Create MediaType Table
CREATE TABLE MediaType (
    MediaTypeID INT PRIMARY KEY IDENTITY(1,1),
    MediaTypeName NVARCHAR(50) NOT NULL
);
GO

-- Create Media Table
CREATE TABLE Media (
    MediaID INT PRIMARY KEY IDENTITY(1,1),
    MediaTypeID INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    IsArchived bit not null default(0),
    FOREIGN KEY (MediaTypeID) REFERENCES MediaType(MediaTypeID)
);
GO

-- Create CheckoutLog Table
CREATE TABLE CheckoutLog (
    CheckoutLogID INT PRIMARY KEY IDENTITY(1,1),
    BorrowerID INT NOT NULL,
    MediaID INT NOT NULL,
    CheckoutDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE,  -- Nullable to indicate ongoing checkouts
    FOREIGN KEY (BorrowerID) REFERENCES Borrower(BorrowerID),
    FOREIGN KEY (MediaID) REFERENCES Media(MediaID)
);
GO

-- Add sample data
INSERT INTO MediaType (MediaTypeName) 
VALUES 
    ('Book'), 
    ('DVD'), 
    ('Digital Audio');

INSERT INTO Borrower (FirstName, LastName, Email, Phone, DoB) 
VALUES 
    ('John', 'Doe', 'john.doe@example.com', '1234567890', '1980-01-01'),
    ('Jane', 'Smith', 'jane.smith@example.com', '2345678901', '1990-02-02'),
    ('Alice', 'Johnson', 'alice.johnson@example.com', '3456789012', '1985-03-03'),
    ('Bob', 'Williams', 'bob.williams@example.com', '4567890123', '1975-04-04'),
    ('Emily', 'Brown', 'emily.brown@example.com', '5678901234', '1995-05-05');

INSERT INTO Media (MediaTypeID, Title) 
VALUES 
    (1, 'Pride and Prejudice'), 
    (1, '1984'), 
    (1, 'To Kill a Mockingbird'), 
    (1, 'The Great Gatsby'), 
    (1, 'Moby Dick'),
    (2, 'The Shawshank Redemption'), 
    (2, 'Forrest Gump'), 
    (2, 'The Godfather'), 
    (2, 'Jurassic Park'), 
    (2, 'Star Wars: A New Hope'),
    (3, 'Abbey Road'), 
    (3, 'Thriller'), 
    (3, 'Back in Black'), 
    (3, 'The Dark Side of the Moon'), 
    (3, 'Led Zeppelin IV');