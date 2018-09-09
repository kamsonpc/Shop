CREATE TABLE [dbo].[Folders] (

    [Id] [bigint] NOT NULL,
    [Name] [nvarchar](256) NOT NULL,
	[ParentId] [bigint] NULL,
    [Depth] [nvarchar](256) NOT NULL,
    [CreatedBy] [nvarchar](256) NOT NULL,
	[ModifitedBy] [nvarchar](256) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate][datetime] NULL,

    CONSTRAINT [PK_files.Folders] PRIMARY KEY ([Id])
)