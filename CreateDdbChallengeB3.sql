Create database ChallengeB3

USE [challengeB3]
GO

CREATE TABLE [dbo].[tb_Register](
	[RegisterId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[Date] [datetime] NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[StoredEvent](
	[Id] [uniqueidentifier] NOT NULL,
	[AggregateId] [int] NOT NULL,
	[Data] [nvarchar](max) NULL,
	[Action] [varchar](100) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[User] [nvarchar](max) NULL,
 CONSTRAINT [PK_StoredEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

