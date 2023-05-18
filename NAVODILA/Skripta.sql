CREATE TABLE [Groups] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Providers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [SID] nvarchar(max) NOT NULL,
    [Url] nvarchar(max) NOT NULL,
    [SentCount] int NOT NULL,
    CONSTRAINT [PK_Providers] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Bussiness] bit NOT NULL,
    [TaxNumber] nvarchar(max) NOT NULL,
    [GroupId] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Users_GroupId] ON [Users] ([GroupId]);
GO

INSERT INTO Providers (Name, SentCount, SID, Url)
VALUES ('Siol', 0, 'ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX', 'https://api.siol.com/2010-04-01/Accounts/{AccountSid}/Messages.json'), ('Telekom', 0, 'ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX', 'https://api.telekom.com/2010-04-01/Accounts/{AccountSid}/Messages.json')