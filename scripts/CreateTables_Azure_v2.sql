USE [Swiss]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/20/2012 08:49:17 ******/
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/20/2012 09:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CountryId] [int] NULL,
	[Birthday] [date] NULL,
	[CreatedDate] [date] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 10/20/2012 01:19:13 ******/
DROP TABLE [dbo].[UserType]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 10/20/2012 01:25:57 ******/
CREATE TABLE [dbo].[UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[User_UserType]    Script Date: 10/20/2012 11:36:41 ******/
DROP TABLE [dbo].[User_UserType]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 10/20/2012 12:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_User_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Master]    Script Date: 10/20/2012 11:36:41 ******/
DROP TABLE [dbo].[Master]
GO
/****** Object:  Table [dbo].[Master]    Script Date: 10/20/2012 12:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nVarchar] (100) NOT NULL,
	[Size] [int] NOT NULL,
	[Expiration_Time] [int] NOT NULL,
	[Price] [Numeric] (18,2) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Master] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Status]    Script Date: 10/20/2012 11:36:41 ******/
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 10/20/2012 12:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nVarchar] (100) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Configuration]    Script Date: 10/20/2012 11:36:41 ******/
DROP TABLE [dbo].[Configuration]
GO
/****** Object:  Table [dbo].[Configuration]    Script Date: 10/20/2012 12:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nVarchar] (100) NOT NULL,
	[Valor] [int] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Message]    Script Date: 10/20/2012 11:36:41 ******/
DROP TABLE [dbo].[Message]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 10/20/2012 12:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nVarchar] (500) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Log]    Script Date: 10/20/2012 11:36:41 ******/
DROP TABLE [dbo].[Log]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 10/20/2012 12:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[MessageId] [int] NOT NULL,
	[Detailed] [nVarchar] (500) NOT NULL,
	[CreatedDate] [Date] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[Country]    Script Date: 10/20/2012 11:36:41 ******/
