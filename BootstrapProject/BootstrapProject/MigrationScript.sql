IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AccountType] (
    [AccountTypeID] int NOT NULL IDENTITY,
    [AccountTypeName] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY ([AccountTypeID])
);
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Brands' AND type = 'U')
CREATE TABLE [Brands] (
    [BrandID] int NOT NULL IDENTITY,
    [BrandName] nvarchar(255) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY ([BrandID])
);
GO

CREATE TABLE [Categories] (
    [CategoryID] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(255) NOT NULL,
    [Description] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryID])
);
GO

CREATE TABLE [EmployeeTypes] (
    [EmployeeTypeID] int NOT NULL IDENTITY,
    [EmployeeTitle] nvarchar(100) NOT NULL,
    [BaseSalary] decimal(10,2) NOT NULL,
    CONSTRAINT [PK_EmployeeTypes] PRIMARY KEY ([EmployeeTypeID])
);
GO

CREATE TABLE [Suppliers] (
    [SupplierID] int NOT NULL IDENTITY,
    [SupplierName] nvarchar(255) NOT NULL,
    [ContactNo] int NOT NULL,
    [Address] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([SupplierID])
);
GO

CREATE TABLE [SystemRoles] (
    [SystemRoleID] int NOT NULL IDENTITY,
    [RoleName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_SystemRoles] PRIMARY KEY ([SystemRoleID])
);
GO

CREATE TABLE [Tasks] (
    [TaskID] int NOT NULL IDENTITY,
    [TaskDescription] nvarchar(255) NOT NULL,
    [TaskType] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([TaskID])
);
GO

CREATE TABLE [Users] (
    [UserID] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Username] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [ContactNo] nvarchar(15) NOT NULL,
    [Address] nvarchar(255) NOT NULL,
    [PostalCode] int NOT NULL,
    [PasswordHash] varbinary(255) NOT NULL,
    [Salt] varbinary(255) NOT NULL,
    [LastLogin] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdateddAt] datetime2 NOT NULL,
    [UserType] nvarchar(10) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserID])
);
GO

CREATE TABLE [Warehouses] (
    [WarehouseID] int NOT NULL IDENTITY,
    [WarehouseName] nvarchar(255) NOT NULL,
    [Address] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Warehouses] PRIMARY KEY ([WarehouseID])
);
GO

