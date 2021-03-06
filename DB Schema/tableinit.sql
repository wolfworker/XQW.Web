USE [XQW_Data]
GO
/****** Object:  Table [dbo].[ACategory]    Script Date: 2020/7/22 19:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACategory](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ACategoryID] [varchar](20) NULL,
	[ACategoryName] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedUser] [varchar](20) NULL,
	[UpdateUser] [varchar](20) NULL,
 CONSTRAINT [PK_ACategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BCategory]    Script Date: 2020/7/22 19:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BCategory](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BCategoryID] [varchar](20) NULL,
	[BCategoryName] [nvarchar](50) NULL,
	[ACategoryID] [varchar](20) NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedUser] [varchar](20) NULL,
	[UpdateUser] [varchar](20) NULL,
 CONSTRAINT [PK_BCategoryID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2020/7/22 19:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductID] [nvarchar](50) NOT NULL,
	[NickName] [nvarchar](50) NOT NULL,
	[ContactNo] [nvarchar](50) NOT NULL,
	[CommentContent] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedUser] [varchar](20) NOT NULL,
	[UpdateUser] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2020/7/22 19:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductID] [varchar](50) NULL,
	[ProductName] [nvarchar](100) NULL,
	[ProductTitle] [nvarchar](500) NULL,
	[ProductSubTtile] [nvarchar](500) NULL,
	[LikeCount] [int] NULL,
	[CommentCount] [int] NULL,
	[SeenCount] [int] NULL,
	[DetailPicCount] [int] NULL,
	[BCategoryID] [varchar](20) NULL,
	[Status] [bit] NULL,
	[CreatedTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedUser] [varchar](20) NULL,
	[UpdateUser] [varchar](20) NULL,
	[IsHot] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SYS_DebugLog]    Script Date: 2020/7/22 19:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_DebugLog](
	[ID] [varchar](36) NOT NULL,
	[LogLevel] [tinyint] NULL,
	[LogContent] [text] NULL,
	[Remark] [nvarchar](100) NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_SYS_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SYS_RequestLog]    Script Date: 2020/7/22 19:52:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SYS_RequestLog](
	[ID] [varchar](36) NOT NULL,
	[UserID] [bigint] NULL,
	[LogType] [varchar](50) NULL,
	[LogTypeName] [nvarchar](50) NULL,
	[BussiessValue] [text] NULL,
	[Remark] [nvarchar](100) NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[UserIp] [varchar](50) NULL,
	[UserCityName] [nvarchar](50) NULL,
 CONSTRAINT [PK_SYS_RequestLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ACategory] ADD  CONSTRAINT [DF_ACategory_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[ACategory] ADD  CONSTRAINT [DF_ACategory_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO
ALTER TABLE [dbo].[BCategory] ADD  CONSTRAINT [DF_BCategory_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[BCategory] ADD  CONSTRAINT [DF_BCategory_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_DetailPicCount]  DEFAULT ((0)) FOR [DetailPicCount]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_UpdateTime]  DEFAULT (getdate()) FOR [UpdateTime]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_IsHot]  DEFAULT ((0)) FOR [IsHot]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否热门   1：热门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'IsHot'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志等级   1：错误  2：警告  3：正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_DebugLog', @level2type=N'COLUMN',@level2name=N'LogLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_DebugLog', @level2type=N'COLUMN',@level2name=N'LogContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注（用于拓展字段）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_DebugLog', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_DebugLog', @level2type=N'COLUMN',@level2name=N'CreatedUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_DebugLog', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_DebugLog', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_DebugLog', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键 GUID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'LogType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'LogTypeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注（用于拓展字段）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人（存用户id）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'CreatedUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人（存用户id）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'UpdateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SYS_RequestLog', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
