
-- GROUP

CREATE TABLE [dbo].[group] (
    [GroupId]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL,
    [Translator] NVARCHAR (100) NULL
);

-- WORD

CREATE TABLE [dbo].[word] (
    [WordId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL,
    [Translator] NVARCHAR (100) NOT NULL,
    [GroupId]    INT            NULL
);

GO
CREATE NONCLUSTERED INDEX [IX_word_GroupId]
    ON [dbo].[word]([GroupId] ASC);

GO
ALTER TABLE [dbo].[word]
    ADD CONSTRAINT [PK_word] PRIMARY KEY CLUSTERED ([WordId] ASC);

GO
ALTER TABLE [dbo].[word]
    ADD CONSTRAINT [FK_word_group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[group] ([GroupId]);