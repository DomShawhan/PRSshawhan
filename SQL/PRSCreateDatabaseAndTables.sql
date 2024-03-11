CREATE DATABASE PRSshawhan;
GO

USE PRSshawhan;
GO

CREATE TABLE [User]
(
	Id int primary key identity(1, 1),
	Username varchar(20) unique not null,
	Password varchar(10) not null,
	Firstname varchar(20) not null,
	Lastname varchar(20) not null,
	Phone varchar(12) null,
	Email varchar(75) null,
	Reviewer bit not null,
	Admin bit not null
)
GO

INSERT INTO [User] (Username, Password, Firstname, Lastname, Phone, Email, Reviewer, Admin) VALUES ('dshawhan', 'mysecret', 'Dominic', 'Shawhan', '859-412-0590', 'dfshawhan@yahoo.com', 1, 1);
INSERT INTO [User] (Username, Password, Firstname, Lastname, Phone, Email, Reviewer, Admin) VALUES ('gmoose', 'noway2gues', 'Stuffed', 'Moose', '123-456-7890', 'moose@glacier.com', 1, 0);
INSERT INTO [User] (Username, Password, Firstname, Lastname, Reviewer, Admin) VALUES ('mrbison', 'not4U2know', 'Stuffed', 'Bison', 0, 0);
INSERT INTO [User] (Username, Password, Firstname, Lastname, Phone, Email, Reviewer, Admin) VALUES ('imthedev','topsecret','Susan','Jones','123-123-1235','susan@maxtrain.com',0,0);
INSERT INTO [User] (Username, Password, Firstname, Reviewer, Admin) VALUES ('cat','topsecret','Prince',0,0); -- Will Fail
INSERT INTO [User] (Username, Password, Firstname, Lastname, Reviewer, Admin) VALUES ('cat','topsecret','BillyBillyBillyBillyBilly','Jones',0,0); -- Will Fail
SELECT * FROM [User];
GO

CREATE TABLE Vendor
(
	Id int primary key identity(1,1),
	Code varchar(10) unique not null,
	Name varchar(255) not null,
	Address varchar(255) not null,
	City varchar(255) not null,
	State char(2) not null,
	Zip varchar(5) not null,
	Phone varchar(12) null,
	Email varchar(100) null
)
GO

INSERT INTO Vendor (Code, Name, Address, City, State, Zip, Phone, Email) Values ('AMAZON', 'Amazon', '123 Amazon Ave.', 'Amazon', 'CA', '12345', '123-456-7890', 'theamazonemail@amazon.com');
INSERT INTO Vendor (Code, Name, Address, City, State, Zip) Values ('mCENTER', 'MicroCenter', '11755 Mosteller Rd', 'Sharonville', 'OH', '45241');
SELECT * FROM Vendor;
GO

CREATE TABLE Product(
	Id int primary key identity(1,1),
	VendorId int not null constraint fk_product_vendor references Vendor(Id),
	PartNumber varchar(50) not null,
	Name varchar(150) not null,
	Price decimal(10,2) not null,
	Unit varchar(255) not null,  --EA, LBS, BOX, CASE
	PhotoPath varchar(255) null  --url
)
GO

ALTER TABLE Product ADD
	  CONSTRAINT product_unique_vendorid_partnumber UNIQUE NONCLUSTERED ( VendorID, PartNumber )
GO

INSERT INTO Product (VendorID, PartNumber, Name, Price, Unit) Values (2, '1T32RTS4KL', 'Laptop', 1799.99, 'EA');
INSERT INTO Product (VendorID, PartNumber, Name, Price, Unit) Values (1, '123456789P', 'Paper', 15, 'REAM');
INSERT INTO Product (VendorID, PartNumber, Name, Price, Unit, PhotoPath) Values (2, '28I4K144HzM', 'Monitor', 259.99, 'EA', 'C:\Users\Dominic\Pictures\20220319_182155.jpg');
SELECT * FROM Product;
GO

CREATE TABLE Request (
	Id int primary key identity(1,1),
	UserId int not null constraint fk_user_request references "User"(Id),
	Description varchar(100) not null,
	Justification varchar(255) not null,
	DateNeeded date not null,
	DeliveryMode varchar(25) not null default 'Pickup',
	Status varchar(20) not null default 'NEW',
	Total decimal(10, 2) not null default 0,
	SubmittedDate datetime not null default GetDate(),
	ReasonForRejection varchar(100) null
)
GO

INSERT INTO Request (UserId, Description, Justification, DateNeeded) Values (3, 'Laptop', 'Old Laptop broke', '2024/06/01');
INSERT INTO Request (UserId, Description, Justification, DateNeeded) Values (2, 'Paper', 'Out of Paper', '2024/06/01');
INSERT INTO Request (UserId, Description, Justification, DateNeeded) Values (3, 'Laptop and monitor', 'Setting up a home office', '2024/06/01');
SELECT * FROM Request;
GO

CREATE TABLE LineItem(
	Id int primary key identity(1,1),
	RequestId int not null constraint fk_lineitem_request references Request(Id),
	ProductId int not null constraint fk_lineitem_product references Product(Id),
	Quantity int not null constraint ch_quantity check(Quantity > 0),
)

ALTER TABLE LineItem ADD
     CONSTRAINT lineitem_unique_requestid_productid UNIQUE NONCLUSTERED ( RequestId, ProductId )

INSERT INTO LineItem(RequestId, ProductId, Quantity) VALUES (3, 1, 1);
INSERT INTO LineItem(RequestId, ProductId, Quantity) VALUES (3, 3, 2);
INSERT INTO LineItem(RequestId, ProductId, Quantity) VALUES (1, 1, 1);
INSERT INTO LineItem(RequestId, ProductId, Quantity) VALUES (2, 2, 5);

SELECT * FROM LineItem;

