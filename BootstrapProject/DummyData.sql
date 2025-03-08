-- Insert dummy data into AccountType table
INSERT INTO AccountType (AccountTypeName)
VALUES 
    ('Savings Account'),
    ('Checking Account'),
    ('Credit Card');

-- Insert dummy data into Brands table
INSERT INTO Brands (BrandName, [Description])
VALUES 
    ('Nike', 'Sportswear and equipment manufacturer'),
    ('Apple', 'Technology company'),
    ('Coca-Cola', 'Beverage manufacturer');

-- Insert dummy data into Categories table
INSERT INTO Categories (CategoryName, [Description])
VALUES 
    ('Electronics', 'Electronic devices and components'),
    ('Clothing', 'Apparel and accessories'),
    ('Food & Beverage', 'Food and beverage products');

-- Insert dummy data into EmployeeTypes table
INSERT INTO EmployeeTypes (EmployeeTitle, BaseSalary)
VALUES 
    ('Manager', 50000.00),
    ('Sales Associate', 30000.00),
    ('Customer Service Representative', 35000.00);

-- Insert dummy data into Suppliers table
INSERT INTO Suppliers (SupplierName, ContactNo, Address)
VALUES 
    ('ABC Electronics', 123456723, '123 Main Street, City'),
    ('XYZ Clothing', 98765432, '456 Elm Street, Town'),
    ('Fresh Groceries', 5556637, '789 Oak Street, Village');

-- Insert dummy data into SystemRoles table
INSERT INTO SystemRoles (RoleName)
VALUES 
    ('Admin'),
    ('Manager'),
    ('Employee');

-- Insert dummy data into Tasks table
INSERT INTO Tasks (TaskDescription, TaskType)
VALUES 
    ('Complete sales report', 'Administrative'),
    ('Restock inventory', 'Operational'),
    ('Respond to customer inquiries', 'Customer Service');

-- Insert dummy data into Users table
INSERT INTO Users (FirstName, LastName, Username, Email, ContactNo, Address, PostalCode, PasswordHash, Salt, LastLogin, CreatedAt, UpdatedAt, UserType)
VALUES 
    ('John', 'Doe', 'johndoe', 'john.doe@example.com', '1234567890', '123 Main Street', 12345, HASHBYTES('SHA2_512', 'password123'), HASHBYTES('SHA2_512', 'salt123'), '2024-05-23 08:00:00', '2024-05-23 08:00:00', '2024-05-23 08:00:00', 'Customer'),
    ('Jane', 'Smith', 'janesmith', 'jane.smith@example.com', '9876543210', '456 Elm Street', 67890, HASHBYTES('SHA2_512', 'password123'), HASHBYTES('SHA2_512', 'salt123'), '2024-05-23 09:00:00', '2024-05-23 09:00:00', '2024-05-23 09:00:00', 'Employee'),
    ('Admin', 'User', 'adminuser', 'admin@example.com', '5556667777', '789 Oak Street', 45678, HASHBYTES('SHA2_512', 'password123'), HASHBYTES('SHA2_512', 'salt123'), '2024-05-23 10:00:00', '2024-05-23 10:00:00', '2024-05-23 10:00:00', 'Admin');


-- Insert dummy data into Warehouses table
INSERT INTO Warehouses (WarehouseName, Address)
VALUES 
    ('Main Warehouse', '123 Warehouse Street'),
    ('Secondary Warehouse', '456 Storage Street'),
    ('Overflow Warehouse', '789 Overflow Street');




-- Insert dummy data into Accounts table
INSERT INTO Accounts (AccountName, isCompanyOrEmployee, Balance, UpdatedAt, Currency, Description, AccountTypeID, EmployeeID, CompanyID)
VALUES 
    ('UKEA Main Savings', 'Company', 10000.00, '2024-05-23 08:00:00', 'USD', 'Savings account for John Doe', 1, NULL, 1),
    ('Jane Smith Checking', 'Employee', 5000.00, '2024-05-23 09:00:00', 'USD', 'Checking account for Jane Smith', 2, 3, NULL),
    ('UKEA Tampines Savings', 'Company', 2000.00, '2024-05-23 10:00:00', 'USD', 'Credit card account for Admin User', 3, NULL, 2),
    ('UKEA Alexandea Savings', 'Company', 2000.00, '2024-05-23 10:00:00', 'USD', 'Credit card account for Admin User', 3, NULL, 3);


