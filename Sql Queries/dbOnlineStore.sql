--==== Database : dbOnlineStore =====
Create Database OnlineStore
use OnlineStore
--=== Users ====
CREATE TABLE UserList (
    UserId   INT          IDENTITY (1, 1) NOT NULL Primary Key,
    Name     VARCHAR (50) NULL,
    Email    VARCHAR (50) NULL,
    Password VARCHAR (50) NULL,
    RoleType INT          NULL    
);
--=== Categories ====
CREATE TABLE Categories (
    CatId INT          IDENTITY (1, 1) NOT NULL Primary Key,
    Name  VARCHAR (50) NULL
);
--=== Products ====
CREATE TABLE [dbo].[Products] (
    [ProID]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)   NULL,
    [Description] VARCHAR (50)   NULL,
    [Unit]        INT            NULL,
    [Image]       VARCHAR (1000) NULL,
    [CatId]       INT            NULL,
    PRIMARY KEY CLUSTERED ([ProID] ASC),
    CONSTRAINT [FK_Products_Categories] FOREIGN KEY ([CatId]) REFERENCES [dbo].[Categories] ([CatId])
);
--=== Invoice ====
CREATE TABLE [dbo].[tblInvoice] (
    [InvoiceId]   INT          IDENTITY (1, 1) NOT NULL,
    [UserId]      INT          NULL,
    [Bill]        INT          NULL,
    [Payment]     VARCHAR (50) NULL,
    [InvoiceDate] DATE         NULL,
    PRIMARY KEY CLUSTERED ([InvoiceId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);
--=== Order ====
CREATE TABLE [dbo].[tblOrder] (
    [OrderId]   INT           IDENTITY (1, 1) NOT NULL,
    [ProID]     INT           NULL,
    [Contact]   VARCHAR (50)  NULL,
    [Address]   VARCHAR (100) NULL,
    [Unit]      INT           NULL,
    [Qty]       INT           NULL,
    [Total]     INT           NULL,
    [OrderDate] DATE          NULL,
    [InvoiceId] INT           NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC),
    FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[tblInvoice] ([InvoiceId]),
    CONSTRAINT [FK_tblOrder_tblProducts] FOREIGN KEY ([ProID]) REFERENCES [dbo].[tblProducts] ([ProID])
);

select * from tblUser
select * from tblProducts
select * from tblCategories
select * from tblInvoice
select * from tblOrder
------------------------
drop table tblUser
drop table tblProducts
drop table tblCategories
drop table tblInvoice
drop table tblOrder
