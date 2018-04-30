
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/09/2018 16:39:13
-- Generated from EDMX file: C:\Users\v-abmedi\Desktop\HRR\HRR\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HRR];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Skills'
CREATE TABLE [dbo].[Skills] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [ProjectId] smallint  NOT NULL,
    [Name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(1000)  NOT NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'SkillSets'
CREATE TABLE [dbo].[SkillSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SkillId] smallint  NOT NULL,
    [RoleId] smallint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Skills'
ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [PK_Skills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SkillSets'
ALTER TABLE [dbo].[SkillSets]
ADD CONSTRAINT [PK_SkillSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK_ProjectRole]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Projects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectRole'
CREATE INDEX [IX_FK_ProjectRole]
ON [dbo].[Roles]
    ([ProjectId]);
GO

-- Creating foreign key on [SkillId] in table 'SkillSets'
ALTER TABLE [dbo].[SkillSets]
ADD CONSTRAINT [FK_SkillSkillSet]
    FOREIGN KEY ([SkillId])
    REFERENCES [dbo].[Skills]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SkillSkillSet'
CREATE INDEX [IX_FK_SkillSkillSet]
ON [dbo].[SkillSets]
    ([SkillId]);
GO

-- Creating foreign key on [RoleId] in table 'SkillSets'
ALTER TABLE [dbo].[SkillSets]
ADD CONSTRAINT [FK_RoleSkillSet]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleSkillSet'
CREATE INDEX [IX_FK_RoleSkillSet]
ON [dbo].[SkillSets]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------