-- Insert dummy data into Products table
INSERT INTO Products (ProductName, SKU, imageUrl, Quantity, UnitPrice, CostPrice, DateReceived, DateSold, Condition, MinStockLevel, ReorderLevel, LeadTime, BatchNo, Notes, BrandID, CategoryID, SupplierID)
VALUES 
    ('iPhone 13', 'IPH13', 'iphone13.jpg', 100, 999.99, 799.99, '2024-05-20 12:00:00', '2024-05-25 12:00:00', 'New', 20, 10, 7, 'BATCH001', 'Latest model', 13, 1, 12),
    ('Nike Air Max', 'NAM001', 'nikeairmax.jpg', 50, 129.99, 89.99, '2024-05-21 10:00:00', '2024-05-25 10:00:00', 'New', 10, 5, 5, 'BATCH002', 'Various colors available', 14, 2, 13),
    ('Coca-Cola 12-pack', 'CC12', 'cocacola.jpg', 200, 9.99, 7.99, '2024-05-22 08:00:00', '2024-05-25 08:00:00', 'New', 50, 30, 3, 'BATCH003', 'Soft drink', 16, 3, 14);

-- Insert dummy data into Customers table
INSERT INTO Customers (DateOfBirth, CreditCardInfo, UserID)
VALUES 
    ('1990-01-01', '1234567890123456', 1),
    ('1985-05-15', '9876543210987654', 2),
    ('1978-10-30', '5555666677778888', 3);

-- Insert dummy data into Companies table
INSERT INTO Companies (CompanyName, AccountID)
VALUES 
    ('UKEA Main', 11),
    ('UKEA Tampines Branch', 13),
    ('UKEA ALexandra Branch', 14);


-- Insert dummy data into Employees table
INSERT INTO Employees (FirstName, LastName, Salary, SystemRoleID, AccountID, EmployeeTypeID, UserID)
VALUES 
    ('Jane', 'Smith', 50000.00, 2, 12, 1, 2);


-- Insert dummy data into ProductWarehouses table
INSERT INTO ProductWarehouses (Quantity, UnitPrice, ProductID, WarehouseID)
VALUES 
    (50, 999.99, 16, 1),
    (30, 129.99, 17, 2),
    (100, 9.99, 18, 3);


-- Insert dummy data into Orders table
INSERT INTO Orders (OrderDate, DeliveryPrice, PaymentType, ShippingAddress, CustomerID)
VALUES 
    ('2024-05-23 08:30:00', 5.00, 'Credit Card', '123 Main Street', 1),
    ('2024-05-23 09:30:00', 7.50, 'PayPal', '456 Elm Street', 2),
    ('2024-05-23 10:30:00', 10.00, 'Cash on Delivery', '789 Oak Street', 3);

-- Insert dummy data into Reviews table
INSERT INTO Reviews (Rating, ReviewText, ReviewDate, CustomerID, ProductID)
VALUES 
    (5, 'Great product, highly recommended!', '2024-05-24 08:00:00', 1, 16),
    (4, 'Nice shoes, comfortable to wear.', '2024-05-24 09:00:00', 2, 17),
    (3, 'Good taste but delivery was slow.', '2024-05-24 10:00:00', 3, 18);

-- Insert dummy data into Absences table
INSERT INTO Absences (DateOfAbsence, ReasonOfAbsence, isApproved, EmployeeID)
VALUES 
    ('2024-05-20', 'Sick leave', 1, 9),
    ('2024-05-21', 'Vacation', 1, 9),
    ('2024-05-22', 'Family emergency', 0, 9);

-- Insert dummy data into CustomerTasks table
INSERT INTO CustomerTasks (Status, DueDate, CreatedAt, UpdatedAt, CustomerID, TaskID, AssignEmployeeID)
VALUES 
    ('Pending', '2024-05-25', '2024-05-23 08:00:00', '2024-05-23 08:00:00', 1, 1, 9),
    ('In Progress', '2024-05-26', '2024-05-23 09:00:00', '2024-05-23 09:00:00', 2, 2, 9),
    ('Completed', '2024-05-27', '2024-05-23 10:00:00', '2024-05-23 10:00:00', 3, 3, 9);

-- Insert dummy data into Interactions table
INSERT INTO Interactions (InteractionDate, Subject, Details, Outcome, CustomerID, EmployeeID)
VALUES 
    ('2024-05-24 08:30:00', 'Product Inquiry', 'Customer inquiring about product features', 'Provided product details', 1, 9),
    ('2024-05-24 09:30:00', 'Order Status', 'Customer checking order status', 'Confirmed order shipment', 2, 9),
    ('2024-05-24 10:30:00', 'Complaint', 'Customer reporting damaged product', 'Offered replacement or refund', 3, 9);

-- Insert dummy data into Payrolls table
INSERT INTO Payrolls (DateOfPayment, Amount, isPaid, EmployeeID)
VALUES
    ('2024-05-31', 2500.00, 1, 9),
    ('2024-05-31', 1500.00, 1, 9),
    ('2024-05-31', 1750.00, 1, 9);

-- Insert dummy data into OrderDetails table
INSERT INTO OrderDetails (Quantity, UnitPrice, TotalPrice, CompanyID, OrderID, ProductWarehouseID)
VALUES 
    (2, 999.99, 1999.98, 14, 1, 15),
    (1, 129.99, 129.99, 14, 2, 16),
    (4, 9.99, 39.96, 16, 3, 17);
