USE [DBJogosAPI]
GO

/****** Object:  Table [dbo].[Charact]    Script Date: 09/11/2023 11:30:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Charact](
	[CharId] [bigint] IDENTITY(1,1) NOT NULL,
	[CharName] [nvarchar](100) NOT NULL,
	[CharClass] [nvarchar](100) NOT NULL,
	[CharSex] [nvarchar](20) NOT NULL,
	[ClassId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [Pk_CharId] PRIMARY KEY CLUSTERED 
(
	[CharId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Charact]  WITH CHECK ADD  CONSTRAINT [FK_CharactClassReference] FOREIGN KEY([ClassId])
REFERENCES [dbo].[ClassReference] ([ClassId])
GO

ALTER TABLE [dbo].[Charact] CHECK CONSTRAINT [FK_CharactClassReference]
GO

