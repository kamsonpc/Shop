CREATE TABLE [dbo].[Files] (

    [Id] [bigint] NOT NULL,
	[FolderId] [bigint] NULL,
    [Name] [nvarchar](256) NOT NULL,
    [Type] [nvarchar](256) NOT NULL,
    [SizeMb] [bigint] NOT NULL,
    [CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate][datetime] NULL,

    CONSTRAINT [PK_files.Files] PRIMARY KEY ([Id]),
	FOREIGN KEY ([FolderId]) REFERENCES [Folders]([Id])
)