CREATE TABLE [Accounts] (
    [AccountID] int NOT NULL IDENTITY,
    [AccountName] nvarchar(max) NOT NULL,
    [isCompanyOrEmployee] nvarchar(450) NOT NULL,
    [Balance] decimal(10,2) NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [Currency] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [AccountTypeID] int NOT NULL,
    [EmployeeID] int NOT NULL,
    [CompanyID] int NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountID]),
    CONSTRAINT [FK_Accounts_AccountType_AccountTypeID] FOREIGN KEY ([AccountTypeID]) REFERENCES [AccountType] ([AccountTypeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Products] (
    [ProductID] int NOT NULL IDENTITY,
    [ProductName] nvarchar(255) NOT NULL,
    [SKU] nvarchar(100) NOT NULL,
    [imageUrl] nvarchar(255) NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(10,2) NOT NULL,
    [CostPrice] decimal(10,2) NOT NULL,
    [DateReceived] datetime2 NOT NULL,
    [DateSold] datetime2 NOT NULL,
    [Condition] nvarchar(25) NOT NULL,
    [MinStockLevel] int NOT NULL,
    [ReorderLevel] int NOT NULL,
    [LeadTime] int NOT NULL,
    [BatchNo] nvarchar(100) NOT NULL,
    [Notes] nvarchar(255) NOT NULL,
    [BrandID] int NOT NULL,
    [CategoryID] int NULL,
    [SupplierID] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductID]),
    CONSTRAINT [FK_Products_Brands_BrandID] FOREIGN KEY ([BrandID]) REFERENCES [Brands] ([BrandID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([CategoryID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Products_Suppliers_SupplierID] FOREIGN KEY ([SupplierID]) REFERENCES [Suppliers] ([SupplierID]) ON DELETE CASCADE
);
GO


CREATE TABLE [Customers] (
    [CustomerID] int NOT NULL IDENTITY,
    [DateOfBirth] datetime2 NOT NULL,
    [CreditCardInfo] nvarchar(255) NOT NULL,
    [UserID] int NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerID]),
    CONSTRAINT [FK_Customers_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Companies] (
    [CompanyID] int NOT NULL IDENTITY,
    [CompanyName] nvarchar(max) NOT NULL,
    [AccountID] int NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY ([CompanyID]),
    CONSTRAINT [FK_Companies_Accounts_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Accounts] ([AccountID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Employees] (
    [EmployeeID] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Salary] decimal(10,2) NOT NULL,
    [SystemRoleID] int NOT NULL,
    [AccountID] int NOT NULL,
    [EmployeeTypeID] int NOT NULL,
    [UserID] int NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeID]),
    CONSTRAINT [FK_Employees_Accounts_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Accounts] ([AccountID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeTypeID] FOREIGN KEY ([EmployeeTypeID]) REFERENCES [EmployeeTypes] ([EmployeeTypeID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employees_SystemRoles_SystemRoleID] FOREIGN KEY ([SystemRoleID]) REFERENCES [SystemRoles] ([SystemRoleID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employees_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]) ON DELETE CASCADE
);
GO


CREATE TABLE [ProductWarehouses] (
    [ProductWarehouseID] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(10,2) NOT NULL,
    [ProductID] int NOT NULL,
    [WarehouseID] int NOT NULL,
    CONSTRAINT [PK_ProductWarehouses] PRIMARY KEY ([ProductWarehouseID]),
    CONSTRAINT [FK_ProductWarehouses_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ProductID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductWarehouses_Warehouses_WarehouseID] FOREIGN KEY ([WarehouseID]) REFERENCES [Warehouses] ([WarehouseID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Orders] (
    [OrderID] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [DeliveryPrice] decimal(10,2) NOT NULL,
    [PaymentType] nvarchar(20) NOT NULL,
    [ShippingAddress] nvarchar(255) NOT NULL,
    [CustomerID] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderID]),
    CONSTRAINT [FK_Orders_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reviews] (
    [ReviewID] int NOT NULL IDENTITY,
    [Rating] int NOT NULL,
    [ReviewText] nvarchar(max) NOT NULL,
    [ReviewDate] datetime2 NOT NULL,
    [CustomerID] int NOT NULL,
    [ProductID] int NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([ReviewID]),
    CONSTRAINT [Ck_ReviewRating] CHECK ([Rating] >= 1 AND [Rating] <= 5),
    CONSTRAINT [FK_Reviews_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reviews_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ProductID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Absences] (
    [AbsenceID] int NOT NULL IDENTITY,
    [DateOfAbsence] datetime2 NOT NULL,
    [ReasonOfAbsence] nvarchar(255) NOT NULL,
    [isApproved] bit NOT NULL,
    [EmployeeID] int NOT NULL,
    CONSTRAINT [PK_Absences] PRIMARY KEY ([AbsenceID]),
    CONSTRAINT [FK_Absences_Employees_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Employees] ([EmployeeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [CustomerTasks] (
    [CustomerTaskID] int NOT NULL IDENTITY,
    [Status] nvarchar(20) NOT NULL,
    [DueDate] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [CustomerID] int NOT NULL,
    [TaskID] int NOT NULL,
    [AssignEmployeeID] int NOT NULL,
    CONSTRAINT [PK_CustomerTasks] PRIMARY KEY ([CustomerTaskID]),
    CONSTRAINT [FK_CustomerTasks_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerTasks_Employees_AssignEmployeeID] FOREIGN KEY ([AssignEmployeeID]) REFERENCES [Employees] ([EmployeeID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerTasks_Tasks_TaskID] FOREIGN KEY ([TaskID]) REFERENCES [Tasks] ([TaskID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Interactions] (
    [InteractionID] int NOT NULL IDENTITY,
    [InteractionDate] datetime2 NOT NULL,
    [Subject] nvarchar(100) NOT NULL,
    [Details] nvarchar(255) NOT NULL,
    [Outcome] nvarchar(255) NOT NULL,
    [CustomerID] int NOT NULL,
    [EmployeeID] int NOT NULL,
    CONSTRAINT [PK_Interactions] PRIMARY KEY ([InteractionID]),
    CONSTRAINT [FK_Interactions_Customers_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Customers] ([CustomerID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Interactions_Employees_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Employees] ([EmployeeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Payrolls] (
    [PayrollID] int NOT NULL IDENTITY,
    [DateOfPayment] datetime2 NOT NULL,
    [Amount] decimal(10,2) NOT NULL,
    [isPaid] bit NOT NULL,
    [EmployeeID] int NOT NULL,
    CONSTRAINT [PK_Payrolls] PRIMARY KEY ([PayrollID]),
    CONSTRAINT [FK_Payrolls_Employees_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [Employees] ([EmployeeID]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderDetails] (
    [OrderDetailsID] int NOT NULL IDENTITY,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(10,2) NOT NULL,
    [TotalPrice] decimal(10,2) NOT NULL,
    [CompanyID] int NOT NULL,
    [OrderID] int NOT NULL,
    [ProductWarehouseID] int NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderDetailsID]),
    CONSTRAINT [FK_OrderDetails_Companies_CompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [Companies] ([CompanyID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderDetails_Orders_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Orders] ([OrderID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderDetails_ProductWarehouses_ProductWarehouseID] FOREIGN KEY ([ProductWarehouseID]) REFERENCES [ProductWarehouses] ([ProductWarehouseID]) ON DELETE NO CASCADE
);
GO


CREATE INDEX [IX_Absences_EmployeeID] ON [Absences] ([EmployeeID]);
GO

CREATE INDEX [IX_Accounts_AccountTypeID] ON [Accounts] ([AccountTypeID]);
GO

CREATE INDEX [IX_Accounts_isCompanyOrEmployee] ON [Accounts] ([isCompanyOrEmployee]);
GO

CREATE UNIQUE INDEX [IX_Companies_AccountID] ON [Companies] ([AccountID]);
GO

CREATE UNIQUE INDEX [IX_Customers_UserID] ON [Customers] ([UserID]);
GO

CREATE INDEX [IX_CustomerTasks_AssignEmployeeID] ON [CustomerTasks] ([AssignEmployeeID]);
GO

CREATE INDEX [IX_CustomerTasks_CustomerID] ON [CustomerTasks] ([CustomerID]);
GO

CREATE INDEX [IX_CustomerTasks_TaskID] ON [CustomerTasks] ([TaskID]);
GO

CREATE UNIQUE INDEX [IX_Employees_AccountID] ON [Employees] ([AccountID]);
GO

CREATE INDEX [IX_Employees_EmployeeTypeID] ON [Employees] ([EmployeeTypeID]);
GO

CREATE INDEX [IX_Employees_SystemRoleID] ON [Employees] ([SystemRoleID]);
GO

CREATE UNIQUE INDEX [IX_Employees_UserID] ON [Employees] ([UserID]);
GO

CREATE INDEX [IX_Interactions_CustomerID] ON [Interactions] ([CustomerID]);
GO

CREATE INDEX [IX_Interactions_EmployeeID] ON [Interactions] ([EmployeeID]);
GO

CREATE INDEX [IX_OrderDetails_CompanyID] ON [OrderDetails] ([CompanyID]);
GO

CREATE INDEX [IX_OrderDetails_OrderID] ON [OrderDetails] ([OrderID]);
GO

CREATE INDEX [IX_OrderDetails_ProductWarehouseID] ON [OrderDetails] ([ProductWarehouseID]);
GO

CREATE INDEX [IX_Orders_CustomerID] ON [Orders] ([CustomerID]);
GO

CREATE INDEX [IX_Payrolls_EmployeeID] ON [Payrolls] ([EmployeeID]);
GO

CREATE INDEX [IX_Products_BrandID] ON [Products] ([BrandID]);
GO

CREATE INDEX [IX_Products_CategoryID] ON [Products] ([CategoryID]);
GO

CREATE INDEX [IX_Products_SupplierID] ON [Products] ([SupplierID]);
GO

CREATE INDEX [IX_ProductWarehouses_ProductID] ON [ProductWarehouses] ([ProductID]);
GO

CREATE INDEX [IX_ProductWarehouses_WarehouseID] ON [ProductWarehouses] ([WarehouseID]);
GO

CREATE INDEX [IX_Reviews_CustomerID] ON [Reviews] ([CustomerID]);
GO

CREATE INDEX [IX_Reviews_ProductID] ON [Reviews] ([ProductID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240523150806_InitialCreate', N'8.0.5');
GO

COMMIT;
GO

INSERT INTO AccountEntries (AccountID, AccountName, AccountType, Balance, DateUpdated, Currency, EmployeeID, Description) VALUES
(10001, 'Cash', 'Assets', 10, '2024-05-15', 'SGD', 0, 'Money held in currency form or in bank accounts'),
(10002, 'Accounts Receivable', 'Assets', 20, '2024-05-15', 'SGD', 0, 'Money owed to the business by customers for goods or services delivered'),
(10003, 'Inventory', 'Assets', 30, '2024-05-15', 'SGD', 0, 'Value of goods available for sale'),
(10004, 'Prepaid Expenses', 'Assets', 40, '2024-05-15', 'SGD', 0, 'Payments made in advance for goods or services to be received in the future'),
(10005, 'Noncurrent Assets', 'Assets', 50, '2024-05-15', 'SGD', 0, 'Long-term assets used in the business, like equipment and buildings'),
(10006, 'Accumulated Depreciation', 'Assets', 60, '2024-05-15', 'SGD', 0, 'The total depreciation of fixed assets over time'),
(10007, 'Accounts Payable', 'Liabilities', 70, '2024-05-15', 'SGD', 0, 'Money owed by the business to suppliers'),
(10008, 'Accrued Expenses', 'Liabilities', 80, '2024-05-15', 'SGD', 0, 'Expenses that have been incurred but not yet paid'),
(10009, 'Unearned Revenue', 'Liabilities', 90, '2024-05-15', 'SGD', 0, 'Money received in advance for goods or services to be delivered in the future'),
(10010, 'Short-term Loans', 'Liabilities', 100, '2024-05-15', 'SGD', 0, 'Loans and other debts that are due within the current year'),
(10011, 'Long-term Loans', 'Liabilities', 110, '2024-05-15', 'SGD', 0, 'Loans and other debts that are not due within the next year'),
(10012, 'Common Stock', 'Equity', 120, '2024-05-15', 'SGD', 0, 'Equity raised from issuing common shares'),
(10013, 'Preferred Stock', 'Equity', 130, '2024-05-15', 'SGD', 0, 'Equity raised from issuing preferred shares'),
(10014, 'Retained Earnings', 'Equity', 140, '2024-05-15', 'SGD', 0, 'Profits reinvested in the business, not distributed to shareholders'),
(10015, 'Sales Revenue', 'Revenue', 150, '2024-05-15', 'SGD', 0, 'Income from goods sold or services rendered'),
(10016, 'Service Revenue', 'Revenue', 160, '2024-05-15', 'SGD', 0, 'Income specifically from services provided'),
(10017, 'Interest Income', 'Revenue', 170, '2024-05-15', 'SGD', 0, 'Income from interest on investments or loans to others'),
(10018, 'Rental Income', 'Revenue', 180, '2024-05-15', 'SGD', 0, 'Income from renting out property or equipment'),
(10019, 'Cost of Goods Sold', 'Expenses', 190, '2024-05-15', 'SGD', 0, 'The cost of inventory sold to customers'),
(10020, 'Operating Expenses', 'Expenses', 200, '2024-05-15', 'SGD', 0, 'Expenses incurred in the normal operation of the business'),
(10021, 'Depreciation', 'Expenses', 210, '2024-05-15', 'SGD', 0, 'The periodic allocation of the cost of a fixed asset over its useful life'),
(10022, 'Interest Expense', 'Expenses', 220, '2024-05-15', 'SGD', 0, 'Costs associated with borrowing money');

