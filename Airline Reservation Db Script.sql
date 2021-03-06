USE [master]
GO
/****** Object:  Database [DBAirlineReservation]    Script Date: 23-11-2021 09:25:38 ******/
CREATE DATABASE [DBAirlineReservation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBAirlineReservation', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DBAirlineReservation.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBAirlineReservation_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DBAirlineReservation_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBAirlineReservation] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBAirlineReservation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBAirlineReservation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DBAirlineReservation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBAirlineReservation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBAirlineReservation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DBAirlineReservation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBAirlineReservation] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBAirlineReservation] SET  MULTI_USER 
GO
ALTER DATABASE [DBAirlineReservation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBAirlineReservation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBAirlineReservation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBAirlineReservation] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBAirlineReservation] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBAirlineReservation] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBAirlineReservation] SET QUERY_STORE = OFF
GO
USE [DBAirlineReservation]
GO
/****** Object:  Table [dbo].[BankDetails]    Script Date: 23-11-2021 09:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankDetails](
	[Bid] [int] NOT NULL,
	[cardholdername] [varchar](50) NULL,
	[cardtype] [varchar](8) NULL,
	[cardno] [varchar](16) NULL,
	[cvv] [varchar](3) NULL,
	[Balance] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[Bid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAdmin]    Script Date: 23-11-2021 09:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdmin](
	[adminemail] [varchar](255) NULL,
	[AdminPassword] [varchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFlightDetails]    Script Date: 23-11-2021 09:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFlightDetails](
	[FlightDetailsID] [int] IDENTITY(1,1) NOT NULL,
	[Flightid] [int] NULL,
	[Source] [varchar](30) NULL,
	[Destination] [varchar](30) NULL,
	[AvailableEconomySeats] [int] NULL,
	[AvailableBusinessSeats] [int] NULL,
	[EconomyclassFare] [money] NULL,
	[BusinessclassFare] [money] NULL,
	[DepatureDate] [date] NULL,
	[DepatureTime] [time](0) NULL,
PRIMARY KEY CLUSTERED 
(
	[FlightDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFlightMaster]    Script Date: 23-11-2021 09:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFlightMaster](
	[Flightid] [int] IDENTITY(1,1) NOT NULL,
	[Fname] [varchar](40) NULL,
	[economySeats] [int] NULL,
	[businessSeats] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Flightid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPassenger]    Script Date: 23-11-2021 09:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPassenger](
	[PassengerId] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[FlightDetailsID] [int] NULL,
	[PassengerName] [varchar](30) NULL,
	[Age] [int] NULL,
	[Seatno] [varchar](5) NULL,
	[tid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PassengerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tblticket]    Script Date: 23-11-2021 09:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tblticket](
	[tid] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NULL,
	[FlightDetailsID] [int] NULL,
	[classtype] [varchar](15) NULL,
	[TotalSeatsReserved] [int] NULL,
	[Type] [varchar](15) NULL,
	[ReturnDate] [varchar](30) NULL,
	[TotalAmount] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[tid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 23-11-2021 09:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[userid] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NULL,
	[LastName] [varchar](20) NULL,
	[Emailid] [varchar](255) NULL,
	[Password] [varchar](30) NULL,
	[confirmpassword] [varchar](30) NULL,
	[Dateofbirth] [date] NULL,
	[Phoneno] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tblAdmin] ([adminemail], [AdminPassword]) VALUES (N'Admin256@gmail.com', N'Admin@256*')
GO
SET IDENTITY_INSERT [dbo].[tblFlightDetails] ON 

INSERT [dbo].[tblFlightDetails] ([FlightDetailsID], [Flightid], [Source], [Destination], [AvailableEconomySeats], [AvailableBusinessSeats], [EconomyclassFare], [BusinessclassFare], [DepatureDate], [DepatureTime]) VALUES (9, 4, N'Kochi', N'Singapore', 31, 8, 10000.0000, 20000.0000, CAST(N'2021-12-08' AS Date), CAST(N'20:10:00' AS Time))
INSERT [dbo].[tblFlightDetails] ([FlightDetailsID], [Flightid], [Source], [Destination], [AvailableEconomySeats], [AvailableBusinessSeats], [EconomyclassFare], [BusinessclassFare], [DepatureDate], [DepatureTime]) VALUES (12, 7, N'Kochi', N'Singapore', 32, 12, 10000.0000, 20000.0000, CAST(N'2021-12-08' AS Date), CAST(N'21:10:00' AS Time))
INSERT [dbo].[tblFlightDetails] ([FlightDetailsID], [Flightid], [Source], [Destination], [AvailableEconomySeats], [AvailableBusinessSeats], [EconomyclassFare], [BusinessclassFare], [DepatureDate], [DepatureTime]) VALUES (13, 4, N'NewDelhi', N'Sydney', 33, 12, 8000.0000, 10000.0000, CAST(N'2021-12-12' AS Date), CAST(N'22:00:00' AS Time))
INSERT [dbo].[tblFlightDetails] ([FlightDetailsID], [Flightid], [Source], [Destination], [AvailableEconomySeats], [AvailableBusinessSeats], [EconomyclassFare], [BusinessclassFare], [DepatureDate], [DepatureTime]) VALUES (14, 4, N'NewDelhi', N'Sydney', 31, 12, 7000.0000, 10000.0000, CAST(N'2021-12-01' AS Date), CAST(N'23:00:00' AS Time))
INSERT [dbo].[tblFlightDetails] ([FlightDetailsID], [Flightid], [Source], [Destination], [AvailableEconomySeats], [AvailableBusinessSeats], [EconomyclassFare], [BusinessclassFare], [DepatureDate], [DepatureTime]) VALUES (16, 4, N'NewDelhi', N'Sydney', 12, 12, 10000.0000, 8800.0000, CAST(N'2022-01-08' AS Date), CAST(N'14:39:00' AS Time))
SET IDENTITY_INSERT [dbo].[tblFlightDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[tblFlightMaster] ON 

INSERT [dbo].[tblFlightMaster] ([Flightid], [Fname], [economySeats], [businessSeats]) VALUES (4, N'Go Air', 34, 12)
INSERT [dbo].[tblFlightMaster] ([Flightid], [Fname], [economySeats], [businessSeats]) VALUES (7, N'Air India', 34, 12)
INSERT [dbo].[tblFlightMaster] ([Flightid], [Fname], [economySeats], [businessSeats]) VALUES (10, N'Air jet', 43, 23)
INSERT [dbo].[tblFlightMaster] ([Flightid], [Fname], [economySeats], [businessSeats]) VALUES (11, N'Air way', 40, 13)
INSERT [dbo].[tblFlightMaster] ([Flightid], [Fname], [economySeats], [businessSeats]) VALUES (12, N'Indigo', 43, 13)
INSERT [dbo].[tblFlightMaster] ([Flightid], [Fname], [economySeats], [businessSeats]) VALUES (13, N'Air', 23, 12)
INSERT [dbo].[tblFlightMaster] ([Flightid], [Fname], [economySeats], [businessSeats]) VALUES (14, N'Air India', 23, 12)
SET IDENTITY_INSERT [dbo].[tblFlightMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[tblPassenger] ON 

INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (68, 2, 9, N'Ananthi', 23, N'25', 61)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (69, 2, 9, N'Saranya', 23, N'26', 61)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (82, 3, 9, N'Sathya', 24, N'1', 68)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (83, 3, 9, N'Ramya', 23, N'2', 68)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (86, 1, 12, N'Ananth', 23, N'4', 70)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (87, 1, 12, N'Ramya', 12, N'5', 70)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (91, 11, 9, N'Siva', 45, N'3', 74)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (92, 11, 13, N'Ram', 45, N'1', 75)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (93, 13, 14, N'Strelina', 22, N'1', 76)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (94, 13, 14, N'Smilin', 18, N'2', 76)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (95, 1, 14, N'Sathya', 23, N'5', 77)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (97, 2, 9, N'Saranya', 23, N'4', 79)
INSERT [dbo].[tblPassenger] ([PassengerId], [userid], [FlightDetailsID], [PassengerName], [Age], [Seatno], [tid]) VALUES (98, 2, 9, N'Ramya', 23, N'5', 79)
SET IDENTITY_INSERT [dbo].[tblPassenger] OFF
GO
SET IDENTITY_INSERT [dbo].[Tblticket] ON 

INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (61, 2, 9, N'Economy', 2, N'Oneway', NULL, 20000.0000)
INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (68, 3, 9, N'Business', 2, N'Return', N'2021-12-12', 80000.0000)
INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (70, 1, 12, N'Economy', 2, N'Return', N'2021-12-10', 40000.0000)
INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (74, 11, 9, N'Economy', 1, N'Oneway', NULL, 10000.0000)
INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (75, 11, 13, N'Economy', 1, N'Oneway', NULL, 8000.0000)
INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (76, 13, 14, N'Economy', 2, N'Oneway', NULL, 14000.0000)
INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (77, 1, 14, N'Economy', 1, N'Return', N'2021-12-04', 14000.0000)
INSERT [dbo].[Tblticket] ([tid], [userid], [FlightDetailsID], [classtype], [TotalSeatsReserved], [Type], [ReturnDate], [TotalAmount]) VALUES (79, 2, 9, N'Business', 2, N'Return', N'2021-12-10', 80000.0000)
SET IDENTITY_INSERT [dbo].[Tblticket] OFF
GO
SET IDENTITY_INSERT [dbo].[tblUser] ON 

INSERT [dbo].[tblUser] ([userid], [FirstName], [LastName], [Emailid], [Password], [confirmpassword], [Dateofbirth], [Phoneno]) VALUES (1, N'Sathya', N'Priya', N'Sathyapriya@gmail.com', N'Sathya@5*', N'Sathya@5*', CAST(N'2000-03-15' AS Date), N'8934557865')
INSERT [dbo].[tblUser] ([userid], [FirstName], [LastName], [Emailid], [Password], [confirmpassword], [Dateofbirth], [Phoneno]) VALUES (2, N'Saranya', N'Prem', N'Saranya@gmail.com', N'Saranya@5*', N'Saranya@5*', CAST(N'1999-05-19' AS Date), N'8922334334')
INSERT [dbo].[tblUser] ([userid], [FirstName], [LastName], [Emailid], [Password], [confirmpassword], [Dateofbirth], [Phoneno]) VALUES (3, N'Kavya', N'Madhu', N'Kaviya@gmail.com', N'Kavya@5*', N'Kavya@5*', CAST(N'1998-06-10' AS Date), N'8923229908')
INSERT [dbo].[tblUser] ([userid], [FirstName], [LastName], [Emailid], [Password], [confirmpassword], [Dateofbirth], [Phoneno]) VALUES (11, N'Siva', N'Ranjini', N'Siva@gmail', N'SivaRanjini@5*', N'SivaRanjini@5*', CAST(N'2021-11-18' AS Date), N'9078563456')
INSERT [dbo].[tblUser] ([userid], [FirstName], [LastName], [Emailid], [Password], [confirmpassword], [Dateofbirth], [Phoneno]) VALUES (13, N'Strelina', N'Milin', N'Strelina@gmail.com', N'Strelina@5*', N'Strelina@5*', CAST(N'2021-11-19' AS Date), N'9867543456')
INSERT [dbo].[tblUser] ([userid], [FirstName], [LastName], [Emailid], [Password], [confirmpassword], [Dateofbirth], [Phoneno]) VALUES (15, N'Prabha', N'Prem', N'prabha@gmail.com', N'Prabha@5*', N'Prabha@5*', CAST(N'2021-11-23' AS Date), N'9075346789')
SET IDENTITY_INSERT [dbo].[tblUser] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tblUser__7EDA1EE6904BE17A]    Script Date: 23-11-2021 09:25:39 ******/
ALTER TABLE [dbo].[tblUser] ADD UNIQUE NONCLUSTERED 
(
	[Emailid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tblUser__F2111EDA9C5636EB]    Script Date: 23-11-2021 09:25:39 ******/
ALTER TABLE [dbo].[tblUser] ADD UNIQUE NONCLUSTERED 
(
	[Phoneno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblFlightDetails]  WITH CHECK ADD FOREIGN KEY([Flightid])
REFERENCES [dbo].[tblFlightMaster] ([Flightid])
GO
ALTER TABLE [dbo].[tblPassenger]  WITH CHECK ADD FOREIGN KEY([FlightDetailsID])
REFERENCES [dbo].[tblFlightDetails] ([FlightDetailsID])
GO
ALTER TABLE [dbo].[tblPassenger]  WITH CHECK ADD FOREIGN KEY([userid])
REFERENCES [dbo].[tblUser] ([userid])
GO
ALTER TABLE [dbo].[tblPassenger]  WITH CHECK ADD FOREIGN KEY([tid])
REFERENCES [dbo].[Tblticket] ([tid])
GO
ALTER TABLE [dbo].[Tblticket]  WITH CHECK ADD FOREIGN KEY([FlightDetailsID])
REFERENCES [dbo].[tblFlightDetails] ([FlightDetailsID])
GO
ALTER TABLE [dbo].[Tblticket]  WITH CHECK ADD FOREIGN KEY([userid])
REFERENCES [dbo].[tblUser] ([userid])
GO
USE [master]
GO
ALTER DATABASE [DBAirlineReservation] SET  READ_WRITE 
GO
