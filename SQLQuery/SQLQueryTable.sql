CREATE TABLE [dbo].[Employee] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [FIO]        NVARCHAR (MAX) COLLATE Cyrillic_General_CI_AS NOT NULL,
    [Salary]     NVARCHAR (MAX) NULL,
    [Department] NVARCHAR (100) NULL,
    CONSTRAINT [PK_dbo.Employee] PRIMARY KEY CLUSTERED ([Id] ASC)
);