DROP TABLE [dbo].[Country]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 10/20/2012 12:14:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
SET IDENTITY_INSERT [dbo].[Country] ON
INSERT [dbo].[Country] ([Id], [Name]) VALUES (1, N'AFGHANISTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (2, N'ALAND ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (3, N'ALBANIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (4, N'ALGERIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (5, N'AMERICAN SAMOA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (6, N'ANDORRA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (7, N'ANGOLA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (8, N'ANGUILLA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (9, N'ANTARCTICA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (10, N'ANTIGUA AND BARBUDA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (11, N'ARGENTINA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (12, N'ARMENIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (13, N'ARUBA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (14, N'AUSTRALIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (15, N'AUSTRIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (16, N'AZERBAIJAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (17, N'BAHAMAS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (18, N'BAHRAIN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (19, N'BANGLADESH')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (20, N'BARBADOS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (21, N'BELARUS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (22, N'BELGIUM')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (23, N'BELIZE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (24, N'BENIN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (25, N'BERMUDA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (26, N'BHUTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (27, N'BOLIVIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (28, N'BONAIRE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (29, N'BOSNIA AND HERZEGOVINA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (30, N'BOTSWANA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (31, N'BOUVET ISLAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (32, N'BRAZIL')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (33, N'BRITISH INDIAN OCEAN TERRITORY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (34, N'BRUNEI DARUSSALAM')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (35, N'BULGARIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (36, N'BURKINA FASO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (37, N'BURUNDI')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (38, N'CAMBODIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (39, N'CAMEROON')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (40, N'CANADA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (41, N'CAPE VERDE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (42, N'CAYMAN ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (43, N'CENTRAL AFRICAN REPUBLIC')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (44, N'CHAD')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (45, N'CHILE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (46, N'CHINA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (47, N'CHRISTMAS ISLAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (48, N'COCOS (KEELING) ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (49, N'COLOMBIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (50, N'COMOROS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (51, N'CONGO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (52, N'CONGO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (53, N'COOK ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (54, N'COSTA RICA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (55, N'COTE D''IVOIRE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (56, N'CROATIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (57, N'CUBA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (58, N'CURACAO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (59, N'CYPRUS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (60, N'CZECH REPUBLIC')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (61, N'DENMARK')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (62, N'DJIBOUTI')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (63, N'DOMINICA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (64, N'DOMINICAN REPUBLIC')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (65, N'ECUADOR')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (66, N'EGYPT')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (67, N'EL SALVADOR')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (68, N'EQUATORIAL GUINEA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (69, N'ERITREA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (70, N'ESTONIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (71, N'ETHIOPIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (72, N'FALKLAND ISLANDS (MALVINAS)')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (73, N'FAROE ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (74, N'FIJI')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (75, N'FINLAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (76, N'FRANCE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (77, N'FRENCH GUIANA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (78, N'FRENCH POLYNESIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (79, N'FRENCH SOUTHERN TERRITORIES')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (80, N'GABON')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (81, N'GAMBIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (82, N'GEORGIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (83, N'GERMANY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (84, N'GHANA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (85, N'GIBRALTAR')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (86, N'GREECE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (87, N'GREENLAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (88, N'GRENADA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (89, N'GUADELOUPE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (90, N'GUAM')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (91, N'GUATEMALA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (92, N'GUERNSEY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (93, N'GUINEA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (94, N'GUINEA-BISSAU')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (95, N'GUYANA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (96, N'HAITI')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (97, N'HEARD ISLAND AND MCDONALD ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (98, N'HOLY SEE (VATICAN CITY STATE)')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (99, N'HONDURAS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (100, N'HONG KONG')
GO
print 'Processed 100 total records'
INSERT [dbo].[Country] ([Id], [Name]) VALUES (101, N'HUNGARY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (102, N'ICELAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (103, N'INDIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (104, N'INDONESIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (105, N'IRAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (106, N'IRAQ')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (107, N'IRELAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (108, N'ISLE OF MAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (109, N'ISRAEL')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (110, N'ITALY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (111, N'JAMAICA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (112, N'JAPAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (113, N'JERSEY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (114, N'JORDAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (115, N'KAZAKHSTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (116, N'KENYA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (117, N'KIRIBATI')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (118, N'KOREA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (119, N'KOREA (NORTH)')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (120, N'KUWAIT')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (121, N'KYRGYZSTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (122, N'LAOS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (123, N'LATVIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (124, N'LEBANON')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (125, N'LESOTHO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (126, N'LIBERIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (127, N'LIBYA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (128, N'LIECHTENSTEIN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (129, N'LITHUANIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (130, N'LUXEMBOURG')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (131, N'MACAO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (132, N'MACEDONIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (133, N'MADAGASCAR')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (134, N'MALAWI')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (135, N'MALAYSIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (136, N'MALDIVES')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (137, N'MALI')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (138, N'MALTA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (139, N'MARSHALL ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (140, N'MARTINIQUE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (141, N'MAURITANIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (142, N'MAURITIUS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (143, N'MAYOTTE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (144, N'MEXICO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (145, N'MICRONESIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (146, N'MOLDOVA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (147, N'MONACO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (148, N'MONGOLIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (149, N'MONTENEGRO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (150, N'MONTSERRAT')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (151, N'MOROCCO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (152, N'MOZAMBIQUE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (153, N'MYANMAR')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (154, N'NAMIBIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (155, N'NAURU')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (156, N'NEPAL')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (157, N'NETHERLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (158, N'NEW CALEDONIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (159, N'NEW ZEALAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (160, N'NICARAGUA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (161, N'NIGER')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (162, N'NIGERIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (163, N'NIUE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (164, N'NORFOLK ISLAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (165, N'NORTHERN MARIANA ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (166, N'NORWAY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (167, N'OMAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (168, N'PAKISTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (169, N'PALAU')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (170, N'PALESTINIAN TERRITORY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (171, N'PANAMA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (172, N'PAPUA NEW GUINEA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (173, N'PARAGUAY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (174, N'PERU')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (175, N'PHILIPPINES')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (176, N'PITCAIRN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (177, N'POLAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (178, N'PORTUGAL')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (179, N'PUERTO RICO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (180, N'QATAR')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (181, N'RÉUNION')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (182, N'ROMANIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (183, N'RUSSIAN FEDERATION')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (184, N'RWANDA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (185, N'SAINT BARTHELEMY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (186, N'SAINT HELENA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (187, N'SAINT KITTS AND NEVIS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (188, N'SAINT LUCIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (189, N'SAINT MARTIN (FRENCH PART)')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (190, N'SAINT PIERRE AND MIQUELON')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (191, N'SAINT VINCENT AND THE GRENADINES')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (192, N'SAMOA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (193, N'SAN MARINO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (194, N'SAO TOME AND PRINCIPE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (195, N'SAUDI ARABIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (196, N'SENEGAL')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (197, N'SERBIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (198, N'SEYCHELLES')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (199, N'SIERRA LEONE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (200, N'SINGAPORE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (201, N'SINT MAARTEN (DUTCH PART)')
GO
print 'Processed 200 total records'
INSERT [dbo].[Country] ([Id], [Name]) VALUES (202, N'SLOVAKIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (203, N'SLOVENIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (204, N'SOLOMON ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (205, N'SOMALIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (206, N'SOUTH AFRICA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (207, N'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (208, N'SOUTH SUDAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (209, N'SPAIN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (210, N'SRI LANKA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (211, N'SUDAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (212, N'SURINAME')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (213, N'SVALBARD AND JAN MAYEN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (214, N'SWAZILAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (215, N'SWEDEN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (216, N'SWITZERLAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (217, N'SYRIAN ARAB REPUBLIC')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (218, N'TAIWAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (219, N'TAJIKISTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (220, N'TANZANIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (221, N'THAILAND')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (222, N'TIMOR-LESTE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (223, N'TOGO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (224, N'TOKELAU')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (225, N'TONGA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (226, N'TRINIDAD AND TOBAGO')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (227, N'TUNISIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (228, N'TURKEY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (229, N'TURKMENISTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (230, N'TURKS AND CAICOS ISLANDS')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (231, N'TUVALU')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (232, N'UGANDA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (233, N'UKRAINE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (234, N'UNITED ARAB EMIRATES')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (235, N'UNITED KINGDOM')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (236, N'UNITED STATES')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (237, N'URUGUAY')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (238, N'UZBEKISTAN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (239, N'VANUATU')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (240, N'VENEZUELA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (241, N'VIET NAM')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (242, N'VIRGIN ISLANDS (BRITISH)')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (243, N'VIRGIN ISLANDS (U.S.)')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (244, N'WALLIS AND FUTUNA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (245, N'WESTERN SAHARA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (246, N'YEMEN')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (247, N'ZAMBIA')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (248, N'ZIMBABWE')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (249, N'OTHER')
SET IDENTITY_INSERT [dbo].[Country] OFF
/****** Object:  Table [dbo].[Bank]    Script Date: 10/20/2012 01:19:13 ******/
DROP TABLE [dbo].[Bank]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 10/20/2012 16:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankId] [int] NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[BankAccount]    Script Date: 10/20/2012 01:19:13 ******/
DROP TABLE [dbo].[BankAccount]
GO
/****** Object:  Table [dbo].[BankAccount]    Script Date: 10/20/2012 16:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BankId] [int] NOT NULL,
	[BankAccountNumber] [int] NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_BankAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[DigitalAccount]    Script Date: 10/20/2012 01:19:13 ******/
DROP TABLE [dbo].[DigitalAccount]
GO
/****** Object:  Table [dbo].[DigitalAccount]    Script Date: 10/20/2012 16:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DigitalAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Website] [nvarchar] (100) NOT NULL,
	[Username] [nvarchar] (100) NOT NULL,
	[Password] [nvarchar] (100) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_DigitalAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)
GO


