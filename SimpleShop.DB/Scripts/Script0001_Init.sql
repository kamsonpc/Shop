CREATE TABLE [dbo].[Carts] (
    [CartItemId] [int] NOT NULL IDENTITY,
    [ApplicationUserId] [nvarchar](128) NOT NULL,
    [ProductId] [int] NOT NULL,
    [OrderedQuantity] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Carts] PRIMARY KEY ([CartItemId])
)
CREATE INDEX [IX_ApplicationUserId] ON [dbo].[Carts]([ApplicationUserId])
CREATE INDEX [IX_ProductId] ON [dbo].[Carts]([ProductId])
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] [nvarchar](128) NOT NULL,
    [Email] [nvarchar](256),
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](max),
    [SecurityStamp] [nvarchar](max),
    [PhoneNumber] [nvarchar](max),
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime],
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserName] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName])
CREATE TABLE [dbo].[UserAddresses] (
    [Id] [int] NOT NULL IDENTITY,
    [NameAndSurname] [nvarchar](255) NOT NULL,
    [PhoneNumber] [nvarchar](max) NOT NULL,
    [Address] [nvarchar](255) NOT NULL,
    [CityCode] [nvarchar](max) NOT NULL,
    [Country] [nvarchar](max) NOT NULL,
    [ApplicationUserId] [nvarchar](max) NOT NULL,
    [ApplicationUser_Id] [nvarchar](128) NOT NULL,
    CONSTRAINT [PK_dbo.UserAddresses] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ApplicationUser_Id] ON [dbo].[UserAddresses]([ApplicationUser_Id])
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [nvarchar](128) NOT NULL,
    [ClaimType] [nvarchar](max),
    [ClaimValue] [nvarchar](max),
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId])
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] [nvarchar](128) NOT NULL,
    [ProviderKey] [nvarchar](128) NOT NULL,
    [UserId] [nvarchar](128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId])
CREATE TABLE [dbo].[Orders] (
    [OrderId] [int] NOT NULL IDENTITY,
    [ProductId] [int] NOT NULL,
    [ApplicationUserId] [nvarchar](128) NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [Quantity] [int] NOT NULL,
    [Date] [datetime] NOT NULL,
    [Payment] [bit] NOT NULL,
    [NameAndSurname] [nvarchar](255) NOT NULL,
    [PhoneNumber] [nvarchar](max) NOT NULL,
    [Address] [nvarchar](255) NOT NULL,
    [CityCode] [nvarchar](max) NOT NULL,
    [Country] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_dbo.Orders] PRIMARY KEY ([OrderId])
)
CREATE INDEX [IX_ProductId] ON [dbo].[Orders]([ProductId])
CREATE INDEX [IX_ApplicationUserId] ON [dbo].[Orders]([ApplicationUserId])
CREATE TABLE [dbo].[Products] (
    [ProductId] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    [Description] [nvarchar](max) NOT NULL,
    [Img] [nvarchar](max),
    [Quantity] [int] NOT NULL,
    [AddDate] [datetime] NOT NULL,
    [CategoryId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY ([ProductId])
)
CREATE INDEX [IX_CategoryId] ON [dbo].[Products]([CategoryId])
CREATE TABLE [dbo].[Categories] (
    [CategoryId] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY ([CategoryId])
)
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] [nvarchar](128) NOT NULL,
    [RoleId] [nvarchar](128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId])
CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId])
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] [nvarchar](128) NOT NULL,
    [Name] [nvarchar](256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([Name])
ALTER TABLE [dbo].[Carts] ADD CONSTRAINT [FK_dbo.Carts_dbo.AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Carts] ADD CONSTRAINT [FK_dbo.Carts_dbo.Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]) ON DELETE CASCADE
ALTER TABLE [dbo].[UserAddresses] ADD CONSTRAINT [FK_dbo.UserAddresses_dbo.AspNetUsers_ApplicationUser_Id] FOREIGN KEY ([ApplicationUser_Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]) ON DELETE CASCADE
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_dbo.Products_dbo.Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE