/* Check if database already exists and delete it if it does exist*/
use master
go
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'GreenThumbGardens') 
BEGIN

DROP DATABASE GreenThumbGardens;
END;
GO

CREATE DATABASE GreenThumbGardens;
GO

USE GreenThumbGardens;
GO

SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

SET ANSI_PADDING ON;
GO


/**********************************************************************************/
/******************************* Schemas ******************************************/
/**********************************************************************************/


create schema Admin;
go
create schema Donations;
go
create schema Expert;
go
create schema Gardens;
go
create schema Needs;
go


/**********************************************************************************/
/******************************* Tables *******************************************/
/**********************************************************************************/

create table Admin.ActivityLog(
	ActivityLogID int identity(1000,1) primary Key not null,
	UserID int not null,
	Date smalldatetime not null,
	LogEntry varchar(250) not null,
	UserAction varchar(100) null
);
go

--modified by trent cullinan 3-24-2016
CREATE TABLE [Admin].[ExpertRequests](
	RequestID		INT IDENTITY(1000,1) 		NOT NULL primary key,
	UserID			INT							NOT NULL,
	Title			VARCHAR(20)					NOT NULL,
	Content			VARCHAR(MAX)				NOT NULL,
	DateCreated		datetime					NOT NULL DEFAULT GETDATE(),
	DateModified	datetime					NULL,
	ModifiedBy		INT							NULL,
	Active			BIT							NOT NULL DEFAULT 1,
	Approved		BIT							NULL
);

-- Poonam Dubey 04/06/2016 -- Made changes to GroupRequest table --
-- TABLE UPDATED BY TREVOR GLISCH 4/7/16
create TABLE [Admin].[GroupRequest](
	[GroupID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[RequestStatus] BIT default 1 NOT NULL,
	[RequestDate] [smalldatetime] NOT NULL,
	[ApprovedDate] [smalldatetime] NULL,
	[ApprovedBy] [int] NULL,
	[Added] bit not null default 0
	CONSTRAINT [PK_GroupRequest] PRIMARY KEY ( Groupid, Userid ASC )
	);
GO


--updated by Ryan Taylor 4-14-16
create table Admin.Messages(
	MessageID int identity(1000,1) primary key not null,
	MessageContent varchar(250) not null,
	MessageDate smalldatetime not null default GETDATE(),
	Subject varchar(50) not null,
	Sender varchar(20) not null,
	Receiver varchar(20) not null,
	Unread bit not null default 1,
	SenderDeleted bit not null default 0,
	ReceiverDeleted bit not null default 0
);
go

create table Admin.Regions(
	RegionID int not null primary key,
	SoilType varchar(20) null,
	AverageTempSummer decimal null,
	AverageTempFall decimal null,
	AverageTempWinter decimal null,
	AverageTempSpring decimal null,
	AverageRainfall decimal null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null	  
);
go

create table Admin.Roles(
	RoleID varchar(30) primary key not null,
	Description varchar(100) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null

);
go

Create table Admin.UserRoles(
	UserID int not null,
	RoleID varchar(30) not null,
	CreatedBy int  null,
	CreatedDate smalldatetime  null,
	Active bit default 1 not null

	CONSTRAINT [PK_UserRoles] PRIMARY KEY ( UserID, RoleID ASC )
);

create table Admin.Users(
	UserID int identity(1000,1) primary key not null,
	FirstName varchar(50) not null,
	LastName varchar(100) not null,
	Zip char(9) null,
	EmailAddress varchar(100) null,
	UserName varchar(20) not null,
	PassWord varchar(150) default 'NEWUSER' not null,
	Active bit not null default 1,
	RegionID int null
	
	CONSTRAINT ck_UserName UNIQUE(UserName) 
);

--updated by Chris Schwebach 2-19-2016
create table Donations.EquipmentDonated(
	EquipmentDonatedID int identity(1000,1) not null primary key, 
	EquipmentName varchar(50) not null,
	EquipmentQuantity int not null,
	DateDonated smalldatetime not null,
	UserID int not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.EquipmentNeeded(
	EquipmentNeededID int identity(1000,1) not null primary key,
	EquipmentName varchar(50) not null,
	EquipmentQuantity int not null,
	DateDonated smalldatetime not null,
	UserID int not null,
	GroupID int not null,
	ReceivingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.EquipmentPendingTrans(
	EquipmentDonatedID int not null, 
	EquipmentNeededID int not null,
	Date smalldatetime not null,
	UserID int not null,
	GroupID int not null,
	Active bit not null default 1
);

create table Donations.LandDonated(
	TransactionNum int identity(1000,1) not null primary key,
	UserID int not null,
	Size int not null,
	Address varchar(100) not null,
	City varchar(30) null,
	state char(2) null,
	zip char(9) null,
	Notes varchar(255) null,
	DateDonated smalldatetime not null,
	Active bit not null default 1 
);

--updated by Chris Schwebach 2-19-2016
create table Donations.LandNeeded(
	LandIdentifier int identity(1000,1) not null primary key,
	UserID int not null,
	DateNeeded smalldatetime not null,
	DateRequested smalldatetime null,
	Notes varchar(255) null,
	Zip varchar(9) null,
	City varchar(30) not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.LandPendingTrans(
	LandDonated int not null,
	LandNeeded int not null,
	DateCompleted smalldatetime null,
	Notes varchar(255) null,
	ExpirationDate smalldatetime null,
	TransDate smalldatetime not null,
	UserID int not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_LandPendingTrans] PRIMARY KEY ( LandDonated, LandNeeded ASC )
);

create table Donations.MoneyDonated(
	DonationID int identity(1000,1) primary key not null,
	UserID int not null,
	Location varchar(50) not null,
	Amount decimal not null,
	DateCreated smalldatetime not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.MoneyNeeded(
	NeedID int identity(1000,1) primary key not null,
	UserID int not null,
	Location varchar(50) not null,
	Amount decimal not null,
	DateCreated smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.MoneyPendingTrans(
	NeedID int not null,
	DonationID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_MoneyPendingTrans] PRIMARY KEY ( NeedID, DonationID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SeedsDonated(
	DonationID int identity(1000,1) primary key not null,
	UserID int not null,
	Quantity int not null,
	SeedType varchar(50) not null,
	Date smalldatetime not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SeedsNeeded(
	SeedsNeededID int identity(1000,1) primary key not null,
	UserID int not null,
	NeededAmount int not null,
	SeedType varchar(50) not null,
	Date smalldatetime not null,
	RecievingNotes varchar(255) not null,
	StateLocated char(2) not null,
	GroupID int not null,
	Active bit not null default 1
);


create table Donations.SeedsPendingTrans(
	SeedsDonatedID int not null,
	SeedsNeededID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_SeedsPendingTrans] PRIMARY KEY ( SeedsDonatedID, SeedsNeededID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SoilDonated(
	SoilDonatedID int identity(1000,1) primary key not null,
	SoilType varchar(50) not null,
	UserID int not null,
	SoilName varchar(75) not null,
	Quantity int not null,
	Date smalldatetime not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SoilNeeded(
	SoilNeededID int identity(1000,1) primary key not null,
	SoilType varchar(50) not null,
	UserID int not null,
	SoilName varchar(75) not null,
	Quantity int not null,
	Date smalldatetime not null,
	RecievingNotes varchar(255) not null,
	StateLocated char(2) not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SoilPendingTrans(
	SoilNeededID int not null,
	SoilDonatedID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_SoilPendingTrans] PRIMARY KEY ( SoilNeededID, SoilDonatedID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SupplyDonated(
	SupplyDonatedID int identity(1000,1) not null primary key,
	userID int not null,
	SupplyName varchar(50) not null,
	SupplyAmount decimal not null,
	Date smalldatetime not null,
	ShippingNotes varchar(255) not null,
	StateLocated char(2) not null,
	active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SupplyNeeded(
	SupplyNeededID int identity(1000,1) not null primary key,
	userID int not null,
	SupplyName varchar(50) not null,
	SupplyAmount decimal not null,
	Date smalldatetime not null,
	RecievingNotes varchar(255) not null,
	StateLocated char(2) not null,
	GroupID int not null,
	active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.SupplyPendingTrans(
	SupplyNeededID int not null,
	SupplyDonatedID int not null,
	UserID int not null,
	Date smalldatetime not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_SupplyPendingTrans] PRIMARY KEY ( SupplyNeededID, SupplyDonatedID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.TimeNeeded(
	TimeNeededID int identity(1000,1) not null primary key,
	UserID int not null, 
	DateNeeded smalldatetime not null,
	GardenAffiliation varchar(50) null,
	Location char(9) null,
	Date smalldatetime not null,
	CityGardenLocated varchar(30) not null,
	GroupID int not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.TimePledge(
	TimePledgeID int identity(1000,1) not null primary key,
	UserID int not null,
	StartTime smalldatetime null,
	FinishTime smalldatetime null,
	DatePledge smalldatetime null,
	Affiliation varchar(75) null,
	Location char(9) null,
	Date smalldatetime not null,
	CityPledging varchar(30) not null,
	Active bit not null default 1
);

--updated by Chris Schwebach 2-19-2016
create table Donations.TimePledgeTrans(
	TimePledgeID int not null,
	TimeNeededID int not null,
	DateMatched smalldatetime not null,
	City varchar(30) not null,
	GroupID int not null,
	Active bit not null default 1
	
	CONSTRAINT [PK_TimePledgeTrans] PRIMARY KEY ( TimePledgeID, TimeNeededID ASC )
);

--updated by Chris Schwebach 2-19-2016
create table Donations.VolunteerHours(
	UserID int not null,
	Date smalldatetime not null,
	HoursVolunteered int not null,
	City varchar(30) not null

	CONSTRAINT [PK_VolunteerHours] PRIMARY KEY ( UserID, Date ASC )
);

create table Expert.BecomeAnExpert(
	RequestNo int identity(1000,1) not null primary key,
	Username int not null,
	WhyShouldIBeAnExpert varchar(200) not null,
	ApprovedBy int null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	Active bit not null default 1
);

create table Expert.BlogEntry(
	BlogID int identity(1000,1) not null primary key,
	BlogTitle varchar(200) not null,
	BlogData varchar(max) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	active bit not null default 1
);

create table Expert.Blueprints(
	BlueprintID int identity(1000,1) not null,
	Title varchar (20) not null,
	Description varchar(200) not null,
	DateCreated smalldatetime not null,
	CreatedBy int null,
	FilePath varchar(150) not null
);

create table Expert.Content(
	ContentID int identity(1000,1) not null primary key,
	UserID int not null,
	RegionID int null,
	Title varchar(50) not null,
	Category varchar(50) null,
	Content varchar(max) not null,
	Date smalldatetime not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.Expertise(
	GardenTypeID Varchar(20) not null,
	RegionID int null,
	Content varchar(max) not null,
	CreatedDate smalldatetime not null,
	ModifiedDate smalldatetime null,
	CreatedBy int not null,
	ModifiedBy int null,
	ExpertID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_Expertise] PRIMARY KEY ( GardenTypeID, ExpertID ASC )
);





create table Expert.GardenNotifications(
	GardenID int not null,
	NotificationID int not null,
	TriggerDate smalldatetime null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null

	CONSTRAINT [PK_GardenNotifications] PRIMARY KEY ( GardenID, NotificationID ASC )
);

create table Expert.GardenPlants(
	GardenID int not null,
	PlantID int not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	Quantity int not null

	CONSTRAINT [PK_GardenPlants] PRIMARY KEY ( GardenID, PlantID ASC )
);

CREATE TABLE Expert.GardenTemplateUploads(
	ImageName VARCHAR(50) NOT NULL PRIMARY KEY,
	CreatedBy INT NOT NULL,
	CreateDate SMALLDATETIME NOT NULL,
	Active BIT NOT NULL DEFAULT 1
);

CREATE TABLE Expert.GardenTemplateFiles(
	ImageName VARCHAR(50) NOT NULL PRIMARY KEY,
	ImageFile VARBINARY(MAX) NOT NULL
);

create table Expert.GardenTypes(
	GardentypeID int identity(1000,1) not null primary key,
	Description varchar(255) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.Notifications(
	NotificationID int primary key identity(1000,1) not null,
	Type varchar(50) not null,
	Description varchar(255) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	Active bit not null default 1
);

create table Expert.Plants(
	PlantID int identity(1000,1) not null primary key,
	Name varchar(100) not null,
	Type varchar(100) not null,
	Category varchar(30) not null,
	Description Varchar(255) not null,
	Season Varchar(10) null,
	CreatedBy int not null, 
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null,
	active bit not null default 1
);

create table Expert.PlantCategory(
	CategoryName varchar(30) not null primary key,
	CreatedBy int not null,
	Date smalldatetime not null,
	Active bit not null default 1
);

create table Expert.PlantNutrients(
	PlantID int not null,
	NutrientID int not null

	CONSTRAINT [PK_PlantNutrients] PRIMARY KEY ( PlantID, NutrientID ASC )
);

create table Expert.PlantRegions(
	PlantID int not null,
	RegionID int not null

	CONSTRAINT [PK_PlantRegions] PRIMARY KEY ( PlantID, RegionID ASC )
);

create table Expert.Nutrients(
	NutrientID int identity(1000,1) not null primary key,
	Name varchar(100) not null,
	Description varchar(255) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.Question(
	QuestionID int not null identity(1000,1) primary key,
	Title varchar(50) not null,
	Category varchar(50) null,
	Content varchar(max) not null,
	RegionID int null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

--updated by rhett 4-29-16
create table Expert.QuestionResponse(
	QuestionID int not null,
	Date smalldatetime not null,
	Response varchar(250) not null,
	UserID int not null,
	BlogID int	null

	CONSTRAINT [PK_QuestionResponse] PRIMARY KEY ( QuestionID, UserID ASC )
);


create table Expert.Recipes(
	RecipeID int identity(1000,1) not null primary key,
	Title varchar(50) not null,
	Category varchar(30) not null,
	Directions varchar(max) not null,
	CreatedBy int not null,
	CreatedDate smalldatetime not null,
	ModifiedBy int null,
	ModifiedDate smalldatetime null
);

create table Expert.RecipeCategory(
	CategoryName varchar(30) not null primary key,
	CreatedBy int not null,
	Date smalldatetime not null,
	Active bit not null default 1
);

create table Expert.Templates(
	TemplateID int identity(1000,1) not null primary key,
	UserID int not null,
	Description varchar(max) not null,
	DateCreated smalldatetime not null,
	Active bit not null default 1
);

create table Expert.Template(
	TemplateID int identity(1000,1) not null,
	Title varchar (20) not null,
	Description varchar(200) not null,
	DateCreated smalldatetime not null,
	CreatedBy int null,
	FilePath varchar(150) not null
);


--created by Dat Tran 2-25-16
--updated by luke frahm 4-22-16
CREATE TABLE Gardens.Announcements (
	AnnouncementID 			INT 					NOT NULL				PRIMARY KEY 			IDENTITY(1000,1),
	UserName				VARCHAR(20)				NOT NULL,
	FirstName				VARCHAR(50)				NOT NULL,
	LastName				VARCHAR(100)			NOT NULL,
	GroupID					INT						NOT NULL,
	Date 					SMALLDATETIME 			NOT NULL,
	Announcement			VARCHAR(MAX) 			NOT NULL
);
GO

--modified by team king 4-7-16
create table Gardens.Gardens(
	GardenID int identity(1000,1) not null primary key,
	GroupID int not null,
	UserID int not null,
	GardenName varchar(100) not null,
	GardenDescription varchar(max) not null,
	GardenRegion varchar(25) null,
	Active bit not null default 1
);

Create table Gardens.GardenGuides(
	GardenGuideID int identity(1000,1) not null primary key,
	UserID int not null,
	Content varchar(max) not null
);

create table Gardens.GroupLeaders(
	UserID int not null,
	GroupID int not null,
	Active bit not null default 1

	CONSTRAINT [PK_GroupLeaders] PRIMARY KEY ( UserID, GroupID ASC )
);

--added by nick king 9-4-16
create table Gardens.GroupLeaderRequests(
	RequestID int identity(1000,1) primary key not null,
	UserID int not null,
	GroupID int not null,
	RequestActive bit not null,
	RequestDate smalldatetime not null,
	ModifiedDate smalldatetime null,
	ModifiedBy int null	
);

--modified by trent 2-23-16
create table Gardens.GroupMembers(
	GroupID int not null,
	UserID int not null,
	CreatedDate smalldatetime not null,
	CreatedBy int not null,
	Leader bit default 0,
	active bit not null default 1

	CONSTRAINT [PK_GroupMembers] PRIMARY KEY ( GroupID, UserID ASC )
);

create table Gardens.GardenPlans(
	GardenPlanID int identity(1000,1) not null primary key,
	UserID int not null,
	Description varchar(max) not null,
	DateCreated smalldatetime not null,
	Active bit not null default 1
);

--Modified by Kristine 2-25-16 (add orgID field)
create table Gardens.Groups(
	GroupID int identity(1000,1) not null primary key, 
	GroupName varchar(100) not null,
	GroupLeaderID int not null,
	Active bit not null default 1,
	OrganizationID int null
);

--Modified by Chris Sheehan, removed Organization Contact varchar(100) added organizationLeader int should come from userID table 2-25-16
create table Gardens.Organizations(
	OrganizationID int identity(1000,1) primary key not null,
	OrganizationName varchar(100) not null, 
	OrganizationLeader int not null,
	ContactPhone char(10) null,
	Active bit not null default 1
);

create table Gardens.PostLineItems(
	PostID int not null,
	PostLineID int not null,
	UserID int not null,
	GroupID int not null,
	DateSent smalldatetime not null,
	CommentContent varchar(255) not null

	constraint [PK_PostLineItems] primary key ( PostID, PostLineID Asc)
);

create table Gardens.PostThreads(
	PostID int identity(1000,1) not null primary key,
	PostType varChar(50),
	GroupComments bit not null default 1,
	NoComments int null,
	ViewByAll bit not null default 1,
	UserID int not null,
	GroupID int not null,
	PostDateTime smalldatetime not null,
	Content varchar(max) not null,
	PostTitle varchar(100) not null,
	Active bit not null default 1
);

create table Gardens.Tasks(
	TaskID int identity(1000,1) not null primary key,
	GardenID int not null,
	Description varchar(100) not null,
	DateAssigned smalldatetime not null,
	DateCompleted smalldatetime null,
	AssignedTo int null,
	AssignedFrom int not null,
	userNotes varchar(250) null,
	Active bit not null default 1
);

create table Gardens.WorkLogs(
	WorkLogID int identity(1000,1) not null primary key, 
	UserID int not null,
	TaskID int not null,
	TimeBegun smalldatetime not null,
	TimeFinished smalldatetime null
);

--created by trent cullinan 5-5-16
create table Needs.NeedTypes(
	NeedType			varchar(50)		not null,
	Description			varchar(255)	not null,
	Active				bit				not null default 1
	constraint [pk_NeedTypes_NeedType] primary key(NeedType)
);

--created by trent cullinan 5-5-16
create table Needs.Needs(
	NeedID				int				not null identity(1000,1),
	GardenID			int				not null,
	Title				varchar(30)		not null,
	Description			varchar(255)	not null,
	NeedType			varchar(50)		not null default 'General',
	Completed			bit				not null default 0,
	DateCreated			smalldatetime	not null default getdate(),
	CreatedBy			int				not null,
	DateModified		smalldatetime	null,
	ModifiedBy			int				null,
	Active				bit				not null default 1,
	constraint [pk_Needs_NeedID] primary key(NeedID)
);

--created by trent cullinan 5-5-16
create table Needs.Contributions(
	ContributionID		int				not null identity(1000,1),
	NeedID				int				not null,
	SentBy				int				not null,
	Description			varchar(255)	null,
	Contributed			bit				null,
	DateCreated			smalldatetime	not null default getdate(),
	DateModified		smalldatetime	null,
	ModifiedBy			int				null,
	Active				bit				not null default 1
	constraint [pk_PendingContributions_ContributionID] primary key(ContributionID)
);
go


/**********************************************************************************/
/******************************* Foreign Keys *************************************/
/**********************************************************************************/


ALTER TABLE Admin.ActivityLog WITH NOCHECK ADD  CONSTRAINT [FK_ActivityLog_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.ActivityLog CHECK CONSTRAINT FK_ActivityLog_UserID;
GO

--added by trent cullinan 3-24-16
ALTER TABLE [Admin].[ExpertRequests] WITH NOCHECK
	ADD CONSTRAINT [FK_ExpertRequests_UserID] FOREIGN KEY ([UserID])
		REFERENCES [Admin].[Users] ([UserID]);
GO

--added by trent cullinan 3-24-16
ALTER TABLE [Admin].[ExpertRequests] WITH NOCHECK
	ADD CONSTRAINT [FK_ExpertRequests_ModifiedBy] FOREIGN KEY ([ModifiedBy])
		REFERENCES [Admin].[Users] ([UserID]);
GO
------------- Added by Poonam Dubey ----------------
--------------Dated : 04/06/2016--------------------
ALTER TABLE [Admin].[GroupRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_GroupRequest_approvedBy] FOREIGN KEY([ApprovedBy])
REFERENCES [Admin].[Users] ([UserID])
GO

ALTER TABLE [Admin].[GroupRequest] CHECK CONSTRAINT [FK_GroupRequest_approvedBy]
GO

ALTER TABLE [Admin].[GroupRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_GroupRequest_groupid] FOREIGN KEY([GroupID])
REFERENCES [Gardens].[Groups] ([GroupID])
GO

ALTER TABLE [Admin].[GroupRequest] CHECK CONSTRAINT [FK_GroupRequest_groupid]
GO


ALTER TABLE [Admin].[GroupRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_GroupRequest_UserID] FOREIGN KEY([UserID])
REFERENCES [Admin].[Users] ([UserID])
GO

ALTER TABLE [Admin].[GroupRequest] CHECK CONSTRAINT [FK_GroupRequest_UserID]
GO

--added by ryan taylor 4-14-16
ALTER TABLE Admin.Messages WITH NOCHECK ADD  CONSTRAINT [FK_Messages_Sender] FOREIGN KEY(Sender)
REFERENCES Admin.Users(Username);
GO
ALTER TABLE Admin.Messages CHECK CONSTRAINT [FK_Messages_Sender];
GO

--added by ryan taylor 4-14-16
ALTER TABLE Admin.Messages WITH NOCHECK ADD  CONSTRAINT [FK_Messages_Receiver] FOREIGN KEY(Receiver)
REFERENCES Admin.Users(Username);
GO
ALTER TABLE Admin.Messages CHECK CONSTRAINT [FK_Messages_Receiver];
GO

ALTER TABLE Admin.Regions WITH NOCHECK ADD  CONSTRAINT [FK_Regions_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.Regions CHECK CONSTRAINT [FK_Regions_CreatedBy];
GO

ALTER TABLE Admin.Regions WITH NOCHECK ADD  CONSTRAINT [FK_Regions_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.Regions CHECK CONSTRAINT [FK_Regions_ModifiedBy];
GO

ALTER TABLE Admin.UserRoles WITH NOCHECK ADD  CONSTRAINT [FK_UserRoles_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Admin.UserRoles CHECK CONSTRAINT [FK_UserRoles_UserID];
GO

ALTER TABLE Admin.Users WITH NOCHECK ADD  CONSTRAINT [FK_Users_RegionID] FOREIGN KEY(RegionID)
REFERENCES Admin.Regions(RegionID);
GO
ALTER TABLE Admin.Users CHECK CONSTRAINT [FK_Users_RegionID];
GO

ALTER TABLE Donations.EquipmentDonated WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.EquipmentDonated CHECK CONSTRAINT [FK_EquipmentDonated_UserID];
GO

ALTER TABLE Donations.EquipmentNeeded WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.EquipmentNeeded CHECK CONSTRAINT [FK_EquipmentNeeded_UserID];
GO

ALTER TABLE Donations.EquipmentPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentPendingTrans_EquipmentDonatedID] FOREIGN KEY(EquipmentDonatedID)
REFERENCES Donations.EquipmentDonated(EquipmentDonatedID);
GO
ALTER TABLE Donations.EquipmentPendingTrans CHECK CONSTRAINT [FK_EquipmentPendingTrans_EquipmentDonatedID];
GO

ALTER TABLE Donations.EquipmentPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentPendingTrans_EquipmentNeededID] FOREIGN KEY(EquipmentNeededID)
REFERENCES Donations.EquipmentNeeded(EquipmentNeededID);
GO
ALTER TABLE Donations.EquipmentPendingTrans CHECK CONSTRAINT [FK_EquipmentPendingTrans_EquipmentNeededID];
GO

ALTER TABLE Donations.EquipmentPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_EquipmentPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.EquipmentPendingTrans CHECK CONSTRAINT [FK_EquipmentPendingTrans_UserID];
GO

ALTER TABLE Donations.LandDonated WITH NOCHECK ADD  CONSTRAINT [FK_LandDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.LandDonated CHECK CONSTRAINT [FK_LandDonated_UserID];
GO

ALTER TABLE Donations.LandNeeded WITH NOCHECK ADD  CONSTRAINT [FK_LandNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.LandNeeded CHECK CONSTRAINT [FK_LandNeeded_UserID];
GO

ALTER TABLE Donations.LandPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_LandPendingTrans_LandDonated] FOREIGN KEY(LandDonated)
REFERENCES Donations.LandDonated(TransactionNum);
GO
ALTER TABLE Donations.LandPendingTrans CHECK CONSTRAINT [FK_LandPendingTrans_LandDonated];
GO

ALTER TABLE Donations.LandPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_LandPendingTrans_LandNeeded] FOREIGN KEY(LandNeeded)
REFERENCES Donations.LandNeeded(LandIdentifier);
GO
ALTER TABLE Donations.LandPendingTrans CHECK CONSTRAINT [FK_LandPendingTrans_LandNeeded];
GO

ALTER TABLE Donations.LandPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_LandPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.LandPendingTrans CHECK CONSTRAINT [FK_LandPendingTrans_UserID];
GO

ALTER TABLE Donations.MoneyDonated WITH NOCHECK ADD  CONSTRAINT [FK_MoneyDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.MoneyDonated CHECK CONSTRAINT [FK_MoneyDonated_UserID];
GO

ALTER TABLE Donations.MoneyNeeded WITH NOCHECK ADD  CONSTRAINT [FK_MoneyNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.MoneyNeeded CHECK CONSTRAINT [FK_MoneyNeeded_UserID];
GO

ALTER TABLE Donations.MoneyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_MoneyPendingTrans_NeedID] FOREIGN KEY(NeedID)
REFERENCES Donations.MoneyNeeded(NeedID);
GO
ALTER TABLE Donations.MoneyPendingTrans CHECK CONSTRAINT [FK_MoneyPendingTrans_NeedID];
GO

ALTER TABLE Donations.MoneyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_MoneyPendingTrans_DonationID] FOREIGN KEY(DonationID)
REFERENCES Donations.MoneyNeeded(NeedID);
GO
ALTER TABLE Donations.MoneyPendingTrans CHECK CONSTRAINT [FK_MoneyPendingTrans_DonationID];
GO

ALTER TABLE Donations.MoneyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_MoneyPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.MoneyPendingTrans CHECK CONSTRAINT [FK_MoneyPendingTrans_UserID];
GO

ALTER TABLE Donations.SeedsDonated WITH NOCHECK ADD  CONSTRAINT [FK_SeedsDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SeedsDonated CHECK CONSTRAINT [FK_SeedsDonated_UserID];
GO

ALTER TABLE Donations.SeedsNeeded WITH NOCHECK ADD  CONSTRAINT [FK_SeedsNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SeedsNeeded CHECK CONSTRAINT [FK_SeedsNeeded_UserID];
GO

ALTER TABLE Donations.SeedsPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SeedsPendingTrans_SeedsDonatedID] FOREIGN KEY(SeedsDonatedID)
REFERENCES Donations.SeedsDonated(DonationID);
GO
ALTER TABLE Donations.SeedsPendingTrans CHECK CONSTRAINT [FK_SeedsPendingTrans_SeedsDonatedID];
GO

ALTER TABLE Donations.SeedsPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SeedsPendingTrans_SeedsNeededID] FOREIGN KEY(SeedsNeededID)
REFERENCES Donations.SeedsNeeded(SeedsNeededID);
GO
ALTER TABLE Donations.SeedsPendingTrans CHECK CONSTRAINT [FK_SeedsPendingTrans_SeedsNeededID];
GO

ALTER TABLE Donations.SeedsPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SeedsPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SeedsPendingTrans CHECK CONSTRAINT [FK_SeedsPendingTrans_UserID];
GO

ALTER TABLE Donations.SoilDonated WITH NOCHECK ADD  CONSTRAINT [FK_SoilDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SoilDonated CHECK CONSTRAINT [FK_SoilDonated_UserID];
GO

ALTER TABLE Donations.SoilNeeded WITH NOCHECK ADD  CONSTRAINT [FK_SoilNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SoilNeeded CHECK CONSTRAINT [FK_SoilNeeded_UserID];
GO

ALTER TABLE Donations.SoilPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SoilPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SoilPendingTrans CHECK CONSTRAINT [FK_SoilPendingTrans_UserID];
GO

ALTER TABLE Donations.SoilPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SoilPendingTrans_SoilDonatedID] FOREIGN KEY(SoilDonatedID)
REFERENCES Donations.SoilDonated(SoilDonatedID);
GO
ALTER TABLE Donations.SoilPendingTrans CHECK CONSTRAINT [FK_SoilPendingTrans_SoilDonatedID];
GO

ALTER TABLE Donations.SoilPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SoilPendingTrans_SoilNeededID] FOREIGN KEY(SoilNeededID)
REFERENCES Donations.SoilNeeded(SoilNeededID);
GO
ALTER TABLE Donations.SoilPendingTrans CHECK CONSTRAINT [FK_SoilPendingTrans_SoilNeededID];
GO

ALTER TABLE Donations.SupplyDonated WITH NOCHECK ADD  CONSTRAINT [FK_SupplyDonated_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SupplyDonated CHECK CONSTRAINT [FK_SupplyDonated_UserID];
GO

ALTER TABLE Donations.SupplyNeeded WITH NOCHECK ADD  CONSTRAINT [FK_SupplyNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SupplyNeeded CHECK CONSTRAINT [FK_SupplyNeeded_UserID];
GO

ALTER TABLE Donations.SupplyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SupplyPendingTrans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.SupplyPendingTrans CHECK CONSTRAINT [FK_SupplyPendingTrans_UserID];
GO

ALTER TABLE Donations.SupplyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SupplyPendingTrans_SupplyNeededID] FOREIGN KEY(SupplyNeededID)
REFERENCES Donations.SupplyNeeded(SupplyNeededID);
GO
ALTER TABLE Donations.SupplyPendingTrans CHECK CONSTRAINT [FK_SupplyPendingTrans_UserID];
GO

ALTER TABLE Donations.SupplyPendingTrans WITH NOCHECK ADD  CONSTRAINT [FK_SupplyPendingTrans_SupplyDonatedID] FOREIGN KEY(SupplyDonatedID)
REFERENCES Donations.SupplyDonated(SupplyDonatedID);
GO
ALTER TABLE Donations.SupplyPendingTrans CHECK CONSTRAINT [FK_SupplyPendingTrans_UserID];
GO

ALTER TABLE Donations.TimeNeeded WITH NOCHECK ADD  CONSTRAINT [FK_TimeNeeded_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.TimeNeeded CHECK CONSTRAINT [FK_TimeNeeded_UserID];
GO

ALTER TABLE Donations.TimePledge WITH NOCHECK ADD  CONSTRAINT [FK_TimePledge_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.TimePledge CHECK CONSTRAINT [FK_TimePledge_UserID];
GO

ALTER TABLE Donations.TimePledgeTrans WITH NOCHECK ADD  CONSTRAINT [FK_TimePledgeTrans_TimePledgeID] FOREIGN KEY(TimePledgeID)
REFERENCES Donations.TimePledge(TimePledgeID);
GO
ALTER TABLE Donations.TimePledgeTrans CHECK CONSTRAINT [FK_TimePledgeTrans_TimePledgeID];
GO

ALTER TABLE Donations.TimePledgeTrans WITH NOCHECK ADD  CONSTRAINT [FK_TimePledgeTrans_TimeNeededID] FOREIGN KEY(TimeNeededID)
REFERENCES Donations.TimeNeeded(TimeNeededID);
GO
ALTER TABLE Donations.TimePledgeTrans CHECK CONSTRAINT [FK_TimePledgeTrans_TimeNeededID];
GO

ALTER TABLE Donations.VolunteerHours WITH NOCHECK ADD  CONSTRAINT [FK_VolunteerHours_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Donations.VolunteerHours CHECK CONSTRAINT [FK_VolunteerHours_UserID];
GO

ALTER TABLE Expert.BecomeAnExpert WITH NOCHECK ADD  CONSTRAINT [FK_BecomeAnExpert_ApprovedBy] FOREIGN KEY(ApprovedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BecomeAnExpert CHECK CONSTRAINT [FK_BecomeAnExpert_ApprovedBy];
GO

ALTER TABLE Expert.BecomeAnExpert WITH NOCHECK ADD  CONSTRAINT [FK_BecomeAnExpert_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BecomeAnExpert CHECK CONSTRAINT [FK_BecomeAnExpert_CreatedBy];
GO

ALTER TABLE Expert.BecomeAnExpert WITH NOCHECK ADD  CONSTRAINT [FK_BecomeAnExpert_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BecomeAnExpert CHECK CONSTRAINT [FK_BecomeAnExpert_ModifiedBy];
GO

ALTER TABLE Expert.BlogEntry WITH NOCHECK ADD  CONSTRAINT [FK_BlogEntry_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BlogEntry CHECK CONSTRAINT [FK_BlogEntry_CreatedBy];
GO

ALTER TABLE Expert.BlogEntry WITH NOCHECK ADD  CONSTRAINT [FK_BlogEntry_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.BlogEntry CHECK CONSTRAINT [FK_BlogEntry_ModifiedBy];
GO

ALTER TABLE Expert.Blueprints WITH NOCHECK ADD  CONSTRAINT [FK_Blueprints_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Blueprints CHECK CONSTRAINT [FK_Blueprints_CreatedBy];
GO

ALTER TABLE Expert.Content WITH NOCHECK ADD  CONSTRAINT [FK_Content_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Content CHECK CONSTRAINT [FK_Content_CreatedBy];
GO

ALTER TABLE Expert.Content WITH NOCHECK ADD  CONSTRAINT [FK_Content_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Content CHECK CONSTRAINT [FK_Content_ModifiedBy];
GO

ALTER TABLE Expert.Expertise WITH NOCHECK ADD  CONSTRAINT [FK_Expertise_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Expertise CHECK CONSTRAINT [FK_Expertise_CreatedBy];
GO

ALTER TABLE Expert.Expertise WITH NOCHECK ADD  CONSTRAINT [FK_Expertise_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Expertise CHECK CONSTRAINT [FK_Expertise_ModifiedBy];
GO

ALTER TABLE Expert.Expertise WITH NOCHECK ADD  CONSTRAINT [FK_Expertise_ExpertID] FOREIGN KEY(ExpertID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Expertise CHECK CONSTRAINT [FK_Expertise_ExpertID];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_CreatedBy];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_ModifiedBy];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_GardenID] FOREIGN KEY(GardenID)
REFERENCES Gardens.Gardens(GardenID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_GardenID];
GO

ALTER TABLE Expert.GardenNotifications WITH NOCHECK ADD  CONSTRAINT [FK_GardenNotifications_NotificationID] FOREIGN KEY(NotificationID)
REFERENCES Expert.Notifications(NotificationID);
GO
ALTER TABLE Expert.GardenNotifications CHECK CONSTRAINT [FK_GardenNotifications_NotificationID];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_GardenID] FOREIGN KEY(GardenID)
REFERENCES Gardens.Gardens(GardenID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_GardenID];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_PlantID] FOREIGN KEY(PlantID)
REFERENCES Expert.Plants(PlantID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_PlantID];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_CreatedBy];
GO

ALTER TABLE Expert.GardenPlants WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlants_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenPlants CHECK CONSTRAINT [FK_GardenPlants_ModifiedBy];
GO

ALTER TABLE Expert.GardenTemplateUploads WITH NOCHECK ADD  CONSTRAINT [FK_GardenTemplate_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenTemplateUploads CHECK CONSTRAINT [FK_GardenTemplate_CreatedBy];
GO

ALTER TABLE Expert.GardenTemplateFiles WITH NOCHECK ADD  CONSTRAINT [FK_GardenTemplate_ImageName] FOREIGN KEY(ImageName)
REFERENCES Expert.GardenTemplateUploads(ImageName);
GO
ALTER TABLE Expert.GardenTemplateFiles CHECK CONSTRAINT [FK_GardenTemplate_ImageName];
GO

ALTER TABLE Expert.GardenTypes WITH NOCHECK ADD  CONSTRAINT [FK_GardenTypes_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenTypes CHECK CONSTRAINT [FK_GardenTypes_CreatedBy];
GO

ALTER TABLE Expert.GardenTypes WITH NOCHECK ADD  CONSTRAINT [FK_GardenTypes_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.GardenTypes CHECK CONSTRAINT [FK_GardenTypes_ModifiedBy];
GO

ALTER TABLE Expert.Notifications WITH NOCHECK ADD  CONSTRAINT [FK_Notifications_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Notifications CHECK CONSTRAINT [FK_Notifications_CreatedBy];
GO

ALTER TABLE Expert.Notifications WITH NOCHECK ADD  CONSTRAINT [FK_Notifications_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Notifications CHECK CONSTRAINT [FK_Notifications_ModifiedBy];
GO

ALTER TABLE Expert.Plants WITH NOCHECK ADD  CONSTRAINT [FK_Plants_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Plants CHECK CONSTRAINT [FK_Plants_CreatedBy];
GO

ALTER TABLE Expert.Plants WITH NOCHECK ADD  CONSTRAINT [FK_Plants_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Plants CHECK CONSTRAINT [FK_Plants_ModifiedBy];
GO

ALTER TABLE Expert.Plants WITH NOCHECK ADD  CONSTRAINT [FK_Plants_Category] FOREIGN KEY(Category)
REFERENCES Expert.PlantCategory(CategoryName);
GO
ALTER TABLE Expert.Plants CHECK CONSTRAINT [FK_Plants_Category];
GO

ALTER TABLE Expert.PlantCategory WITH NOCHECK ADD  CONSTRAINT [FK_PlantCategory_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.PlantCategory CHECK CONSTRAINT [FK_PlantCategory_CreatedBy];
GO

ALTER TABLE Expert.PlantNutrients WITH NOCHECK ADD  CONSTRAINT [FK_PlantNutrients_PlantID] FOREIGN KEY(PlantID)
REFERENCES Expert.Plants(PlantID);
GO
ALTER TABLE Expert.PlantNutrients CHECK CONSTRAINT [FK_PlantNutrients_PlantID];
GO

ALTER TABLE Expert.PlantNutrients WITH NOCHECK ADD  CONSTRAINT [FK_PlantNutrients_NutrientID] FOREIGN KEY(NutrientID)
REFERENCES Expert.Nutrients(NutrientID);
GO
ALTER TABLE Expert.PlantNutrients CHECK CONSTRAINT [FK_PlantNutrients_NutrientID];
GO

ALTER TABLE Expert.Nutrients WITH NOCHECK ADD  CONSTRAINT [FK_Nutrients_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Nutrients CHECK CONSTRAINT [FK_Nutrients_CreatedBy];
GO

ALTER TABLE Expert.Nutrients WITH NOCHECK ADD  CONSTRAINT [FK_Nutrients_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Nutrients CHECK CONSTRAINT [FK_Nutrients_ModifiedBy];
GO

ALTER TABLE Expert.Question WITH NOCHECK ADD  CONSTRAINT [FK_Question_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Question CHECK CONSTRAINT [FK_Question_CreatedBy];
GO

ALTER TABLE Expert.Question WITH NOCHECK ADD  CONSTRAINT [FK_Question_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Question CHECK CONSTRAINT [FK_Question_ModifiedBy];
GO

ALTER TABLE Expert.Question WITH NOCHECK ADD  CONSTRAINT [FK_Question_RegionID] FOREIGN KEY(RegionID)
REFERENCES Admin.Regions(RegionID);
GO
ALTER TABLE Expert.Question CHECK CONSTRAINT [FK_Question_RegionID];
GO

ALTER TABLE Expert.QuestionResponse WITH NOCHECK ADD  CONSTRAINT [FK_QuestionResponse_QuestionID] FOREIGN KEY(QuestionID)
REFERENCES Expert.Question(QuestionID);
GO
ALTER TABLE Expert.QuestionResponse CHECK CONSTRAINT [FK_QuestionResponse_QuestionID];
GO

ALTER TABLE Expert.QuestionResponse WITH NOCHECK ADD  CONSTRAINT [FK_QuestionResponse_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.QuestionResponse CHECK CONSTRAINT [FK_QuestionResponse_UserID];
GO

--added by rhett 4-29-16
ALTER TABLE Expert.QuestionResponse WITH NOCHECK ADD  CONSTRAINT [FK_QuestionResponse_BlogID] FOREIGN KEY(BlogID)
REFERENCES Expert.BlogEntry(BlogID);
GO
ALTER TABLE Expert.QuestionResponse CHECK CONSTRAINT [FK_QuestionResponse_BlogID];
GO

ALTER TABLE Expert.Recipes WITH NOCHECK ADD  CONSTRAINT [FK_Recipes_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Recipes CHECK CONSTRAINT [FK_Recipes_CreatedBy];
GO

ALTER TABLE Expert.Recipes WITH NOCHECK ADD  CONSTRAINT [FK_Recipes_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Recipes CHECK CONSTRAINT [FK_Recipes_ModifiedBy];
GO

ALTER TABLE Expert.Recipes WITH NOCHECK ADD  CONSTRAINT [FK_Recipes_Category] FOREIGN KEY(Category)
REFERENCES Expert.RecipeCategory(CategoryName);
GO
ALTER TABLE Expert.Recipes CHECK CONSTRAINT [FK_Recipes_Category];
GO

ALTER TABLE Expert.RecipeCategory WITH NOCHECK ADD  CONSTRAINT [FK_RecipeCategory_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.RecipeCategory CHECK CONSTRAINT [FK_RecipeCategory_CreatedBy];
GO

ALTER TABLE Expert.PlantRegions WITH NOCHECK ADD  CONSTRAINT [FK_PlantRegions_Plants] FOREIGN KEY(PlantID)
REFERENCES Expert.Plants(PlantID);
GO
ALTER TABLE Expert.PlantRegions CHECK CONSTRAINT [FK_PlantRegions_Plants];
GO

ALTER TABLE Expert.PlantRegions WITH NOCHECK ADD  CONSTRAINT [FK_PlantRegions_Regions] FOREIGN KEY(RegionID)
REFERENCES Admin.Regions(RegionID);
GO
ALTER TABLE Expert.PlantRegions CHECK CONSTRAINT [FK_PlantRegions_Regions];
GO

ALTER TABLE Expert.Template WITH NOCHECK ADD  CONSTRAINT [FK_Template_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Template CHECK CONSTRAINT [FK_Template_CreatedBy];
GO

ALTER TABLE Expert.Templates WITH NOCHECK ADD  CONSTRAINT [FK_Templates_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Expert.Templates CHECK CONSTRAINT [FK_Templates_UserID];
GO

-- created by luke frahm 4-22-16 FK UserName
ALTER TABLE Gardens.Announcements WITH NOCHECK ADD CONSTRAINT [FK_Announcements_UserName] FOREIGN KEY(UserName)
REFERENCES Admin.Users(UserName);
GO
ALTER TABLE Gardens.Announcements CHECK CONSTRAINT [FK_Announcements_UserName];
GO


-- created by luke frahm 4-22-16-- FK GroupID
ALTER TABLE Gardens.Announcements WITH NOCHECK ADD CONSTRAINT [FK_Announcements_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.Announcements CHECK CONSTRAINT [FK_Announcements_GroupID];
GO


ALTER TABLE Gardens.Gardens WITH NOCHECK ADD  CONSTRAINT [FK_Gardens_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.Gardens CHECK CONSTRAINT [FK_Gardens_GroupID];
GO

ALTER TABLE Gardens.Gardens WITH NOCHECK ADD  CONSTRAINT [FK_Gardens_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.Gardens CHECK CONSTRAINT [FK_Gardens_UserID];
GO

ALTER TABLE Gardens.GardenGuides WITH NOCHECK ADD  CONSTRAINT [FK_GardenGuides_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GardenGuides CHECK CONSTRAINT [FK_GardenGuides_UserID];
GO

ALTER TABLE Gardens.GroupLeaders WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaders_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupLeaders CHECK CONSTRAINT [FK_GroupLeaders_UserID];
GO

ALTER TABLE Gardens.GroupLeaders WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaders_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.GroupLeaders CHECK CONSTRAINT [FK_GroupLeaders_GroupID];
GO

--Added by Nick King 3-4-16
ALTER TABLE Gardens.GroupLeaderRequests WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaderRequests_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupLeaderRequests CHECK CONSTRAINT FK_GroupLeaderRequests_UserID;
GO

--Added by Nick King 3-4-16
ALTER TABLE Gardens.GroupLeaderRequests WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaderRequests_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.GroupLeaderRequests CHECK CONSTRAINT FK_GroupLeaderRequests_GroupID;
GO

--Added by Nick King 3-4-16
ALTER TABLE Gardens.GroupLeaderRequests WITH NOCHECK ADD  CONSTRAINT [FK_GroupLeaderRequests_ModifiedBy] FOREIGN KEY(ModifiedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupLeaderRequests CHECK CONSTRAINT FK_GroupLeaderRequests_ModifiedBy;
GO


ALTER TABLE Gardens.GroupMembers WITH NOCHECK ADD  CONSTRAINT [FK_GroupMembers_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.GroupMembers CHECK CONSTRAINT [FK_GroupMembers_GroupID];
GO

ALTER TABLE Gardens.GroupMembers WITH NOCHECK ADD  CONSTRAINT [FK_GroupMembers_CreatedBy] FOREIGN KEY(CreatedBy)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupMembers CHECK CONSTRAINT [FK_GroupMembers_CreatedBy];
GO

ALTER TABLE Gardens.GroupMembers WITH NOCHECK ADD  CONSTRAINT [FK_GroupMembers_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GroupMembers CHECK CONSTRAINT [FK_GroupMembers_UserID];
GO

ALTER TABLE Gardens.GardenPlans WITH NOCHECK ADD  CONSTRAINT [FK_GardenPlans_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.GardenPlans CHECK CONSTRAINT [FK_GardenPlans_UserID];
GO

ALTER TABLE Gardens.Groups WITH NOCHECK ADD  CONSTRAINT [FK_Groups_GroupLeaderID] FOREIGN KEY(GroupLeaderID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.Groups CHECK CONSTRAINT [FK_Groups_GroupLeaderID];
GO

Alter Table Gardens.Organizations with nocheck add constraint FK_Organizations_OrganizationLeader FOREIGN KEY(OrganizationLeader)
references Admin.Users(UserID);
go
Alter Table Gardens.Organizations CHECK CONSTRAINT FK_Organizations_OrganizationLeader;
GO

ALTER TABLE Gardens.PostLineItems WITH NOCHECK ADD  CONSTRAINT [FK_PostLineItems_GroupLeaderID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.PostLineItems CHECK CONSTRAINT [FK_PostLineItems_GroupLeaderID];
GO

ALTER TABLE Gardens.PostLineItems WITH NOCHECK ADD  CONSTRAINT [FK_PostLineItems_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.PostLineItems CHECK CONSTRAINT [FK_PostLineItems_GroupID];
GO

ALTER TABLE Gardens.PostLineItems WITH NOCHECK ADD  CONSTRAINT [FK_PostLineItems_PostID] FOREIGN KEY(PostID)
REFERENCES Gardens.PostThreads(PostID);
GO
ALTER TABLE Gardens.PostLineItems CHECK CONSTRAINT [FK_PostLineItems_PostID];
GO

ALTER TABLE Gardens.PostThreads WITH NOCHECK ADD  CONSTRAINT [FK_PostThreads_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.PostThreads CHECK CONSTRAINT [FK_PostThreads_UserID];
GO

ALTER TABLE Gardens.PostThreads WITH NOCHECK ADD  CONSTRAINT [FK_PostThreads_GroupID] FOREIGN KEY(GroupID)
REFERENCES Gardens.Groups(GroupID);
GO
ALTER TABLE Gardens.PostThreads CHECK CONSTRAINT [FK_PostThreads_GroupID];
GO

ALTER TABLE Gardens.Tasks WITH NOCHECK ADD  CONSTRAINT [FK_Tasks_AssignedTo] FOREIGN KEY(AssignedTo)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.Tasks CHECK CONSTRAINT [FK_Tasks_AssignedTo];
GO

ALTER TABLE Gardens.Tasks WITH NOCHECK ADD  CONSTRAINT [FK_Tasks_GardenID] FOREIGN KEY(GardenID)
REFERENCES Gardens.Gardens(GardenID);
GO
ALTER TABLE Gardens.Tasks CHECK CONSTRAINT [FK_Tasks_GardenID];
GO


ALTER TABLE Gardens.Tasks WITH NOCHECK ADD  CONSTRAINT [FK_Tasks_AssignedFrom] FOREIGN KEY(AssignedFrom)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.Tasks CHECK CONSTRAINT [FK_Tasks_AssignedFrom];
GO

ALTER TABLE Gardens.WorkLogs WITH NOCHECK ADD  CONSTRAINT [FK_WorkLogs_UserID] FOREIGN KEY(UserID)
REFERENCES Admin.Users(UserID);
GO
ALTER TABLE Gardens.WorkLogs CHECK CONSTRAINT [FK_WorkLogs_UserID];
GO

ALTER TABLE Gardens.WorkLogs WITH NOCHECK ADD  CONSTRAINT [FK_WorkLogs_TaskID] FOREIGN KEY(TaskID)
REFERENCES Gardens.Tasks(TaskID);
GO
ALTER TABLE Gardens.WorkLogs CHECK CONSTRAINT [FK_WorkLogs_TaskID];
GO


/**********************************************************************************/
/******************************* Indexes ******************************************/
/**********************************************************************************/


CREATE NONCLUSTERED INDEX IX_ActivityLog_UserID ON Admin.ActivityLog (UserID);
GO
--modified by ryan taylor 4-14-16
CREATE NONCLUSTERED INDEX IX_Messages_SenderID ON Admin.Messages (Sender);
GO

--modified by ryan taylor 4-14-16
CREATE NONCLUSTERED INDEX IX_Messages_ReceiverID ON Admin.Messages (Receiver);
GO

CREATE NONCLUSTERED INDEX IX_Users_LastName ON Admin.Users (LastName);
GO

CREATE NONCLUSTERED INDEX IX_Users_RegionID ON Admin.Users (RegionID);
GO


/****************************************************************************************/
/*******************************Stored Procedures ***************************************/
/****************************************************************************************/


------------------------------------------
-----------Admin.ActivityLog--------------
------------------------------------------

create procedure Admin.spInsertActivityLog(	
	@UserID int,
    @date smalldatetime,
	@LogEntry varchar(250),
	@UserAction varchar(100)
	)
as 
begin
INSERT INTO [Admin].[ActivityLog]
    ([UserID]
    ,[Date]
    ,[LogEntry]
    ,[UserAction])
VALUES(
    @UserID,
    @date,
	@LogEntry,
	@UserAction
	);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Admin.ExpertRequest------------
------------------------------------------

create procedure Admin.spInsertExpertRequest(
@UserID int, 
@Title varchar(20),
@Content VARCHAR(MAX),
@DateCreated datetime
)
as
begin
insert into Admin.ExpertRequests(
UserID, 
Title,
Content,
DateCreated
)
values
(
@UserID,
@Title,
@Content,
@DateCreated
);

return @@ROWCOUNT;
end;
go


--created by chris sheehan 4/28/16
create PROCEDURE Admin.spUpdateExpertRequestApproved (
@requestID int,
@userID int,
@modifiedby int,
@approved bit)
AS
BEGIN
      UPDATE Admin.ExpertRequests 
	    SET 
		approved = @approved,
		ModifiedBy = @modifiedby, 
		Active = 0
		WHERE UserID = @UserID   AND
                      requestid = @requestid;
		if (NOT EXISTS(select * from userroles where UserID = @userID and RoleID = 'Expert'))
		begin
		insert into admin.userroles(UserID, RoleID, CreatedBy, CreatedDate) 
		values (@userID, 'Expert', @modifiedby, getdate());
		end

	return @@ROWCOUNT;  
END;
go

create PROCEDURE Admin.spUpdateExpertRequestDecline (
@requestID int,
@userID int,
@modifiedby int,
@approved bit)
AS
BEGIN
      UPDATE Admin.ExpertRequests 
	    SET 
		approved = @approved,
		ModifiedBy = @modifiedby, 
		Active = 0
		WHERE UserID = @UserID   AND
                      requestid = @requestid;
	return @@ROWCOUNT;  
END;
go

--created by Chris Sheehan 4-28-16
create PROCEDURE Admin.spSelectExpertRequests(
	@active bit)
AS
BEGIN
select er.requestid, u.userid, u.username, u.firstname, u.lastname, 
u.emailaddress, er.datecreated, er.title, er.content
from admin.ExpertRequests er inner join admin.Users u
on er.UserID = u.userid 
where er.Active = @active
END;
GO

--created by Chris Sheehan 4/28/16


------------------------------------------
-----------Admin.GroupRequest-------------
------------------------------------------

-- UPDATED BY TREVOR GLISCH 4/6/16
CREATE PROCEDURE Admin.spInsertGroupRequest(
	@GroupID int,
	@UserID int, 	
	@RequestDate smalldatetime)
AS
BEGIN
INSERT INTO Admin.GroupRequest(
	GroupID,
	UserID, 	
	RequestDate)
	
VALUES(
	@GroupID,
	@UserID,
	@RequestDate);
	
	RETURN @@ROWCOUNT;
END;
GO

-- GETS ALL OF THE REQUESTS FOR A DEFINED GROUP SO THAT GROUP LEADERS CAN ADDRESS REQUESTS
-- UPDATED BY TREVOR GLISCH 4/6/16
CREATE PROCEDURE Admin.spGetRequestsForGroup(
	@GroupID int)
AS
BEGIN
SELECT UserID, RequestDate
FROM Admin.GroupRequest
WHERE RequestStatus = 1 AND GroupID = @GroupID AND Added = 0

END;
GO

------------------------------------------
-----------Admin.GroupRequest-------------
------------------------------------------

-- SETS REQUEST STATUS TO ACCEPPTED AND THEN RUNS STORED PROCEDURE TO ADD USER TO THE GROUP
-- UPDATED BY Nick king 4/22/16
-- UPDATED BY TREVOR GLISCH 4/6/16
CREATE PROCEDURE Admin.spAcceptRequest(
	@GroupID int,
	@UserID int,
	@ApprovedID int,
	@ApprovedDate smalldatetime)
AS
BEGIN
UPDATE Admin.GroupRequest
	SET
		RequestStatus = 0,
		ApprovedDate = @ApprovedDate,
		ApprovedBy = @ApprovedID,
		Added = 1
	WHERE
		GroupID = @GroupID AND
		UserID = @UserID ;
	
	EXEC Gardens.spInsertGroupMembers @GroupID, @UserID, @ApprovedDate, @ApprovedID, 1;
	-- THIS LINE CAN LATER BE USED TO SEND A MESSAGE TO A USER THAT THEY HAVE BEEN ACCEPTED
	--EXEC Admin.spInsertMessageLineItems 	
END;
GO

-- SETS THE REQUESTSTATUS AND ACTIVE FEILDS TO 0 LEAVING OUT AND APPROVED BY AND APPROVED DATE, THIS INDICATES THE REQUEST WAS DECLINED
-- UPDATED BY TREVOR GLISCH 4/6/16
CREATE PROCEDURE Admin.spDeclineRequest(
	@GroupID int,
	@UserID int)
AS
BEGIN
UPDATE Admin.GroupRequest
	SET
		RequestStatus = 0,
		Added = 0
	WHERE
		GroupID = @GroupID AND
		UserID = @UserID;
		
	-- THIS LINE CAN LATER BE USED TO SEND A MESSAGE TO A USER THAT THEY HAVE BEEN DECLINED
	--EXEC Admin.spInsertMessageLineItems 
END;
GO

------------------------------------------
-----------Admin.Messages-----------------
------------------------------------------

create procedure Admin.spInsertMessage(
	@MessageContent varchar(250),
	@Subject varchar(50),
	@Sender varchar(20),
	@Receiver varchar(20)
	)
as 
begin
	insert into Admin.Messages(
			MessageContent,
			Subject,
			Sender,
			Receiver)
		values(
			@MessageContent,
			@Subject,
			@Sender,
			@Receiver);
		return @@ROWCOUNT;
end;
go


create procedure Admin.spSelectMessage(
	@Username varchar(20)
	)
as
begin
	SELECT MessageID, MessageContent, MessageDate, Subject, Sender, Receiver, Unread, SenderDeleted, ReceiverDeleted
	FROM Admin.Messages
	WHERE Sender = @Username
	OR Receiver = @Username
end;
go


create procedure Admin.spMarkMessageRead(
	@MessageID int,
	@Username varchar(20)
)
as
begin
	UPDATE Admin.Messages
	SET Unread = 0
	WHERE MessageID = @MessageID
	AND Receiver = @Username
end;
go


create procedure Admin.spMarkMessageSenderDeleted(
	@MessageID int,
	@Username varchar(20)
)
as
begin
	UPDATE Admin.Messages
	SET SenderDeleted = 1
	WHERE MessageID = @MessageID
	AND Sender = @Username
end;
go

create procedure Admin.spMarkMessageReceiverDeleted(
	@MessageID int,
	@Username varchar(20)
)
as
begin
	UPDATE Admin.Messages
	SET ReceiverDeleted = 1
	WHERE MessageID = @MessageID
	AND Receiver = @Username
end;
go

------------------------------------------
-----------Admin.Regions------------------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Admin.spInsertRegions(
	@RegionID int,
	@SoilType varchar(20),
	@AverageTempSummer decimal,
	@AverageTempFall decimal,
	@AverageTempWinter decimal,
	@AverageTempSpring decimal,
	@AverageRainfall decimal,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Admin.Regions(
	RegionID,
	SoilType,
	AverageTempSummer,
	AverageTempFall,
	AverageTempWinter,
	AverageTempSpring,
	AverageRainfall,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@RegionID,
	@SoilType,
	@AverageTempSummer,
	@AverageTempFall,
	@AverageTempWinter,
	@AverageTempSpring,
	@AverageRainfall,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Admin.spSelectRegions
AS
BEGIN
	SELECT RegionID, SoilType, AverageTempSummer, AverageTempFall, AverageTempWinter, 
		AverageTempSpring, AverageRainfall, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Admin.Regions
END;
go

------------------------------------------
-----------Admin.Roles--------------------
------------------------------------------
 
 create procedure Admin.spInsertRoles(
	@roleID varchar(30),
	@Description varchar(100),
	@CreatedBy int,
	@CreatedDate smalldatetime)
as 
begin
insert into Admin.Roles(
	roleID,
	Description, 
	CreatedBy,
	CreatedDate)
values(
	@roleID,		
	@Description,
	@CreatedBy,
	@CreatedDate);
	return @@ROWCOUNT;
end;
go

CREATE PROCEDURE Admin.spSelectRoles (
    @userID INT
)
AS
BEGIN
    SELECT Admin.UserRoles.RoleID, Admin.Roles.Description, Admin.UserRoles.Active 
    FROM [Admin].[Roles], [Admin].[UserRoles]
    WHERE UserRoles.UserID = @userID
    AND Roles.roleID = UserRoles.roleID;
END;
GO



------------------------------------------
-----------Admin.UserRoles----------------
------------------------------------------

create procedure Admin.spInsertUserRoles(
	@UserID int,
	@RoleID varchar(30)
	)
as begin
insert into Admin.UserRoles(
	UserID, 
	RoleID)
values(
	@UserID,
	@RoleID);
	return @@ROWCOUNT;
end;
go

--created by ibrahim 3-11-16
CREATE PROCEDURE Admin.spUpdateUserRoleRemove (
@userID int,
@RoleID varchar(30))
AS
BEGIN
      UPDATE Admin.UserRoles 
	    SET Active = 0 
		WHERE UserID = @UserID   AND
                      RoleID = @RoleID;

	return @@ROWCOUNT;  
END;
go

--created by ibrahim 3-11-16
CREATE PROCEDURE Admin.spSelectUserRole
AS
Begin

   Select [UserID]
      ,[RoleID]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[Active] 
	  from Admin.UserRoles;
end;
go

-- Displaying userRoles for a specific User
-- By Ibrahim Abuzaid 04-16-16
CREATE PROCEDURE [Admin].[spSelectUserRoleByUser]
(@UserID int)
AS
Begin

   Select [UserID]
      ,[RoleID]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[Active] 
	  from Admin.UserRoles
	  where UserID = @UserID;
end;
go

--created by ibrahim 3-11-16
CREATE PROCEDURE [Admin].[spUpdateUserRoles] (
@UserID int,
@RoleID varchar(30),
@CreatedBy int,
@CreatedDate smallDateTime
)
AS
BEGIN
        UPDATE Admin.UserRoles
		     SET     
			      CreatedBy = @CreatedBy,
				CreatedDate = @CreatedDate
						 
			WHERE    userID = @userID AND
			        RoleID  = @RoleID;
	return @@ROWCOUNT;     
END;
go

create procedure Admin.spRemoveUserRole(
	@userid int, @roleid varchar(30))
as
begin
	delete from Admin.UserRoles where userid = @userid and roleid = @roleid;
end;
go

------------------------------------------
-----------Admin.Users--------------------
------------------------------------------

--Modified By : Poonam Dubey 
--Modified Date : 16th March 2016 
--Description : Added code to insert value into userrole table
CREATE procedure [Admin].[spInsertUsers] (
	@FirstName varchar(50),
	@LastName varchar(100),
	@Zip char(9) ,
	@EmailAddress varchar(100),
	@UserName varchar(20),
	@Password varchar(150),
	@RegionID int)
AS
BEGIN
DECLARE @UserID INT = 0
IF ((SELECT COUNT(*) FROM Admin.Users AU WHERE LOWER(AU.UserName) = LOWER(@UserName)) > 0)
	BEGIN
		SELECT 2 'ReturnValue'		
	END
ELSE
	BEGIN
		INSERT INTO Admin.Users(
			FirstName,
			LastName,
			Zip ,
			EmailAddress,
			UserName,
			Password,
			RegionID)
		VALUES(
			@FirstName,
			@LastName,
			@Zip ,
			@EmailAddress,
			@UserName,
			@Password,
			@RegionID);

		SET @UserID = (SELECT IDENT_CURRENT('Admin.Users'))

		INSERT INTO Admin.UserRoles 
		(
		UserID,
		RoleID,
		CreatedBy,
		CreatedDate,
		Active
		)
		VALUES(
		@UserID,
		'User',
		1000,
		GETDATE(),
		1
		);
			SELECT 1 AS 'ReturnValue';
	END;
END;
GO

CREATE PROCEDURE Admin.spUpdateUser (
@UserID int,
@FirstName varchar(50),
@LastName varchar(100),
@Zip char(9),
@EmailAddress varchar(100),
@UserName varchar(20),
@PassWord varchar(150),
@Active bit,
@RegionID int,
@originalFirstName varchar(50),
@originalLastName varchar(100),
@originalZip char(9),
@originalEmailAddress varchar(100),
@originalUserName varchar(20),
@originalPassWord varchar(150),
@originalActive bit,
@originalRegionID int)
AS
BEGIN
        UPDATE Admin.Users
		     SET     FirstName = @FirstName,
					 LastName  = @LastName, 
					 Zip       = @Zip,
				  EmailAddress = @EmailAddress, 
					 userName  = @userName,
					 PassWord  = @PassWord, 
					 Active    = @Active,
					 RegionID  = @RegionID					 
			WHERE       userID = @userID AND
			         FirstName = @originalFirstName AND
					 LastName  = @originalLastName AND 
					 Zip       = @originalZip AND
				  EmailAddress = @originalEmailAddress AND 
					 userName  = @originaluserName AND
					 PassWord  = @originalPassWord AND 
					 Active    = @originalActive AND
					 RegionID  = @originalRegionID;

	return @@ROWCOUNT;     
END;
go

--created by Rhett 2-25-16
CREATE PROCEDURE Admin.spSelectUsers (
	@Active	bit
)
AS
BEGIN
	SELECT UserID, FirstName, LastName, Zip, EmailAddress, UserName,
		PassWord, Active, RegionID
	FROM Admin.Users
	WHERE Active = @Active;
END;
go

create procedure Admin.spSelectUsersByRole(
	@Active bit, @roleid varchar(30))
as
begin
	select u.userid, u.firstname, u.LastName, u.emailaddress, u.username
	from admin.users u inner join userroles ur on u.userid = ur.userid
	where ur.active = @active and u.active = @active and ur.roleid = @roleid;
end;
go


create PROCEDURE Admin.spSelectUsersExcludingRole 
AS
BEGIN
	SELECT u.UserID, u.FirstName, u.LastName, u.EmailAddress, u.UserName
	FROM Admin.Users u 
	WHERE UserID not in (select userid from UserRoles where RoleID = 'expert') and u.Active = 1;
END;
go


--created by Rhett 2-25-16
CREATE PROCEDURE Admin.spSelectUser (
	@UserID int
)
AS
BEGIN
	SELECT UserID, FirstName, LastName, Zip, EmailAddress, UserName,
		PassWord, Active, RegionID
	FROM Admin.Users
	WHERE UserID = @UserID;
END;
go

--created by Ryan Taylor 3-4-16, updated by Ryan 3-4-16
CREATE PROCEDURE Admin.spSelectUserByUserName (
    @username VARCHAR(20)
)
AS
BEGIN
	SELECT UserID, UserName, FirstName, LastName, Zip, EmailAddress, RegionID, Active
    FROM [Admin].[Users]
    WHERE username = @username
END
GO

--created by Ryan Taylor 3-4-16
CREATE PROCEDURE Admin.spSelectUserWithUsernameAndPassword (
    @username VARCHAR(20),
    @password VARCHAR(150)
)
AS
BEGIN
    SELECT COUNT(UserName)
    FROM [Admin].[Users]
    WHERE username = @username
    AND password = @password
    AND active = 1;
END;
GO

--Created by Chris Schwebach 2-25-16
CREATE PROCEDURE Admin.spUpdateUserPersonalInfo (
	@UserID int,
	@FirstName varchar(50),
	@LastName varchar(100),
	@Zip char(9),
	@EmailAddress varchar(100),
	@regionID int)
AS
BEGIN
     UPDATE Admin.Users
		     SET     FirstName = @FirstName,
					 LastName  = @LastName, 
					 Zip       = @Zip,
					 EmailAddress =  @EmailAddress, 
					 regionID = @regionID				 
			WHERE userID = @userID;
	return @@ROWCOUNT;    
END;
go

Create procedure Admin.spSelectUserPersonalInfo (
	@UserID int
)
as
begin
	SELECT FirstName, LastName, Zip, EmailAddress, RegionID
	FROM Admin.Users
	WHERE UserID = @UserID;
end;
go

--created by ibrahim 2-19-16
CREATE PROCEDURE Admin.spUpdateUserRemove (
@userID int)
AS
BEGIN
   UPDATE Admin.Users 
	    SET Active = 0 
		WHERE UserID = @UserID;

	return @@ROWCOUNT; 
END;
go
 --created by Ryan Taylor 3-4-16 
CREATE PROCEDURE Admin.spUpdatePassword (
    @username VARCHAR(20),
    @oldPassword VARCHAR(150),
    @newPassword VARCHAR(150)
)
AS
BEGIN
    UPDATE Admin.Users
    SET password = @newPassword
    WHERE username = @username
    AND password = @oldPassword
    AND active = 1
    RETURN @@rowcount;
END;
GO

-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Admin.spSelectUsersByOrganization (
	@OrganizationID		INT
)
AS 
BEGIN
SELECT DISTINCT u.UserID, u.UserName, u.FirstName, u.LastName, u.EmailAddress, gm.Leader, gm.CreatedDate
FROM Admin.Users AS u
INNER JOIN Gardens.GroupMembers AS gm
	ON u.UserID = gm.UserID
INNER JOIN Gardens.Groups AS g
	ON gm.GroupID = g.GroupID
INNER JOIN Gardens.Organizations AS o
	ON g.OrganizationID = o.OrganizationID
WHERE g.OrganizationID = @OrganizationID AND u.Active = 1 AND g.Active = 1;
END;
GO

--created by trent cullinin 3-26-16
create procedure admin.spSelectUserNameCount(
	@UserName		varchar(20)
)
as begin
	select count(*) as Match
	from admin.users as u
	where u.UserName = @UserName;
end
go

--created by trent cullinin 3-26-16
create procedure admin.spSelectUserInformationCount(
	@UserName		varchar(20),
	@EmailAddress	varchar(100),
	@PassWord		varchar(150)
)
as begin
	select count(*) as Verification
	from admin.users as u
	where u.Active = 1 and
		u.UserName = @UserName and 
		u.PassWord = @PassWord and
		u.EmailAddress = @EmailAddress;
end
go


--Updated by Trevor Glisch 4-1-16
CREATE PROCEDURE Admin.spGetUserCount
AS BEGIN
	SELECT count(*)
	FROM Admin.Users
	WHERE Active = 1

	
	RETURN @@ROWCOUNT
END
GO


/*	
CREATE PROCEDURE Admin.spSelectAllUserNames
AS
BEGIN
	SELECT Username
	FROM Admin.Users
	WHERE Active = 1;
END;
go
*/

--updated by ryan taylor 4-22-16
CREATE PROCEDURE Admin.spSelectAllUserNames
AS
BEGIN
	SELECT Username, FirstName, LastName
	FROM Admin.Users
	WHERE Active = 1;
END;
go

------------------------------------------
-----------Donations.EquipmentDonated-----
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016 
create procedure Donations.spInsertEquipmentDonated(
	@EquipmentName varchar(50),
	@EquipmentQuntity int,
	@DateDonated smalldatetime,
	@UserID int,
	@ShippingNotes varchar(255),
	@StateLocated char(2))
as
begin
insert into Donations.EquipmentDonated(
	EquipmentName,
	EquipmentQuantity,
	DateDonated,
	UserID,
	ShippingNotes,
	StateLocated)
values(
	@EquipmentName,
	@EquipmentQuntity,--
	@DateDonated,
	@UserID,
	@ShippingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.EquipmentNeeded------
------------------------------------------
	
--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016 
create procedure Donations.spInsertEquipmentNeeded(
	@EquipmentName varchar(50),
	@EquipmentQuantity int,
	@dateDonated smalldatetime,
	@UserID int,
	@GroupID int,
	@ReceivingNotes varchar(255),
	@StateLocated char(2))
as 
begin
insert into Donations.EquipmentNeeded(
	EquipmentName,
	EquipmentQuantity,
	DateDonated,
	UserID,
	GroupID,
	ReceivingNotes,
	StateLocated)
values(
	@EquipmentName,
	@EquipmentQuantity,
	@dateDonated,
	@UserID,
	@GroupID,
	@ReceivingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.EquipmentPendingTrans
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016 
create procedure Donations.spInsertEquipmentPendingTrans(
	@EquipmentDonatedID int,
	@EquipmentNeededID int,
	@Date smalldatetime,
	@UserID int,
	@GroupID int)
as 
begin
insert into Donations.EquipmentPendingTrans(
	EquipmentDonatedID,
	EquipmentNeededID,
	Date,
	UserID,
	GroupID)
values(
	@EquipmentDonatedID,
	@EquipmentNeededID,
	@Date,
	@UserID,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.LandDonated----------
------------------------------------------

create procedure Donations.spInsertLandDonated(
	@UserID int,
	@Size int,
	@Address varchar(100),
	@City varchar (30),
	@State char(2),
	@Zip char(9),
	@Notes varchar(255),
	@DateDonated smalldatetime)
as 
begin
insert into Donations.LandDonated(
	UserID,
	Size,
	Address,
	City,
	state,
	zip,
	Notes,
	DateDonated)
values(
	@UserID,
	@Size,
	@Address,
	@City,
	@State,
	@Zip,
	@Notes,
	@DateDonated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.LandNeeded-----------
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertLandNeeded(
	@UserID int,
	@DateNeeded smalldatetime,
	@DateRequested smalldatetime,
	@Notes varchar(255),
	@Zip varchar(9),
	@City varchar(30),
	@GroupID int)
as
begin
insert into Donations.LandNeeded(
	UserID,
	DateNeeded,
	DateRequested,
	Notes,
	Zip,
	City,
	GroupID)
values(
	@UserID,
	@DateNeeded,
	@DateRequested,
	@Notes,
	@Zip,
	@City,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.LandPendingTrans-----
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertLandPendingTrans(
	@LandDonated int,
	@LandNeeded int,
	@DateCompleted smalldatetime,
	@Notes varchar(255),
	@ExpirationDate smalldatetime,
	@TransDate smalldatetime,
	@UserID int,
	@GroupID int)
as
begin
insert into Donations.LandPendingTrans(
	LandDonated,
	LandNeeded,
	DateCompleted,
	Notes,
	ExpirationDate,
	TransDate,
	UserID,
	GroupID)
values(
	@LandDonated,
	@LandNeeded,
	@DateCompleted,
	@Notes,
	@ExpirationDate,
	@TransDate,
	@UserID,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.MoneyDonated---------
------------------------------------------

create procedure Donations.spInsertMoneyDonated(
	@UserID int,
	@Location varchar(50),
	@Amount decimal,
	@DateCreated smalldatetime)
as
begin
insert into Donations.MoneyDonated(
	UserID,
	Location,
	Amount,
	DateCreated)
values(
	@UserID,
	@Location,
	@Amount,
	@DateCreated);
	return @@ROWCOUNT;
end;
go

-- Ibrahim Abuzaid 4-1-2016
CREATE PROCEDURE Admin.spUpdateUserRoleActive (
@userID int,
@RoleID varchar(30),
@Active bit)
AS
BEGIN
      UPDATE Admin.UserRoles 
	    SET Active = @Active 
		WHERE UserID = @UserID   AND
                      RoleID = @RoleID;

	return @@ROWCOUNT;  
END;
GO
------------------------------------------
-----------Donations.MoneyNeeded----------
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertMoneyNeeded(
	@UserId int,
	@Location varchar(50),
	@Amount decimal,
	@DateCreated smalldatetime,
	@GroupID int)
as
begin
insert into Donations.MoneyNeeded(
	UserID,
	Location,
	Amount,
	DateCreated,
	GroupID)
values(
	@UserId,
	@Location,
	@Amount,
	@DateCreated,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.MoneyPendingTrans----
------------------------------------------

-- motified by Sara Nanke 3/5/16
-- monet > money
--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertMoneyPendingTrans(
	@NeedID int,
	@DonationID int,
	@UserID int,
	@Date smalldatetime,
	@GroupID int)
as
begin
insert into Donations.MoneyPendingTrans(
	NeedID,
	DonationID,
	UserID,
	Date,
	GroupID)
values(
	@NeedID,
	@DonationID,
	@UserID,
	@Date,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SeedsDonated---------
------------------------------------------

--updated due to Chris Schwebach's table update by Chris Sheehan 2-19-2016
create procedure Donations.spInsertSeedsDonated(
	@UserID int,
	@Quantity int,
	@SeedType varchar(50),
	@Date smalldatetime,
	@ShippingNotes varchar(255),
	@StateLocated char(2))
as 
begin
insert into Donations.SeedsDonated(
	UserID,
	Quantity,
	SeedType,
	Date,
	ShippingNotes,
	StateLocated)
values(
	@UserID,
	@Quantity,
	@SeedType,
	@Date,
	@ShippingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SeedsNeeded----------
------------------------------------------


--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSeedsNeeded(
	@UserID int,
	@NeededAmount int,
	@SeedType varchar(50),
	@Date smalldatetime,
	@RecievingNotes varchar(255),
	@StateLocated char(2),
	@GroupID int)
as
begin
insert into Donations.SeedsNeeded (
	UserID,
	NeededAmount,
	SeedType,
	Date,
	RecievingNotes,
	StateLocated,
	GroupID)
values(
	@UserID,
	@NeededAmount,
	@SeedType,
	@Date,
	@RecievingNotes,
	@StateLocated,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SeedsPendingTrans----
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSeedsPendingTrans(
	@SeedsDonatedID int,
	@SeedsNeededID int,
	@UserID int,
	@Date smalldatetime,
	@GroupID int)
as
begin
insert into Donations.SeedsPendingTrans (
	SeedsDonatedID,
	SeedsNeededID,
	UserID,
	Date,
	GroupID)
values(
	@SeedsDonatedID,
	@SeedsNeededID,
	@UserID,
	@Date,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SoilDonated----------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSoilDonated(
	@SoilType varchar(50),
	@UserID int,
	@SoilName varchar(75),
	@Quantity int,
	@Date smalldatetime,
	@ShippingNotes varchar(255),
	@StateLocated char(2))
as
begin
insert into Donations.SoilDonated(
	SoilType,
	UserID,
	SoilName,
	Quantity,
	Date,
	ShippingNotes,
	StateLocated)
values(
	@SoilType,
	@UserID,
	@SoilName,
	@Quantity,
	@Date,
	@ShippingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SoilNeeded-----------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSoilNeeded(
	@SoilType varchar(50),
	@UserID int,
	@SoilName varchar(75),
	@Quantity int,
	@Date smalldatetime,
	@RecievingNotes varchar(255),
	@StateLocated char(2),
	@GroupID int)
as
begin
insert into Donations.SoilNeeded(
	SoilType,
	UserID,
	SoilName,
	Quantity,
	Date,
	RecievingNotes,
	StateLocated,
	GroupID)
values(
	@SoilType,
	@UserID,
	@SoilName,
	@Quantity,
	@Date,
	@RecievingNotes,
	@StateLocated,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SoilPendingTrans-----
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSoilPendingTrans(
	@SoilNeededID int,
	@SoilDonatedID int,
	@UserID int,
	@Date smalldatetime,
	@GroupID int)
as
begin
insert into Donations.SoilPendingTrans(
	SoilNeededID,
	SoilDonatedID,
	UserID,
	Date,
	GroupID)
values(
	@SoilNeededID,
	@SoilDonatedID,
	@UserID,
	@Date,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SupplyDonated--------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSupplyDonated(
	@userID int,
	@SupplyName varchar(50),
	@SupplyAmount decimal,
	@Date smalldatetime,
	@ShippingNotes varchar(255),
	@StateLocated char(2))
as
begin
insert into Donations.SupplyDonated(
	userID,
	SupplyName,
	SupplyAmount,
	Date,
	ShippingNotes,
	StateLocated)
values (
	@userID,
	@SupplyName,
	@SupplyAmount,
	@Date,
	@ShippingNotes,
	@StateLocated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SupplyNeeded---------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSupplyNeeded(
	@userID int,
	@SupplyName varchar(50),
	@SupplyAmount decimal,
	@Date smalldatetime,
	@RecievingNotes varchar(255),
	@StateLocated char(2),
	@GroupID int)
as
begin
insert into Donations.SupplyNeeded(
	userID,
	SupplyName,
	SupplyAmount,
	Date,
	RecievingNotes,
	StateLocated,
	GroupID)
values(
	@userID,
	@SupplyName,
	@SupplyAmount,
	@Date,
	@RecievingNotes,
	@StateLocated,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.SupplyPendingTrans---
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertSupplyPendingTrans(
	@SupplyNeededID int,
	@SupplyDonatedID int,
	@UserID int,
	@Date smalldatetime,
	@GroupID int)
as
begin
insert into Donations.SupplyPendingTrans(
	SupplyNeededID,
	SupplyDonatedID,
	UserID,
	Date,
	GroupID)
values(
	@SupplyNeededID,
	@SupplyDonatedID,
	@UserID,
	@Date,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.TimeNeeded-----------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertTimeNeeded(
	@UserID int, 
	@DateNeeded smalldatetime,
	@GardenAffiliation varchar(50),
	@Location char(9),
	@Date smalldatetime,
	@CityGardenLocated varchar(30),
	@GroupID int)
as
begin
insert into Donations.TimeNeeded(
	UserID, 
	DateNeeded,
	GardenAffiliation,
	Location,
	Date,
	CityGardenLocated,
	GroupID)
values(
	@UserID, 
	@DateNeeded,
	@GardenAffiliation,
	@Location,
	@Date,
	@CityGardenLocated,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.TimePledge-----------
------------------------------------------
	
--added by Sara Nanke 3/5/16
create procedure Donations.spInsertTimePledge(
	@UserID int,
	@StartTime smalldatetime,
	@FinishTime smalldatetime,
	@DatePledge smalldatetime,
	@Affiliation varchar(75),
	@Location char(9),
	@Date smalldatetime,
	@CityPledging varchar(30))
as
begin
insert into Donations.TimePledge(
	UserID,
	StartTime,
	FinishTime,
	DatePledge,
	Affiliation,
	Location,
	Date,
	CityPledging)
values(
	@UserID,
	@StartTime,
	@FinishTime,
	@DatePledge,
	@Affiliation,
	@Location,
	@Date,
	@CityPledging);
	return @@ROWCOUNT;
end;
go

------------------------------------------
------Donations.TimePledgePendingTrans----
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertTimePledgeTrans(
	@TimePledgeID int,
	@TimeNeededID int,
	@DateMatched smalldatetime,
	@City varchar(30),
	@GroupID int)
as
begin
insert into Donations.TimePledgeTrans(
	TimePledgeID,
	TimeNeededID,
	DateMatched,
	City,
	GroupID)
values(
	@TimePledgeID,
	@TimeNeededID,
	@DateMatched,
	@City,
	@GroupID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Donations.VolunteeerHours------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Donations.spInsertVolunteers(
	@UserID int,
	@Date smalldatetime,
	@City varchar(30))
as
begin
insert into Donations.VolunteerHours(
	UserID,
	Date,
	City)
values(
	@UserID,
	@Date,
	@City);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.BecomeAnExpert----------
------------------------------------------

create procedure Expert.spInsertExpertBecomeAnExpert(
	@Username int,
	@WhyShouldIBeAnExpert varchar(200),
	@ApprovedBy int,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.BecomeAnExpert(
	UserName,
	WhyShouldIBeAnExpert,
	ApprovedBy,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@UserName,
	@WhyShouldIBeAnExpert,
	@ApprovedBy,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.BlogEntry---------------
------------------------------------------

create procedure Expert.spInsertBlogEntry(
	@BlogData varchar(max),
	@BlogTitle varchar(200),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as 
begin
insert into Expert.BlogEntry(
	BlogData,
	BlogTitle,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@BlogData,
	@BlogTitle,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

--added by Sara Nanke 3/24/16
create procedure Expert.spSelectBlogs(
	@BlogID int = null,
	@BlogTitle varchar(200) = null,
	@BlogData varchar(max) = null,
	@CreatedBy int = null,
	@CreatedDate smalldatetime = null,
	@ModifiedBy int = null,
	@ModifiedDate smalldatetime = null,
	@Active	bit = null)
AS
BEGIN
	SELECT BlogID, BlogTitle, BlogData, CreatedBy, CreatedDate, 
	ModifiedBy, ModifiedDate, Active
	FROM Expert.BlogEntry
	WHERE 
		BlogID = ISNULL(@BlogID,BlogID) AND
		BlogTitle = ISNULL(@BlogTitle,BlogTitle) AND
		BlogData = ISNULL(@BlogData,BlogData) AND
		CreatedBy = ISNULL(@CreatedBy,CreatedBy) AND
		CreatedDate = ISNULL(@CreatedDate,CreatedDate) AND
		ModifiedBy = ISNULL(@ModifiedBy,ModifiedBy) AND
		ModifiedDate = ISNULL(@ModifiedDate,ModifiedDate) AND
		Active = ISNULL(@Active,Active)
	order by CreatedDate desc
END;
go

------------------------------------------
-----------Expert.Blueprints--------------
------------------------------------------

CREATE PROCEDURE Expert.spInsertExpertBluePrints(
@Title VARCHAR(20),
@Description varchar(200),
@DateCreated SMALLDATETIME,
@CreatedBy INT,
@FilePath VARCHAR(150)
)
AS
BEGIN
INSERT INTO Expert.Template
(Title, Description, DateCreated, CreatedBy , FilePath )
VALUES
(@Title, @Description, @DateCreated, @CreatedBy, @FilePath );
return @@ROWCOUNT;
END;
GO

CREATE PROCEDURE Expert.spSelectAllBlueprints
AS
BEGIN
SELECT Title, Description , DateCreated, CreatedBy ,FilePath 
FROM Expert.Blueprints
END;
GO


CREATE PROCEDURE Expert.spSelectBlueprintByID (
@BlueprintID int
)
AS
BEGIN
SELECT Title,
 Description , DateCreated, CreatedBy ,FilePath 
FROM Expert.Blueprints
WHERE BlueprintID
= @BlueprintID ;
END;
go

------------------------------------------
-----------Expert.Content-----------------
------------------------------------------

create procedure Expert.spInsertContent(
	@UserID int,
	@RegionID int,
	@Title varchar(50),
	@Category varchar(50),
	@Content varchar(max),
	@Date smalldatetime ,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.Content(
	UserID,
	RegionID,
	Title,
	Category,
	Content,
	Date,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@UserID,
	@RegionID,
	@Title,
	@Category,
	@Content,
	@Date,
	@Createdby,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Expertise---------------
------------------------------------------

create procedure Expert.spInsertExpertise(
	@GardenTypeId varchar(20),
	@RegionID int,
	@Content varchar(max),
	@CreatedDate smalldatetime,
	@ModifiedDate smalldatetime,
	@CreatedBy int,
	@ModifiedBy int,
	@ExpertID int)
as
begin
insert into Expert.Expertise(
	GardenTypeID,
	RegionID,
	Content,
	CreatedDate,
	ModifiedDate,
	CreatedBy,
	ModifiedBy,
	ExpertID)
values(
	@GardenTypeId,
	@RegionID,
	@Content,
	@CreatedDate,
	@ModifiedDate,
	@CreatedBy,
	@ModifiedBy,
	@ExpertID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.GardenNotifications-----
------------------------------------------

create procedure Expert.spInsertGardenNotifications(
	@GardenID int,
	@NotificationID int,
	@TriggerDate smalldatetime,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.GardenNotifications(
	GardenID,
	NotificationID,
	TriggerDate,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@GardenID,
	@NotificationID,
	@TriggerDate,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.GardenPlants------------
------------------------------------------

create procedure Expert.spInsertGardenPlants(
	@GardenID int,
	@PlantID int,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime,
	@Quantity int)
as
begin
insert into Expert.GardenPlants(
	GardenID,
	PlantID,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate,
	Quantity)
values(
	@GardenID,
	@PlantID,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate,
	@Quantity);
	return @@ROWCOUNT;
end;
go	

------------------------------------------
-----------Expert.GardenTemplteUplods-----
------------------------------------------	

CREATE PROCEDURE Expert.spInsertGardenTemplate(
	@ImageName VARCHAR(50),
	@CreatedBy INT,
	@CreateDate SMALLDATETIME,
	@Active BIT,
	@ImageFile VARBINARY(MAX)
	)
AS
BEGIN
INSERT INTO Expert.GardenTemplateUploads
	(ImageName, CreatedBy, CreateDate, Active)
VALUES
	(@ImageName, @CreatedBy, @CreateDate, @Active);

INSERT INTO Expert.GardenTemplateFiles
	(ImageName, ImageFile)
VALUES
	(@ImageName, @ImageFile);
	return @@ROWCOUNT;
END;
GO

CREATE PROCEDURE Expert.spSelectAllGardenTemplateNames
AS
BEGIN
SELECT ImageName, CreateDate
FROM Expert.GardenTemplateUploads
WHERE Active = 1
END;
GO

CREATE PROCEDURE Expert.spSelectGardenTemplate(
	@FileName VARCHAR(50))
AS
BEGIN
SELECT ImageFile
FROM Expert.GardenTemplateFiles
Where ImageName = @FileName
END;
GO

------------------------------------------
-----------Expert.GardenTypes-------------
------------------------------------------	

create procedure Expert.spInsertGardenTypes(
	@Description varchar(255),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.GardenTypes(
	Description,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Description,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Notifications-----------
------------------------------------------

-- motified by Sara Nanke 3/5/16
-- Notofications > Notifications
-- ModifiedDte > ModifiedDate
create procedure Expert.spInsertNotifications(
	@Type varchar(50),
	@Description varchar(255),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.Notifications(
	Type,
	Description,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Type,
	@Description,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Nutrients---------------
------------------------------------------

create procedure Expert.spInsertNutrients(
	@Name varchar(100),
	@Description varchar(255),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.Nutrients(
	Name,
	Description,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Name,
	@Description,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

/* Rhett Allen  4/07/16 */
CREATE PROCEDURE Expert.spSelectNutrients
AS
BEGIN
	SELECT NutrientID, Name, Description, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Nutrients
END;
go

/* Rhett Allen  4/07/16 */
CREATE PROCEDURE Expert.spSelectPlantNutrients(
	@PlantID int
)
AS
BEGIN
	SELECT n.NutrientID, Name, Description, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Nutrients as n
	INNER JOIN Expert.PlantNutrients as p
	ON n.NutrientID = p.NutrientID
	WHERE p.PlantID = @PlantID
END;
go

------------------------------------------
-----------Expert.PlantCtegory------------
------------------------------------------

create procedure Expert.spInsertPlantCategory(
	@CategoryName varchar(30),
	@CreatedBy int,
	@Date smalldatetime)
as 
begin
insert into Expert.PlantCategory(
	CategoryName,
	CreatedBy,
	Date)
values(
	@CategoryName,
	@CreatedBy,
	@Date);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.PlantNutrients----------
------------------------------------------

create procedure Expert.spInsertPlantNutrients(
	@PlantID int,
	@NutrientID int)
as 
begin
insert into Expert.PlantNutrients(
	PlantID,
	NutrientID)
values(
	@PlantID,
	@NutrientID);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Plants------------------
------------------------------------------


--sara nanke 4-8-16
create procedure Expert.spInsertPlants(
	@Name varchar(100),
	@Type varchar(100),
	@Category varchar(30),
	@Description varchar(255),
	@Season varchar(10),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as 
begin
insert into Expert.Plants(
	Name,
	Type,
	Category,
	Description,
	Season,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Name,
	@Type,
	@Category,
	@Description,
	@Season,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
select cast(scope_identity() As int);
end;
go

--created by Rhett 2-25-16
CREATE PROCEDURE Expert.spUpdatePlant (
	@PlantID int,
	@Name varchar(100),
	@Type varchar(100),
	@Category varchar(30),
	@Description Varchar(255),
	@Season Varchar(10),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime,
	@Active bit,
	@OriginalName varchar(100),
	@OriginalType varchar(100),
	@OriginalCategory varchar(30),
	@OriginalDescription Varchar(255),
	@OriginalSeason Varchar(10),
	@OriginalCreatedBy int,
	@OriginalCreatedDate smalldatetime,
	@OriginalModifiedBy int,
	@OriginalModifiedDate smalldatetime,
	@OriginalActive bit
	)
AS
BEGIN
     UPDATE Expert.Plants
			 SET    Name = @Name,
					Type = @Type, 
					Category = @Category, 
					Description = @Description,
					Season = @Season, 
					CreatedBy = @CreatedBy,
					CreatedDate = @CreatedDate,	
					ModifiedBy = @ModifiedBy,	
					ModifiedDate = @ModifiedDate,	
					Active = @Active
			WHERE @PlantID = PlantID
			and @OriginalName = Name
			and @OriginalType = Type
			and @OriginalCategory = Category
			and @OriginalDescription = Description
			and @OriginalSeason = Season
			and @OriginalCreatedBy = CreatedBy
			and @OriginalCreatedDate = CreatedDate
			and @OriginalModifiedBy = ModifiedBy
			and @OriginalModifiedDate = ModifiedDate
			and @OriginalActive = Active;
	return @@ROWCOUNT;  
END;
go

CREATE PROCEDURE Expert.spSelectPlants (
	@Active	bit
)
AS
BEGIN
	SELECT PlantID, Name, Type, Category, Description, Season, 
		CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
	FROM Expert.Plants
	WHERE Active = @Active;
END;
go

CREATE PROCEDURE Expert.spSelectPlant (
	@PlantID int
)
AS
BEGIN
	SELECT PlantID, Name, Type, Category, Description, Season, 
		CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Active
	FROM Expert.Plants
	WHERE PlantID = @PlantID;
END;
go

--Added by Ibrahim 2-25-16
CREATE PROCEDURE Expert.spUpdatePlantsRemove (
@PlantID int)
AS
BEGIN
      UPDATE Expert.Plants 
	    SET Active = 0
		WHERE PlantID = @PlantID;

	return @@ROWCOUNT;  
END;
go

------------------------------------------
-----------Expert.PlantRegions------------
------------------------------------------

--Sara Nanke 4-8-16
create procedure Expert.spInsertPlantRegions(
	@PlantID int,
	@RegionID int)
as
begin
insert into Expert.PlantRegions(
	PlantID,
	RegionID)
values(
	@PlantID,
	@RegionID);
	return @@ROWCOUNT;
end;
go

--Sara Nanke 4-8-16
create procedure Expert.spSelectPlantRegions(
	@PlantID int = null,
	@RegionID int = null)
as
begin
select PlantID, RegionID
	from Expert.PlantRegions
	where 
		PlantID = ISNULL(@PlantID,PlantID) and
		RegionID = ISNULL(@RegionID,RegionID)
end;
go

------------------------------------------
-----------Expert.Question----------------
------------------------------------------

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Expert.spSelectQuestionsByUserID (
	@UserID int
)
AS
BEGIN
	SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Question
	WHERE CreatedBy = @UserID;
END;
go

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Expert.spSelectQuestionsWithKeyword (
	@Keyword varchar(max)
)
AS
BEGIN
	SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Question
	WHERE Title LIKE '%' + @Keyword + '%'
		OR Content LIKE '%' + @Keyword + '%'
END;
go

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Expert.spSelectQuestionsWithKeywordAndRegion (
	@Keyword varchar(max),
	@RegionID int
)
AS
BEGIN
	IF(@RegionID IS NULL)
        BEGIN
            SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
			FROM Expert.Question
			WHERE RegionID IS NULL
				AND (Title LIKE '%' + @Keyword + '%' OR Content LIKE '%' + @Keyword + '%')
        END
    ELSE
        BEGIN
            SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
			FROM Expert.Question
			WHERE RegionID = @RegionID
				AND (Title LIKE '%' + @Keyword + '%' OR Content LIKE '%' + @Keyword + '%')
        END
END;
go

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Expert.spSelectQuestionsWithNoRegion
AS
BEGIN
	SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Question
	WHERE RegionID IS NULL
END;
go

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Expert.spSelectQuestions
AS
BEGIN
	SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Question
END;
go

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Expert.spSelectQuestionByID (
	@QuestionID int
)
AS
BEGIN
	SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Question
	WHERE QuestionID = @QuestionID;
END;
go

/* Rhett Allen - 3/24/16 */
CREATE PROCEDURE Expert.spSelectQuestionsByRegionID (
	@RegionID int
)
AS
BEGIN
	SELECT QuestionID, Title, Category, Content, RegionID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Question
	WHERE RegionID = @RegionID;
END;
go

create procedure Expert.spInsertQuestion(
	@Title varchar(50),
	@Category varchar(50),
	@Content varchar(max),
	@RegionID int,
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as
begin
insert into Expert.Question(
	Title,
	Category,
	Content,
	RegionID,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Title,
	@Category,
	@Content,
	@RegionID,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.QuestionResponse--------
------------------------------------------

--updated by rhett 4-29-16
create procedure Expert.spInsertQuestionResponse(
	@QuestionID int,
	@Date smalldatetime,
	@Response varchar(250),
	@UserID int,
	@BlogID int)
as
begin
insert into Expert.QuestionResponse(
	QuestionID,
	Date,
	Response,
	UserID,
	BlogID)
values(
	@QuestionID,
	@Date,
	@Response,
	@UserID,
	@BlogID);
	return @@ROWCOUNT;
end;
go

--updated by rhett 4-29-16
CREATE PROCEDURE Expert.spUpdateQuestionResponse (
	@QuestionID int,
	@Response varchar(250),
	@UserID int,
	@OriginalResponse varchar(250),
	@BlogID int
	)
AS
BEGIN
     UPDATE Expert.QuestionResponse
			 SET    Response = @Response,
					BlogID = @BlogID
			WHERE @QuestionID = QuestionID
			and @UserID = UserID
			and @OriginalResponse = Response
	return @@ROWCOUNT;  
END;
go 

/* Rhett Allen - 3/24/16 */
--updated by rhett 4-29-16
CREATE PROCEDURE Expert.spSelectResponseByQuestionIDAndUser (
	@QuestionID int,
	@UserID int
)
AS
BEGIN
	SELECT QuestionID, Date, Response, UserID, BlogID
	FROM Expert.QuestionResponse
	WHERE QuestionID = @QuestionID
		AND UserID = @UserID
END;
go

/* Rhett Allen - 3/24/16 */
--updated by rhett 4-29-16
CREATE PROCEDURE Expert.spSelectResponsesByQuestionID (
	@QuestionID int
)
AS
BEGIN
	SELECT QuestionID, Date, Response, UserID, BlogID
	FROM Expert.QuestionResponse
	WHERE QuestionID = @QuestionID;
END;
go

------------------------------------------
-----------Expert.RecipeCategory----------
------------------------------------------

create procedure Expert.spInsertRecipeCategory(
	@CategoryName varchar(30),
	@CreatedBy int,
	@Date smalldatetime)
as
begin
insert into Expert.RecipeCategory(
	CategoryName,
	CreatedBy,
	Date)
values(
	@CategoryName,
	@CreatedBy,
	@Date);
	return @@ROWCOUNT;
end;
go

/* Expert.RecipeCategory */
/* Rhett Allen  4/01/16 */
CREATE PROCEDURE Expert.spSelectRecipeCategories
	@Active bit
AS
BEGIN
	SELECT CategoryName, CreatedBy, Date, Active
	FROM Expert.RecipeCategory
	WHERE Active = @Active
	ORDER BY CategoryName ASC
END;
go

------------------------------------------
-----------Expert.Recipes-----------------
------------------------------------------

--created by rhett 3-31-16
CREATE PROCEDURE Expert.spSelectRecipesWithKeywordAndCategory (
	@Keyword varchar(max),
	@Category varchar(max),
	@Offset int,
	@ReturnAmount int
)
AS
BEGIN
	IF @Category IS NULL
	BEGIN
		SELECT RecipeID, Title, Category, Directions, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
		FROM Expert.Recipes
		WHERE Title LIKE '%' + @Keyword + '%'
			OR Category LIKE '%' + @Keyword + '%'
			OR Directions LIKE '%' + @Keyword + '%'
		ORDER BY CreatedDate
		OFFSET @Offset ROWS FETCH NEXT @ReturnAmount ROWS ONLY
	END
	ELSE
		SELECT RecipeID, Title, Category, Directions, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
		FROM Expert.Recipes
		WHERE UPPER(Category) LIKE UPPER(@Category)
			AND (Title LIKE '%' + @Keyword + '%'
			OR Category LIKE '%' + @Keyword + '%'
			OR Directions LIKE '%' + @Keyword + '%')
		ORDER BY CreatedDate
		OFFSET @Offset ROWS FETCH NEXT @ReturnAmount ROWS ONLY
END;
go

--created by rhett 3-31-16
CREATE PROCEDURE Expert.spCountRecipes(
	@Keyword varchar(max),
	@Category varchar(max)
)
AS
BEGIN
	IF @Category IS NULL
	BEGIN
		SELECT COUNT(*)
		FROM Expert.Recipes
		WHERE Title LIKE '%' + @Keyword + '%'
			OR Category LIKE '%' + @Keyword + '%'
			OR Directions LIKE '%' + @Keyword + '%'
	END
	ELSE
		SELECT COUNT(*)
		FROM Expert.Recipes
		WHERE UPPER(Category) LIKE UPPER(@Category)
			AND (Title LIKE '%' + @Keyword + '%'
			OR Category LIKE '%' + @Keyword + '%'
			OR Directions LIKE '%' + @Keyword + '%')
END;
go


create procedure Expert.spInsertRecipes(
	@Title varchar(50),
	@Category varchar(30),
	@Directions varchar(max),
	@CreatedBy int,
	@CreatedDate smalldatetime,
	@ModifiedBy int,
	@ModifiedDate smalldatetime)
as 
begin
insert into Expert.Recipes(
	Title,
	Category,
	Directions,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate)
values(
	@Title,
	@Category,
	@Directions,
	@CreatedBy,
	@CreatedDate,
	@ModifiedBy,
	@ModifiedDate);
	return @@ROWCOUNT;
end;
go


/* Rhett Allen 3/27/16 */
CREATE PROCEDURE Expert.spSelectRecipesWithKeyword (
	@Keyword varchar(max),
	@Offset int,
	@ReturnAmount int
)
AS
BEGIN
	SELECT RecipeID, Title, Category, Directions, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
	FROM Expert.Recipes
	WHERE Title LIKE '%' + @Keyword + '%'
		OR Category LIKE '%' + @Keyword + '%'
		OR Directions LIKE '%' + @Keyword + '%'
	ORDER BY CreatedDate
	OFFSET @Offset ROWS FETCH NEXT @ReturnAmount ROWS ONLY
END;
go

------------------------------------------
-----------Expert.Templates---------------
------------------------------------------

create procedure Expert.spInsertTemplates(
	@UserID int,
	@Description varchar(max),
	@DateCreated smalldatetime)
as
begin
insert into Expert.Templates(
	UserID,
	Description,
	DateCreated)
values(
	@UserID,
	@Description,
	@DateCreated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Expert.Template----------------
------------------------------------------

CREATE PROCEDURE Expert.spInsertExpertTemplate(
@Title VARCHAR(20),
@Description varchar(200),
@DateCreated SMALLDATETIME,
@CreatedBy INT,
@FilePath VARCHAR(150)
)
AS
BEGIN
INSERT INTO Expert.Template
(Title, Description, DateCreated, CreatedBy , FilePath )
VALUES
(@Title, @Description, @DateCreated, @CreatedBy, @FilePath );
return @@ROWCOUNT;
END;
GO

CREATE PROCEDURE Expert.spSelectAllTemplates
AS
BEGIN
SELECT Title, Description , DateCreated, CreatedBy ,FilePath 
FROM Expert.Template
END;
GO

CREATE PROCEDURE Expert.spSelectTemplateByID (
@TemplateID int
)
AS
BEGIN
SELECT Title,
 Description , DateCreated, CreatedBy ,FilePath 
FROM Expert.Template
WHERE TemplateID
= @TemplateID ;
END;
go

------------------------------------------
-----------Gardens.Announcements----------
------------------------------------------

-- CREATE
CREATE PROCEDURE Gardens.spInsertAnnouncements (
	@UserName 				VARCHAR(20),
	@FirstName				VARCHAR(50),
	@LastName				VARCHAR(50),
	@GroupID				INT,
	@Date 					SMALLDATETIME,
	@Announcement 			VARCHAR(MAX)
)
AS
BEGIN
	INSERT INTO Gardens.Announcements(UserName, FirstName, LastName, GroupID, Date, Announcement)
	VALUES(@UserName, @FirstName, @LastName, @GroupID, @Date, @Announcement);
	RETURN @@ROWCOUNT;
END;
GO

-- RETRIEVE All By Group
CREATE PROCEDURE Gardens.spSelectAnnouncementsByGroupID (
	@GroupID 				INT
)
AS
BEGIN
	SELECT AnnouncementID, UserName, FirstName, LastName, GroupID, Date, Announcement
	FROM Gardens.Announcements
	WHERE GroupID = @GroupID
	ORDER BY Date DESC
END;
go

-- RETRIEVE TOP(10) By Group
-- Jim requests announcements page only display 10 most recent announcements
CREATE PROCEDURE Gardens.spSelectAnnouncementsByGroupIDTop10 (
	@GroupID INT
)
AS
BEGIN
	SELECT TOP(10) AnnouncementID, UserName, FirstName, LastName, GroupID, Date, Announcement
	FROM Gardens.Announcements
	WHERE GroupID = @GroupID
	ORDER BY Date DESC
END;
go

-- UPDATE
CREATE PROCEDURE Gardens.spUpdateAnnouncements (
	@AnnouncementID			INT,
	@UserName 				VARCHAR(20),
	@FirstName				VARCHAR(50),
	@LastName				VARCHAR(50),
	@GroupID				INT,
	@Date 					SMALLDATETIME,
	@Announcement 			VARCHAR(MAX)
)
AS
BEGIN
	UPDATE Gardens.Announcements
	SET
		UserName = @UserName,
		FirstName = @FirstName,
		LastName = @LastName,
		GroupID = @GroupID,
		Date = @Date,
		Announcement = @Announcement
	WHERE AnnouncementID = @AnnouncementID
END;
GO

------------------------------------------
-----------Gardens.GardenGuides-----------
------------------------------------------

create procedure Gardens.spInsertGardenGuides(
	@UserID int,
	@Content varchar(max))
as
begin
insert into Gardens.GardenGuides(
	UserID,
	Content)
values(
	@UserID,
	@Content);
	return @@ROWCOUNT;
end;
go



------------------------------------------
-----------Gardens.GardenPlans------------
------------------------------------------

create procedure Gardens.spInsertGardenPlans(
	@UserID int,
	@Description varchar(max),
	@DateCreated smalldatetime)
as 
begin
insert into Gardens.GardenPlans(
	UserID,
	Description,
	DateCreated)
values(
	@UserID,
	@Description,
	@DateCreated);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Gardens.Gardens----------------
------------------------------------------

--Modified By Chris Sheehan 4-7-16
create procedure Gardens.spInsertGardens(
	@GroupID int,
	@UserID int,
	@GardenName varchar(100), 
	@GardenDescription varchar(max),
	@GardenRegion varchar(25))
as 
begin
insert into Gardens.Gardens(
	GroupID,
	UserID,
	GardenName,
	GardenDescription,
	GardenRegion)
values(
	@GroupID,
	@UserID,
	@GardenName,
	@GardenDescription,
	@GardenRegion);
	return @@ROWCOUNT;
end;
go

--Added by Chris Schwebach & Nick King
CREATE PROCEDURE Gardens.spSelectUserGardens(
	@UserID 		int
)
AS
BEGIN
	SELECT G.GardenID, G.GroupID, G.GardenName, G.UserID, G.GardenDescription, G.GardenRegion, GR.GroupName, U.UserName
	FROM Gardens.Gardens AS G, Gardens.GroupMembers AS GM, Gardens.Groups AS GR, Admin.Users AS U
	WHERE G.GroupID = GM.GroupID AND GM.UserID = @UserID AND G.GroupID = GR.GroupID AND GR.GroupLeaderID = U.UserID AND GR.Active = 1 AND G.Active = 1
	ORDER BY GR.GroupName DESC
END;
go

--- Created By : Poonam Dubey 04/07/2016
CREATE PROCEDURE [Gardens].[spSelectUserGardensOnGroup](
	@UserID 		int,
	@GroupID		int
)
AS
BEGIN
	SELECT G.GardenID, G.GardenName, G.GardenDescription
	FROM Gardens.Gardens AS G, Gardens.GroupMembers AS GM, Gardens.Groups AS GR, Admin.Users AS U
	WHERE G.GroupID = GM.GroupID AND G.GroupID = @GroupID AND GM.UserID = @UserID AND G.GroupID = GR.GroupID AND GR.GroupLeaderID = U.UserID AND GR.Active = 1 AND G.Active = 1
	ORDER BY GR.GroupName DESC
END;
go

------------------------------------------
-----------Gardens.GroupLeaders-----------
------------------------------------------

create procedure Gardens.spInsertGroupLeaders(
	@UserID int,
	@GroupID int)
as
begin
insert into Gardens.GroupLeaders(
	UserID,
	GroupID)
values(
	@UserID,
	@GroupID);
	return @@ROWCOUNT;
end;
go

-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Gardens.spUpdateGroupLeader (
	@GroupID		INT,
	@UserID			INT,
	@Active			INT = 1
)
AS 
BEGIN
	IF (SELECT 1 FROM Gardens.GroupLeaders WHERE UserID = @UserID AND GroupID = @GroupID) IS NOT NULL
		BEGIN
			UPDATE Gardens.GroupLeaders
			SET Active = @Active
			WHERE GroupID = @GroupID AND UserID = @UserID;
		END;
	ELSE
		BEGIN
			INSERT INTO Gardens.GroupLeaders (GroupID, UserID, Active)
			VALUES (@GroupID, @UserID, @Active);
		END;
END;
GO

------------------------------------------
-------Gardens.GroupLeaderRequests--------
------------------------------------------

--added by nick king 3-4-16
create procedure Gardens.spInsertGroupLeaderRequest(
	@UserID int,
	@GroupID int,
	@RequestDate smalldatetime,
	@ModifiedDate smalldatetime,
	@ModifiedBy int,
	@RequestActive bit)
as
begin
insert into Gardens.GroupLeaderRequests(
	UserID, 
	GroupID,
	RequestDate,
	ModifiedDate,
	ModifiedBy,
	RequestActive)
values(
	@UserID,
	@GroupID,
	@RequestDate,
	@ModifiedDate,
	@ModifiedBy,
	@RequestActive);
	return @@ROWCOUNT;
end;
go

-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Gardens.spSelectGroupLeaderRequests (
	@OrganizationID		INT
)
AS
BEGIN
	SELECT glr.RequestID, glr.UserID, g.GroupID, g.GroupName, u.UserName, u.FirstName, u.LastName, u.EmailAddress, glr.RequestDate
	FROM Gardens.GroupLeaderRequests AS glr
	INNER JOIN Admin.Users AS u
		ON glr.UserID = u.UserID AND u.Active = 1
	INNER JOIN Gardens.Groups AS g
		ON glr.GroupID = g.GroupID AND g.Active = 1
	WHERE g.OrganizationID = @OrganizationID AND glr.RequestActive = 1
END;
GO

-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Gardens.spUpdateOrgGroupLeaderRequest (
	@RequestID	INT,
	@UserID		INT
)
AS 
BEGIN
	UPDATE Gardens.GroupLeaderRequests 
	SET RequestActive = 0,
		ModifiedDate = GETDATE(),
		ModifiedBy = @UserID
	WHERE RequestID = @RequestID;
END;
GO

------------------------------------------
-----------Gardens.GroupMembers-----------
------------------------------------------

-- Created By: Trent Cullinan 4-1-16
CREATE PROCEDURE Gardens.spUpdateGroupMemberInactive(
	@UserID			INT,
	@GroupID		INT
)
AS BEGIN
	UPDATE Gardens.GroupMembers
	SET Active = 0
	WHERE UserID = @UserID AND GroupID = @GroupID;
END;
go

/*updated due to a table update by Trent - updated by chris sheehan 2-25-16*/
create PROCEDURE Gardens.spInsertGroupMembers(
	@GroupID int,
	@UserID int,
	@CreatedDate smalldatetime,
	@CreatedBy int,
	@Leader bit)
as
begin
insert into Gardens.GroupMembers(
	GroupID,
	UserID,
	CreatedDate,
	CreatedBy,
	Leader)
values(
	@GroupID,
	@UserID,
	@CreatedDate,
	@CreatedBy,
	@Leader);
	return @@ROWCOUNT;
end;
go



-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Gardens.spUpdateUserOrgGroupLeader (
	@OrganizationID		INT,
	@UserID				INT,
	@Leader				BIT
)
AS
BEGIN
	UPDATE gm
	SET Leader = @Leader
	FROM Gardens.GroupMembers AS gm
	INNER JOIN Gardens.Groups AS g
		ON gm.GroupID = g.GroupID
	WHERE gm.UserID = @UserID AND g.OrganizationID = @OrganizationID AND g.Active = 1;
END;
GO


-- Created By: Trent 4-4-16
CREATE PROCEDURE Gardens.spCheckLeaderStatus (
    @UserId     				INT,
    @GroupId    				INT
)
AS
BEGIN
        SELECT Leader
        FROM Gardens.GroupMembers
        WHERE GroupID = @GroupId AND UserId = @UserId
 END
 GO
 
 CREATE PROCEDURE Gardens.spUpdateGroupName (
    @GroupID    				INT,
    @OldGroupName		VARCHAR(100),
    @NewGroupName		VARCHAR(100)
)
AS
BEGIN
	UPDATE Gardens.Groups
		SET GroupName = @NewGroupName
		WHERE GroupID = @GroupID AND GroupName = @OldGroupName
	RETURN @@rowcount
END
GO

-- Created By: Trent 4-4-16
CREATE PROCEDURE Gardens.spSelectGroupByID (
	@GroupID	INT
)
AS BEGIN
	SELECT g.GroupName, g.GroupLeaderID, u.UserName, u.EmailAddress, u.FirstName, u.LastName
	FROM Gardens.Groups AS g
	INNER JOIN Admin.Users AS u
		ON g.GroupLeaderID = u.UserID
	WHERE g.GroupID = @GroupID
END;
GO


-- Created By: Trent 4-4-16
CREATE PROCEDURE Gardens.spSelectGroupMembers (
	@GroupID	INT
)
AS BEGIN
	SELECT gm.UserID, gm.CreatedDate, u.UserName, u.EmailAddress, u.FirstName, u.LastName
	FROM Gardens.GroupMembers AS gm
	INNER JOIN Admin.Users AS u
		ON gm.UserID = u.UserID AND u.Active = 1
	WHERE gm.GroupID = @GroupID AND gm.Active = 1;
END;
GO

-- Created By: Trent 4-4-16
CREATE PROCEDURE Gardens.spSelectGroupGardens (
	@GroupID	INT
)
AS BEGIN
	SELECT g.GardenID, g.GardenName, g.GardenDescription
	FROM Gardens.Gardens AS g
	WHERE g.GroupID = @GroupID
END;
go

------------------------------------------
-----------Gardens.Groups-----------------
------------------------------------------

create procedure Gardens.spInsertGroups(
	@GroupName varchar(100),
	@GroupLeaderID int,
	@OrganizationID int)
as 
begin
insert into Gardens.Groups(
	GroupName,
	GroupLeaderID,
	OrganizationID)
values(
	@GroupName,
	@GroupLeaderID,
	@OrganizationID);
	return @@ROWCOUNT;
end;
go

--created by Nicholas King 4-4-16
CREATE PROCEDURE Gardens.spSelectJoinableGroups(
	@UserID 		int,
	@Active		int
)
AS
BEGIN
	SELECT DISTINCT g.GroupID, g.GroupName, g.Active, g.GroupLeaderID, u.UserName, u.FirstName, u.LastName, u.EmailAddress
	FROM Gardens.Groups AS g
	INNER JOIN Gardens.GroupMembers AS gm
		ON g.GroupID = gm.GroupID
		and gm.Active = @Active
	INNER JOIN Admin.Users AS u
		ON g.GroupLeaderID = u.UserID 
	WHERE g.Active = @Active and gm.UserID <> @UserID 
		AND g.GroupID not in 
			(SELECT g.GroupID
			 FROM Gardens.Groups AS g, Gardens.GroupMembers AS gm
			 WHERE g.GroupID = gm.GroupID AND gm.UserID = @UserID)
		
END;
go

-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Gardens.spSelectUserGroupCount (
	@UserID				INT,
	@OrganizationID		INT
)
AS
BEGIN
	SELECT COUNT(*)
	FROM Gardens.Groups AS g
	INNER JOIN Gardens.GroupMembers AS gm
		ON g.GroupID = gm.GroupID
	WHERE gm.UserID = @UserID AND g.OrganizationID = @OrganizationID AND g.Active = 1
END;
GO

-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Gardens.spSelectOrgUserLeads (
	@UserID				INT,
	@OrganizationID		INT
)
AS 
BEGIN
	SELECT g.GroupID, g.GroupName
	FROM Gardens.Groups AS g
	INNER JOIN Gardens.GroupLeaders AS gl
		ON g.GroupID = gl.GroupID
	WHERE g.OrganizationID = @OrganizationID AND gl.UserID = @UserID AND gl.Active = 1
END;
GO

--created by Chris Schwebach 4-22-16
CREATE PROCEDURE Gardens.spSelectGroupIdByGroupName (
	@GroupName	varchar(100)
)
AS
BEGIN
	SELECT GroupID, GroupName, GroupLeaderID, Active, OrganizationID
	FROM Gardens.Groups
	WHERE GroupName = @GroupName;
END;
go

--Created By : Poonam Dubey
--Created Date : 04/06/2016
--Description : SP to fetch groups to view

CREATE PROCEDURE Gardens.spSelectGroupsToView(
	@UserID 		int
	)
AS
BEGIN
	SELECT DISTINCT g.GroupID, g.GroupName
	FROM Gardens.Groups AS g
	INNER JOIN Gardens.GroupMembers AS gm
		ON g.GroupID = gm.GroupID
		and gm.Active = 1
	INNER JOIN Admin.Users AS u
		ON g.GroupLeaderID = u.UserID 
	WHERE g.Active = 1 and gm.UserID != @UserID; 
END;
GO

-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Admin.spSelectOrgGroups (
	@OrganizationID		INT
)
AS 
BEGIN
	SELECT g.GroupID, g.GroupName, g.GroupLeaderID, g.Active, u.UserName, u.FirstName, u.LastName, u.EmailAddress
	FROM Gardens.Groups AS g
	INNER JOIN Admin.Users AS u
		ON g.GroupLeaderID = u.UserID
	WHERE g.OrganizationID = @OrganizationID AND u.Active = 1 AND g.Active = 1;
END;
GO


-- Created By: Trent Cullinan 02/20/2016
CREATE PROCEDURE Gardens.spUpdatePrimaryGroupLeader (
	@GroupID		INT,
	@UserID			INT
)
AS 
BEGIN
	UPDATE Gardens.Groups
	SET GroupLeaderID = @UserID
	WHERE GroupID = @GroupID;
END;
GO


--Updated by Trent Cullinan 4-1-16
CREATE PROCEDURE Gardens.spSelectUserGroups(
	@UserID 		int
)
AS
BEGIN
	SELECT g.GroupID, g.GroupName, g.Active, g.GroupLeaderID, u.UserName, u.FirstName, u.LastName, u.EmailAddress
	FROM Gardens.Groups AS g
	INNER JOIN Gardens.GroupMembers AS gm
		ON g.GroupID = gm.GroupID
		and gm.Active = 1
	INNER JOIN Admin.Users AS u
		ON g.GroupLeaderID = u.UserID 
	WHERE g.Active = 1 and gm.UserID = @UserID; 
END;
go

--created by Trent Cullinan 4-1-16
CREATE PROCEDURE Gardens.spDeactivateGroupByID(
	@GroupID		INT,
	@Active			INT
)
AS
BEGIN
	UPDATE Gardens.Groups
		SET Active = @Active
		WHERE GroupID = @GroupID
	RETURN @@rowcount
END
GO

--created by trent cullinan 5-5-16
create procedure Gardens.spSelectGroupByGardenID(
	@GardenID		int
)
as begin
	select g.GroupID, g.GroupLeaderID, g.GroupName, g.OrganizationID, g.Active
	from Gardens.Groups g
	inner join Gardens.Gardens ga
		on g.GroupID = ga.GroupID
	where ga.GardenID = @GardenID
end;
go

------------------------------------------
-----------Gardens.Organizations----------
------------------------------------------

--modified by Kris Johnson 3-24-16
--Modified by Chris Sheehan, removed OrganizationContact varchar(100) added OrganizationLeader int should come from userID table 2-25-16
create procedure Gardens.spInsertOrganizations(
	@OrganizationName varchar(100),
	@OrganizationLeader int,
	@ContactPhone char(10))
as 
begin
insert into Gardens.Organizations(
	OrganizationName,
	OrganizationLeader,
	ContactPhone)
values(
	@OrganizationName,
	@OrganizationLeader,
	@ContactPhone);
	return @@ROWCOUNT;
end;
go

--created by Kris Johnson 3-4-16
create procedure Gardens.spSelectOrganization(
	@OrganizationID int)
as 
begin
select GroupID,GroupName,GroupLeaderID
from Gardens.Groups 
where  OrganizationID = @OrganizationID;
end;
go

------------------------------------------
-----------Gardens.PostLineItems----------
------------------------------------------

create procedure Gardens.spInsertPostLineItems(
	@PostID int,
	@PostLineID int,
	@UserID int,
	@GroupID int,
	@DateSent smalldatetime,
	@CommentContent varchar(255))
as
begin
insert into Gardens.PostLineItems(
	PostID,
	PostLineID,
	UserID,
	GroupID,
	DateSent,
	CommentContent)
values(
	@PostID,
	@PostLineID,
	@UserID,
	@GroupID,
	@DateSent,
	@CommentContent);
	return @@ROWCOUNT;
end;

go

------------------------------------------
-----------Gardens.PostThreads------------
------------------------------------------

Create procedure Gardens.spInsertPostThreads(
	@PostType varchar(50),
	@GroupComments bit,
	@NoComments int,
	@ViewByAll bit,
	@UserID int,
	@GroupID int,
	@PostDateTime smalldatetime,
	@Content varchar(max),
	@PostTitle varchar(100))
as 
begin
insert into Gardens.PostThreads(
	PostType,
	GroupComments,
	NoComments,
	ViewByAll,
	UserID,
	GroupID,
	PostDateTime,
	Content,
	PostTitle)
values(
	@PostType,
	@GroupComments,
	@NoComments,
	@ViewByAll,
	@UserID,
	@GroupID,
	@PostDateTime,
	@Content,
	@PostTitle);
	return @@ROWCOUNT;
	end;
	
go

--create procedure Gardens.sp

------------------------------------------
-----------Gardens.Tasks------------------
------------------------------------------

--created by Nasr 3-4-16
CREATE PROCEDURE Gardens.spUpdateTasks 
	(@TaskID INT,
	@gardenID int,
	@Description VARCHAR(100),
	@Active BIT,
	@originalGardenID int,
	@OriginalTaskID INT,
	@OriginalDescription VARCHAR(100),
	@OriginalActive BIT)
 AS
 BEGIN 
	UPDATE Gardens.Tasks
	SET   
		Gardenid = @gardenID,
		Description = @Description,
		Active = @Active
		WHERE TaskID = @TaskID
		and Description = @OriginalDescription
		and Active = @OriginalActive
		and gardenID = @originalGardenID;
	RETURN @@ROWCOUNT;
END;
GO

--created by Nasr 3-4-16
CREATE PROCEDURE Gardens.spInsertTasks 
	(@GardenID int,
	@Description VARCHAR(100),
	@dateAssigned smalldatetime,
	@AssignedFrom int,
	@userNotes varchar(250))	
AS
BEGIN
INSERT INTO Gardens.Tasks
    (GardenID,
	Description,
	DateAssigned,
	AssignedFrom,
	userNotes)	
VALUES
   (@GardenID,
   @Description,
   @dateAssigned,
   @AssignedFrom,
   @userNotes);	
END;
GO

--created by Nasr Mohammed 4-10-16
CREATE PROCEDURE Gardens.spSelectListOfGardens(
	@UserID 		int
)
AS
BEGIN
	SELECT g.GardenID, g.GardenName, g.Active, g.GroupID, u.UserName, u.FirstName, u.LastName, u.EmailAddress
	FROM Gardens.Gardens AS g
	INNER JOIN Gardens.GroupMembers AS gm
		ON g.GroupID = gm.GroupID
		and gm.Active = 1
	INNER JOIN Admin.Users AS u
		ON g.GroupID = u.UserID 
	WHERE g.Active = 1 and gm.UserID = @UserID; 
END;
go 

--- Created By : Poonam Dubey 04/07/2016
CREATE PROCEDURE [Gardens].[spSelectTasksGarden](
	@GardenID int
)
AS
BEGIN
	SELECT GT.TaskID,GT.Description, GT.DateAssigned AS 'AssignedOn', AU.FirstName +  ' ' + AU.LastName AS 'AssignedTo', AUU.FirstName +  ' ' + AUU.LastName AS 'AssignedBy' 
	FROM Gardens.Tasks GT
	INNER JOIN Admin.Users AU
	ON GT.AssignedTo = AU.UserID
	INNER JOIN Admin.Users AUU
	ON GT.AssignedFrom = AUU.UserID
	WHERE GT.GardenID = @GardenID AND GT.Active = 1
END;
go

--created by nasr mohammed 4-19-16
CREATE PROCEDURE Gardens.spSelectTasks  	 	
AS
BEGIN
	SELECT TaskID, GardenID,  Description , DateAssigned, AssignedFrom, userNotes, Active
	FROM Gardens.Tasks	
END;
GO

-- Author:		Poonam Dubey
-- Create date: 19th April 2016
-- Description:	Procedure to volunteer for a task
CREATE PROCEDURE Gardens.spVolunteerForTask 
	-- Add the parameters for the stored procedure here
	@TaskID int = 0, 
	@UserID int = 0
AS
BEGIN
	UPDATE Gardens.Tasks
	SET AssignedTo = @UserID,
		DateAssigned = GETDATE()
	WHERE TaskID = @TaskID
END
GO

-- Author:		Poonam Dubey
-- Create date: 19th April 2016
-- Description:	Procedure to Mark a task as completed
CREATE PROCEDURE Gardens.spMarkTaskAsComplete 
	-- Add the parameters for the stored procedure here
	@TaskID int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Gardens.Tasks
	SET DateCompleted = GETDATE()
	WHERE TaskID = @TaskID

END
GO

-- Author:		Poonam Dubey
-- Create date: 17th April 2016
-- Description:	Procedure to deactivate task
CREATE PROCEDURE Gardens.spDeactivateTask 
	@TaskID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Gardens.Tasks
	SET Active = 0
	WHERE TaskID = @TaskID
   
END
GO

--- Created By : Chris Schwebach 04/15.2016 and Modified by Poonam dubey 04/22.2016 

--created by ponam dubey 4-22-16
CREATE PROCEDURE [Gardens].[spSelectTasksForGarden](
	@GardenID int
)
As
BEGIN
	SELECT TaskID, Description , CONVERT(VARCHAR(20),DateAssigned) 'AssignedOn',   ISNULL(CONVERT(VARCHAR(20),DateCompleted),'') 'CompletedOn', ISNULL(AUU.FirstName + ' ' + AUU.LastName,'') 'AssignedTo', AU.FirstName + ' ' + AU.LastName 'AssignedBy' , UserNotes, GT.Active , ISNULL(GT.AssignedTo,0) 
	FROM Gardens.Tasks GT
	INNER JOIN Admin.Users AU 
	ON GT.AssignedFrom = AU.UserID  
	LEFT JOIN  Admin.Users AUU ON GT.AssignedTo = AUU.UserID  
	WHERE GT.GardenID = @GardenID AND GT.Active = 1
END;
go

------------------------------------------
-----------Gardens.WorkLogs---------------
------------------------------------------

--added by Sara Nanke 3/5/16
create procedure Gardens.spInsertWorkLogs(
	@UserID int,
	@TaskID int,
	@TimeBegun smalldatetime,
	@TimeFinished smalldatetime)
as
begin
insert into Gardens.WorkLogs(
	UserID,
	TaskID,
	TimeBegun,
	TimeFinished)
values(
	@UserID,
	@TaskID,
	@TimeBegun,
	@TimeFinished);
	return @@ROWCOUNT;
end;
go

------------------------------------------
-----------Needs.Contributions------------
------------------------------------------

--created by trent cullinan 5-5-16
create procedure Needs.spInsertContributions(
	@NeedID			int,
	@UserID			int,
	@Description	varchar(255)
)
as begin
	insert into Needs.Contributions(NeedID,SentBy,Description,DateCreated,Active)
	values(@NeedID,@UserID,@Description,default,default);
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spUpdateContribution(
	@ContributionID	int,
	@Contributed	bit,
	@UserID			int
)
as begin
	declare @NeedID int = 0;
	select @NeedID = NeedID
	from Needs.Contributions
	where ContributionID = @ContributionID;	
	update Needs.Contributions 
	set Contributed = @Contributed,
		DateModified = getdate(),
		ModifiedBy = @UserID
	where ContributionID = @ContributionID;
	if @Contributed = 1
	begin
		update Needs.Needs
		set Completed = 1,
			Active = 0
		where NeedID = @NeedID;
	end;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spUpdateCancelContribution(
	@ContributionID	int
)
as begin
	update Needs.Contributions
	set Contributed = 0
	where ContributionID = @ContributionID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectGardenPendingContributions(
	@GardenID		int
)
as begin
	select c.ContributionID, c.NeedID, c.SentBy, c.Description, 
		c.Contributed, c.DateCreated, c.DateModified, c.ModifiedBy, c.Active, n.Title, u.UserName
	from Needs.Contributions as c
	inner join Needs.Needs as n
		on c.NeedID = n.NeedID and
			n.Active = 1 and
			n.Completed = 0
	inner join Admin.Users as u
		on c.SentBy = u.UserID
	where c.Active = 1 and 
		c.Contributed is null and 
		n.GardenID = @GardenID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectUsersPendingNeeds(
	@UserID			int
)
as begin
	select c.ContributionID, c.NeedID, c.Description, 
		c.DateCreated, c.DateModified, c.ModifiedBy
	from Needs.Contributions as c
	where c.Active = 1 and
		c.Contributed is null and
		c.SentBy = @UserID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectUsersMetNeeds(
	@UserID			int
)
as begin
	select c.ContributionID, c.NeedID, c.Description, 
		c.DateCreated, c.DateModified, c.ModifiedBy
	from Needs.Contributions as c
	where c.Active = 1 and
		c.Contributed = 1 and
		c.SentBy = @UserID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectUsersNonMetMeeds(
	@UserID			int
)
as begin
	select c.ContributionID, c.NeedID, c.Description, 
		c.DateCreated, c.DateModified, c.ModifiedBy 
	from Needs.Contributions as c
	where c.Active = 1 and
		c.Contributed = 0 and
		c.SentBy = @UserID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectPendingNeed(
	@ContributionID		int
)
as begin
	select c.NeedID, c.SentBy, c.Description, c.Contributed,
		c.DateCreated, c.DateModified, c.ModifiedBy
	from Needs.Contributions as c
	where c.ContributionID = @ContributionID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectUsersContributions(
	@UserID				int
)
as begin
	select c.ContributionID, c.NeedID, c.Description, c.Contributed, c.DateCreated, c.DateModified, c.ModifiedBy, c.Active, 
	n.GardenID, n.Title, n.Description, n.Completed, n.DateCreated, n.CreatedBy, n.Active
	from needs.contributions as c
	inner join needs.needs as n
		on c.NeedID = n.NeedID
	where c.SentBy = @UserID;
end;
go

------------------------------------------
-----------Needs.Needs--------------------
------------------------------------------

--created by trent cullinan 5-5-16
create procedure Needs.spSelectNeed(
	@NeedID			int
)
as begin
	select n.GardenID, n.Title, n.Description, n.NeedType, n.Active, n.Completed,
		n.DateCreated, n.CreatedBy, n.DateModified, n.ModifiedBy
	from Needs.Needs as n
	where NeedID = @NeedID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spInsertGardenNeed(
	@GardenID		int,
	@UserID			int,
	@Title			varchar(30),
	@Description	varchar(255),
	@DateCreated	smalldatetime,
	@NeedType		varchar(50) = 'General'	
)
as begin
	insert into Needs.Needs(GardenID,Title,Description,NeedType,DateCreated,CreatedBy,Active)
	values(@GardenID,@Title,@Description,@NeedType,@DateCreated,@UserID,default);
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spUpdateNeed(
	@NeedID			int,
	@UserID			int,
	@Title			varchar(30),
	@Description	varchar(255),
	@Completed		bit,
	@NeedType		varchar(50) = 'General'
)
as begin
	update Needs.Needs
	set Title = @Title,
		Description = @Description,
		NeedType = @NeedType,
		Completed = @Completed,
		DateModified = getdate(),
		ModifiedBy = @UserID
	where NeedID = @NeedID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spUpdateRemoveNeed(
	@NeedID			int
)
as begin
	update Needs.Needs
	set Active = 0
	where NeedID = @NeedID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectGardenNeeds(
	@GardenID		int
)
as begin
	select n.NeedID, n.Title, n.Description, n.NeedType, 
		n.DateCreated, n.CreatedBy, n.DateModified, n.ModifiedBy
	from Needs.Needs as n
	where n.Active = 1 and n.Completed = 0 and n.GardenID = @GardenID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectGardenActiveNeeds(
	@GardenID		int
)
as begin
	select n.NeedID, n.Title, n.Description, n.NeedType,
		n.DateCreated, n.CreatedBy, n.DateModified, n.ModifiedBy
	from Needs.Needs as n
	where n.Active = 1 and 
		n.Completed = 0 and
		n.GardenID = @GardenID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectGardenMetNeeds(
	@GardenID		int
)
as begin
	select n.NeedID, n.Title, n.Description, n.NeedType,
		n.DateCreated, n.CreatedBy, n.DateModified, n.ModifiedBy
	from Needs.Needs as n
	left join Needs.Contributions c
		on n.NeedID = c.NeedID
		and c.Active = 0
		and c.Contributed = 1
	where n.Active = 0 and
		n.Completed = 1 and
		n.GardenID = @GardenID;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectGroupsInNeed
as begin
	select distinct
	count(ga1.GardenID) as active_needs,
	count(ga2.GardenID) as completed_needs,
	g.GroupID, g.GroupName, u.UserName
	from Needs.Needs as n
	left join Gardens.Gardens as ga1
		on n.GardenID = ga1.GardenID
		and n.Active = 1
		and n.Completed = 0
	left join Gardens.Gardens as ga2
		on n.GardenID = ga2.GardenID
		and n.Active = 0
		and n.Completed = 1
	inner join Gardens.Gardens as ga
		on n.GardenID = ga.GardenID
	inner join Gardens.Groups as g
		on ga.GroupID = g.GroupID
	inner join Admin.Users as u
		on g.GroupLeaderID = u.UserID
	where g.Active = 1
	group by g.GroupID, g.GroupName, u.UserName
	having count(ga1.GardenID) > 0
	order by 1 desc;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectGroupsGardenNeeds(
	@GroupID		int
)
as begin
	select distinct
	count(g1.GardenID) as active_needs,
	count(g2.GardenID) as completed_needs,
	g.GardenID, g.GardenName, g.GardenDescription, g.GardenRegion
	from Needs.Needs as n
	inner join Gardens.Gardens as g
		on n.GardenID = g.GardenID
	left outer join Gardens.Gardens as g1
		on n.GardenID = g1.GardenID
		and n.Active = 1
		and n.Completed = 0
	left outer join Gardens.Gardens as g2
		on n.GardenID = g2.GardenID
		and n.Active = 0
		and n.Completed = 1
	where g1.GroupID = 1002 or g2.GroupID = @GroupID
	group by g.GardenID, g.GardenName, g.GardenDescription, g.GardenRegion
	having count(g1.GardenID) > 0
	order by 1 desc;
end;
go

--created by trent cullinan 5-5-16
create procedure Needs.spSelectGroupsActiveNeeds(
	@GroupID			int
)
as begin
	select n.NeedID, n.GardenID, n.Title, n.Description, n.NeedType, n.Active, n.Completed,
		n.DateCreated, n.CreatedBy, n.DateModified, n.ModifiedBy
	from Needs.Needs as n
	left join Gardens.Gardens as g
		on n.GardenID = g.GardenID
		and g.Active = 1
	where n.Active = 1 and n.Completed = 0 and g.GroupID = @GroupID;
end;
go

------------------------------------------
-----------Needs.NeedTypes----------------
------------------------------------------

--created by trent cullinan 5-5-16
create procedure Needs.spSelectNeedTypes
as begin
	select nt.NeedType, nt.Description
	from Needs.NeedTypes as nt
	where nt.Active = 1;
end;
go

/*********************************************************************************************/
/******************************* Triggers ****************************************************/
/*********************************************************************************************/

--added by Ryan Taylor 3/24/16
CREATE TRIGGER trgInsertNewGroup ON Gardens.Groups
AFTER INSERT AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @userID INT,
			@groupID INT
			 
	SELECT @userid = INSERTED.GroupLeaderID FROM INSERTED
	SELECT @groupID = INSERTED.GroupID FROM INSERTED
	INSERT INTO Gardens.GroupMembers(groupID, userID, createdDate, createdBy, leader)
	VALUES(@groupID, @userID, GETDATE(), @userID, 1)
END;
GO


/**********************************************************************************/
/******************************* Test Data ****************************************/
/**********************************************************************************/


exec Admin.spInsertUsers 'Jeff', 'Bridges', '11111', 'E@E.com', 'jeffB', '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', null;



/*inserts added by Sara Nanke 3/5/16 */

-----------------------------ADMIN-------------------------------------
print 'admin';
GO

--* spInsertRegions               		RegionID int,	SoilType varchar(20),	AverageTempSummer decimal,	AverageTempFall decimal,	AverageTempWinter decimal,	AverageTempSpring decimal,	AverageRainfall decimal,	CreatedBy int,	CreatedDate smalldatetime,	ModifiedBy int,	ModifiedDate smalldatetime
exec Admin.spInsertRegions				1					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				2					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				3					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				4					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				5					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				6					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				7					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				8					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				9					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';
exec Admin.spInsertRegions				10					,'dry'					,99.3						,66.2 						,40.5 						,58.5 						,5.8 						,1000 			,'3/7/89' 					,1000 			,'4/8/98';


go
--* spInsertUsers						@FirstName varchar(50),	@LastName varchar(100),	@Zip char(9) ,	@EmailAddress varchar(100),	@UserName varchar(20),	@Password varchar(150),	@RegionID int
exec Admin.spInsertUsers				'Sally'					,'Smith'				,'634529919'	,'sally.smith@gmail.com'	,'sSmith'				,'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8'				,null;
exec Admin.spInsertUsers				'Steve'					,'Poppers'				,'293428282'	,'steve.popper@yahoo.com'	,'sPopper'				,'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8'				,null;
exec Admin.spInsertUsers				'Al'					,'Chipper'				,'293829103'	,'al.chipper@gmail.com'		,'aChipper'				,'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8'				,null;
go
--* spInsertActivityLog					@UserID int, 	@date smalldatetime,	@LogEntry varchar(250)	@UserAction varchar(100)
exec Admin.spInsertActivityLog			1000, 			'12/12/15', 			'This is a log entry',	'logged'

--* spInsertMessage					@MessageContent varchar(250), @subject varchar(50), 	@Sender varchar(20),	@Receiver varchar(20)
exec Admin.spInsertMessage			'This is a message, wahoo!!'	,'Test Message'					,'jeffb'				,'jeffb'

--* spInsertRoles						@RoleID				@Description varchar(100),	@CreatedBy int,	@CreatedDate smalldatetime 
exec Admin.spInsertRoles				'Guest'				,'Guest Account'					,1003			,'1/4/99'
exec Admin.spInsertRoles				'User'				,'Basic User Account'						,1003			,'1/4/99'
exec Admin.spInsertRoles				'Admin'				,'Admininistrator Account'					,1003			,'1/4/99'
exec Admin.spInsertRoles				'Expert'			,'Expert Account - Admin of the Expert Tab'					,1003			,'1/4/99'
exec Admin.spInsertRoles				'GroupMember'		,'Member of a Garden'				,1003			,'1/4/99'
exec Admin.spInsertRoles				'GroupLeader'	    ,'Leader of a Garden'				,1003			,'1/4/99'
			
--* spInsertUserRoles           		@UserID int,	@RoleID int,	
exec Admin.spInsertUserRoles			1000			,'Expert'					
exec Admin.spInsertUserRoles			1001			,'Expert'		
exec Admin.spInsertUserRoles			1002			,'GroupMember'		
exec Admin.spInsertUserRoles			1003			,'GroupLeader'	
exec Admin.spInsertUserRoles			1000			,'Admin'		

-----------------------------GARDENS--------------------------------------
print 'gardens'
GO

--* spInsertOrganizations        			@OrganizationName varchar(100),	@OrganizationLeader varchar(100),	@ContactPhone char(10)
exec Gardens.spInsertOrganizations			'Hiawatha School'				,1003								,'1234567890'
	
--* spInsertGroups               			@GroupName varchar(100),	@GroupLeaderID int,	@OrganizationID int
exec Gardens.spInsertGroups					'Mrs.Smith - 3rd grade'		,1003				,1000

--* spInsertAnnouncements        			@UserID int,	@Date smalldatetime,	@OrganizationID int,	@Announcement VARCHAR(250)
--exec Gardens.spInsertAnnouncements			1000			,'3/4/89'				,1000					,'New garden templates available'

--* spInsertGardens              			@GroupID int,	@UserID int,			@GardenName				@GardenDescription varchar(max),	@GardenRegion varchar(25)
exec Gardens.spInsertGardens				1000			,1000					,'School Garden'			,'RoofTop garden'					,1

--* spInsertGardenGuides         			@UserID int,	@Content varchar(max)
exec Gardens.spInsertGardenGuides			1002			,'how to make a shoebox garden'

--* spInsertGroupLeaders         			@UserID int,	@GroupID int
exec Gardens.spInsertGroupLeaders			1001			,1000

--* spInsertGroupLeaderRequests  			@UserID int,	@GroupID int,	@RequestDate smalldatetime,	@ModifiedDate smalldatetime,	@ModifiedBy int,	@RequestActive bit
exec Gardens.spInsertGroupLeaderRequest		1002			,1000 			,'8/17/57'					,'11/7/73'						,1002				,1

--* spInsertGroupMembers         			@GroupID int,	@UserID int,	@CreatedDate smalldatetime,	@CreatedBy int,	@Leader bit
exec Gardens.spInsertGroupMembers			1000			,1000			,'9/8/69'					,1003 			,1
			
--* spInsertGardenPlans          			@UserID int,	@Description varchar(max),	@DateCreated smalldatetime
exec Gardens.spInsertGardenPlans			1003			,'building a fence'			,'1/12/13'

--* spInsertPostThreads          			@PostType varchar(50),	@GroupComments bit,	@NoComments int,	@ViewByAll bit,	@UserID int,	@GroupID int,	@PostDateTime smalldatetime,	@Content varchar(max),	@PostTitle varchar(100)
exec Gardens.spInsertPostThreads			'plant question'		,1 					,2 					,1 				,1000 			,1000			,'7/23/78'						,'How do I grow basil?'	,'Grow Basil?'
							
--* spInsertPostLineItems        			@PostID				@PostLineID int,	@UserID int,	@GroupID int,@DateSent smalldatetime,	@CommentContent varchar(255)
exec Gardens.spInsertPostLineItems			1000				,1					,1000				,1000			,'10/10/10'				,'Yes'
				
--* spInsertTasks                			@gardenID int not null,		@Description VARCHAR(100),	@dateAssigned smalldatetime,		@AssignedFrom int,	@userNotes varchar(250))
exec Gardens.spInsertTasks					1000	,						'Watering the garden'		,'4/4/44'						,1002				,'Poppy said do this, Sally'
			
--* spInsertWorkLogs             			@UserID int,	@TaskID int,	@TimeBegun smalldatetime,	@TimeFinished smalldatetime
exec Gardens.spInsertWorkLogs				1000			,1000			,'9/25/57'					,'9/26/57'


--* spInsertGroupRequest				@groupid	@UserID int,	@RequestDate smalldatetime
exec Admin.spInsertGroupRequest			1000,			1000			,'04/05/53'	

----------------------------DONATIONS------------------------------------
print 'donations'
GO

--* spInsertEquipmentDonated     				@EquipmentName varchar(50),	@EquipmentQuntity int,	@DateDonated smalldatetime,	@UserID int,	@ShippingNotes varchar(255),	@StateLocated char(2) 
exec Donations.spInsertEquipmentDonated			'shovel'					,12 					,'12/7/79'					,1002			,'shipped in good condition'	,'WA'
	
--* spInsertEquipmentNeeded      				@EquipmentName varchar(50),	@EquipmentQuantity int,	@dateDonated smalldatetime,	@UserID int,	@GroupID int,	@ReceivingNotes varchar(255),	@StateLocated char(2)
exec Donations.spInsertEquipmentNeeded			'rake'						,2 						,'8/9/87'					,1002 			,1000			,'Received'						,'NJ'	
	
--* spInsertEquipmentPendingTrans				@EquipmentDonatedID int,	@EquipmentNeededID int,	@Date smalldatetime,	@UserID int,	@GroupID int
exec Donations.spInsertEquipmentPendingTrans	1000						,1000					,'6/23/64'				,1000 			,1000
	
--* spInsertLandDonated          				@UserID int,	@Size int,	@Address varchar(100),	@City varchar (30),	@State char(2),	@Zip char(9),	@Notes varchar(255),	@DateDonated smalldatetime
exec Donations.spInsertLandDonated				1000			,300		,'123 1st st'			,'Cedar Rapids'		,'IA'			,'524023333'	,'dry and sandy'		,'8/24/92'

--* spInsertLandNeeded           				@UserID int,	@DateNeeded smalldatetime,	@DateRequested smalldatetime,	@Notes varchar(255),	@Zip varchar(9),	@City varchar(30),	@GroupID int
exec Donations.spInsertLandNeeded				1002			,'10/3/76'					,'12/4/77'						,'best if flat'			,'5240233333'		,'Cedar Rapids'		,1000			

--* spInsertLandPendingTrans     				@LandDonated int,	@LandNeeded int,	@DateCompleted smalldatetime,	@Notes varchar(255),	@ExpirationDate smalldatetime,	@TransDate smalldatetime,	@UserID int,	@GroupID int
exec Donations.spInsertLandPendingTrans			1000				,1000				,'9/22/09'						,'trans complete'		,'11/8/11'						,'12/8/11'					,1000			,1000
			
--* spInsertMoneyDonated         				@UserID int,	@Location varchar(50),	@Amount decimal,	@DateCreated smalldatetime
exec Donations.spInsertMoneyDonated				1001			,'Cedar Rapids'			,300.0				,'9/14/01'	
			
--* spInsertMoneyNeeded          				@UserId int,	@Location varchar(50),	@Amount decimal,	@DateCreated smalldatetime,	@GroupID int
exec Donations.spInsertMoneyNeeded				1002			,'Hiawatha'				,200.0				,'12/12/12'					,1000
			
--* spInsertMoneyPendingTrans	 				@NeedID int,	@DonationID int,	@UserID int,	@Date smalldatetime,	@GroupID int
exec Donations.spInsertMoneyPendingTrans		1000			,1000				,1003			,'11/21/10'				,1000
			
--* spInsertSeedsDonated         				@UserID int,	@Quantity int,	@SeedType varchar(50),	@Date smalldatetime,	@ShippingNotes varchar(255),	@StateLocated char(2)
exec Donations.spInsertSeedsDonated				1002			,2 				,'tomato'				,'5/3/09'				,'shipped'						,'IL'				
			
--* spInsertSeedsNeeded             			@UserID int,	@NeededAmount int,	@SeedType varchar(50),	@Date smalldatetime,	@RecievingNotes varchar(255),	@StateLocated char(2),	@GroupID int
exec Donations.spInsertSeedsNeeded				1003			,6 					,'carrot'				,'4/9/06'				,'recieved'						,'MI'					,1000
			
--* spInsertSeedsPendingTrans       			@SeedsDonatedID int,	@SeedsNeededID int,	@UserID int,	@Date smalldatetime,	@GroupID int
exec Donations.spInsertSeedsPendingTrans		1000					,1000				,1003			,'3/19/08'				,1000
			
--* spInsertSoilDonated             			@SoilType varchar(50),	@UserID int,	@SoilName varchar(75),	@Quantity int,	@Date smalldatetime,	@ShippingNotes varchar(255),	@StateLocated char(2)
exec Donations.spInsertSoilDonated				'sandy'					,1000			,'Clay'					,3 				,'9/8/98'				,'shipped'						,'PA'
			
--* spInsertSoilNeeded              			@SoilType varchar(50),	@UserID int,	@SoilName varchar(75),	@Quantity int,	@Date smalldatetime,	@RecievingNotes varchar(255),	@StateLocated char(2),	@GroupID int
exec Donations.spInsertSoilNeeded				'dry'					,1002			,'dry'					,5				,'4/29/14'				,'recieved'						,'PA'					,100
			
--* spInsertSoilPendingTrans        			@SoilNeededID int,	@SoilDonatedID int,	@UserID int,	@Date smalldatetime,	@GroupID int
exec Donations.spInsertSoilPendingTrans			1000				,1000				,1002			,'9/14/89'				,1000
			
--* spInsertSupplyDonated           			@userID int,	@SupplyName varchar(50),	@SupplyAmount decimal,	@Date smalldatetime,	@ShippingNotes varchar(255),	@StateLocated char(2)
exec Donations.spInsertSupplyDonated			1002			,'peppers'					,9 						,'2/28/12'				,'shipped'						,'GA'
			
--* spInsertSupplyNeeded            			@userID int,	@SupplyName varchar(50),	@SupplyAmount decimal,	@Date smalldatetime,	@RecievingNotes varchar(255),	@StateLocated char(2),	@GroupID int
exec Donations.spInsertSupplyNeeded				1001			,'basil'					,8.9 					,'3/4/15'				,'recieved'						,'IL'					,1000
			
--* spInsertSupplyPendingTrans      			@SupplyNeededID int,	@SupplyDonatedID int,	@UserID int,	@Date smalldatetime,	@GroupID int
exec Donations.spInsertSupplyPendingTrans		1000					,1000					,1001			,'5/6/09'				,1000
			
--* spInsertTimeNeeded              			@UserID int, 	@DateNeeded smalldatetime,	@GardenAffiliation varchar(50),	@Location char(9),	@Date smalldatetime,	@CityGardenLocated varchar(30),	@GroupID int
exec Donations.spInsertTimeNeeded				1001			,'2/3/98'					,'neighbor'						,'523413333'		,'11/9/03'				,'town square'					,1000		
exec Donations.spInsertTimeNeeded				1001			,'2/3/98'					,'neighbor'						,'523413333'		,'11/9/03'				,'town square'					,1000		
			
--* spInsertTimePledge              			@UserID int,	@StartTime smalldatetime,	@FinishTime smalldatetime,	@DatePledge smalldatetime,	@Affiliation varchar(75),	@Location char(9),	@Date smalldatetime,	@CityPledging varchar(30)
exec Donations.spInsertTimePledge				1001			,'12/3/92'					,'3/9/99'					,'9/8/77'					,'volunteer'				,'128433333'		,'6/7/89'				,'Waterloo'
			
--* spInsertTimePledgeTrans         			@TimePledgeID int,	@TimeNeededID int,	@DateMatched smalldatetime,	@City varchar(30),	@GroupID int
exec Donations.spInsertTimePledgeTrans			1000				,1000				,'10/5/99'					,'Iowa City'		,1000
			
--* spInsertVolunteerHours          			@UserID int,	@Date smalldatetime,	@HoursVolunteered int,	@City varchar(30)
--exec Donations.spInsertVolunteerHours			1001			,'2/22/94'				,3						,'Chicago'

-------------------------------EXPERT----------------------------------------
print 'expert'
GO
--* spInsertPlantCategory        		@CategoryName varchar(30),	@CreatedBy int,	@Date smalldatetime
exec Expert.spInsertPlantCategory		'Vegetable'					,1001			,'4/8/96'		

--* spInsertPlants               		@Name varchar(100),	@Type varchar(100),	@Category varchar(30),	@Description varchar(255),		@Season varchar(10),	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertPlants				'Red Potato'		,'Potato'			,'Vegetable'			,'Small potato with red skin'	,'summer'				,1000			,'1/2/01'					,1001				,'1/3/04'

--* spInsertExpertBecomeAnExpert       		@Username int,	@WhyShouldIBeAnExpert varchar(200),	@ApprovedBy int,	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertExpertBecomeAnExpert	1001			,'I am the best'					,1000				,1001			,'12/3/99'					,1000				,'8/20/13'


------------------------
--logic for creating text with carriage returns, used in the blog following...
------------------------
DECLARE @CarriageReturn_LineFeed VARCHAR(max);
--SET @CarriageReturn_LineFeed=REPLACE(@CarriageReturn_LineFeed,'[\r\n];',CHAR(13)+CHAR(10));
--SET @CarriageReturn_LineFeed='Line One.[\r\n];[\r\n];Line Two.[\r\n];[\r\n];Line Three.[\r\n];[\r\n];'

--* spInsertBlogEntry            		@BlogData varchar(max),		@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
SET @CarriageReturn_LineFeed='Forest trees use carbon not only for themselves; they also trade large quantities of it with their neighbours. Botanists from the University of Basel report this in the journal Science. The extensive carbon trade among trees -- even among different species -- is conducted via symbiotic fungi in the soil.[\r\n];[\r\n];It is well known that plants take up carbon dioxide from the air by photosynthesis. The resulting sugar is used to build cellulose, wood pulp (lignin), protein and lipid -- the building blocks of plants. While growing, the tree transports sugar from its leaves to the building sites: to the branches, stems, roots and to their symbiotic fungi below ground (mycorrhizal fungi).[\r\n];[\r\n];Carbon dioxide shower for trees.[\r\n];Dr. Tamir Klein and Prof. Christian K�rner of the University of Basel together with Dr. Rolf Siegwolf of the Paul Scherrer Institute (PSI) now report, that this sugar export goes further than previously thought. In a forest near Basel the researchers used a construction crane and a network of fine tubes to flood the crowns of 120 year old and 40 meter tall spruce trees with carbon dioxide that carried a label. The researchers used carbon dioxide that, compared to normal air, contains less of the rare and heavier 13C atom.[\r\n];[\r\n];While this modification made no difference for the trees, it allowed the botanists to track the carbon through the entire tree using an atomic mass spectrometer. This way they were able to trace the path of the carbon taken up by photosynthesis from the crowns down to the root tips. The researchers found the labelled carbon not only in the roots of the marked spruce trees. The roots of the neighbouring trees also showed the same marker, even though they had not received labelled carbon dioxide. This included trees from other species.[\r\n];Forest is more than the sum of its trees.[\r\n];The only way the carbon could have been exchanged from spruce to beech, pine or larch tree -- or vice versa -- is by the network of tiny fungal filaments of the shared mycorrhizal fungi. Understory plants which partner up with other types of fungi remained entirely unmarked. The research group called the discovered exchange of large quantities of carbon among completely unrelated tree species in a natural forest "a big surprise".[\r\n];[\r\n];According to the researchers, the discovery questions the concept of tree individuality with regard to the single largest constituent of the biosphere, tree carbon. Furthermore, the results of the study funded by the Swiss National Science Foundation add a new dimension to the role of mycorrhizal fungi in forests. "Evidently the forest is more than the sum of its trees," comments Prof. Christian K�rner the findings. [\r\n];[\r\n];Source: https://www.sciencedaily.com/releases/2016/04/160414144711.htm[\r\n];[\r\n];'
SET @CarriageReturn_LineFeed=REPLACE(@CarriageReturn_LineFeed,'[\r\n];',CHAR(13)+CHAR(10));
exec Expert.spInsertBlogEntry			@CarriageReturn_LineFeed	,'Forest discovery: Trees trade carbon among each other'	,1000			,'7/19/06'					,1000				,'4/4/16'

SET @CarriageReturn_LineFeed='The modern method of building is to wipe all the top soil away from a lot, build a house, and bring back 3 or 4 inches or soil or just sod and call it good.[\r\n];[\r\n];This, of course, is not good for gardening, and many people want to increase the depth of the top soil on their property. But how? I see advice posted how you just top dress your lawn, add an inch a year, or whatever. So you slowly bury your driveway and sidewalk? Other people bemoan the thought of digging up everything, digging out some of the fill soil, and then adding new top soil. Common knowledge is that there is no other way, that top soil is like fossil fuels, a nonrenewable resource we just use up. Common knowledge is wrong, you can in fact, make it.[\r\n];[\r\n];How is top soil formed to begin with, in the great American plains, where it is many feet deep, how did it form? Well, they say 1% of decaying organic matter ends up as stable humus in the soil. So over thousands of years as grass lived, and died, and animals grazed, and pooped, it formed. Dead grass and animal droppings? Is that it? Not quite.[\r\n];[\r\n];We often do not think about what happens beneath the soil, but things happen, many important things. We see plants above ground dropping leaves, and twigs, and shedding organic matter all over, but it also happens beneath the soil. Roots die off, they�re abandoned by their plant and they rot. When grasses grow up big and tall they need long deep roots, when the bison wander by and munch that grass down, they don�t need those roots anymore and some are abandoned. As those roots, which in some grasses can reach down over 10 feet deep (imagine, grass roots over 10 feet deep). The roots decay and add organic matter deep within the soil, once they decay they create channels for water, air, and nutrient infiltration. [\r\n];[\r\n];Source: http://www.gardeningblog.net/2016/03/24/how-to-add-more-topsoil-the-natural-way/[\r\n];[\r\n];'
SET @CarriageReturn_LineFeed=REPLACE(@CarriageReturn_LineFeed,'[\r\n];',CHAR(13)+CHAR(10));
exec Expert.spInsertBlogEntry			@CarriageReturn_LineFeed	,'How to add more topsoil the natural way'	,1000			,'7/19/15'					,1000				,'4/14/16'

DECLARE @Note VARCHAR (max);
SET @Note = 'If you’re tossing all your scraps into your compost pile, read this before you toss any more! Here’s some of my favourite things to regrow, though many more plants can be regrown.[crlf][crlf]Potatoes - If you have organic potatoes (those sprayed with chemicals often are sprayed to resist growth), cut the eyes out but leave a very large chunk of potato attached (1-2″)- a medium size whole potato can yield 4-6 plants. Plant in soil as you would any seed potato.[crlf][crlf]Romaine Lettuce & Cabbage - The white stem of romaine lettuce and cabbage leaves will grow roots and make a new plant – place your leaf flat in a dish, thick stem side down, with just enough water to allow the stem to stay wet, but not enough to cover the top. Mist with water daily or as needed to keep the top most. Transplant when roots are established. [crlf][crlf]Celery - Celery is very easy to regrow. Take the base/root end and place in a glass of water for a few days, once roots are established and you see the tops start to grow, transplant to soil. Leeks and green onions too![crlf][crlf]Onions - Onions are one of the easiest things in the world to replant ~ take the root end cut after you’ve cut if off your onion, and stick it room side down in soil. Cover with about 1/2″ of soil and water.[crlf][crlf]Source - http://www.garden-of-eatin.com/2014/06/24/compost-that-scrap-or-regrow-it/'
SET @Note = REPLACE(@Note,'[crlf]',CHAR(13)+CHAR(10))
--* spInsertBlogEntry            		@BlogData varchar(max),		@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertBlogEntry			@note	,'Compost that scrap – or regrow it?'	,1000			,'7/19/06'					,1002				,'2/17/87'	


--SET @Note = 'Line One.[crlf];Line Two[crlf]Line Three.'
--SET @Note = REPLACE(@Note,'[crlf]',CHAR(13)+CHAR(10))
--* spInsertBlogEntry            		@BlogData varchar(max),		@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertBlogEntry			'This is a blog about...'	,'Vegetables in Florida'	,1000			,'7/19/06'					,1002				,'2/17/87'	

--* spInsertContent              		@UserID int,	@RegionID int,	@Title varchar(50),		@Category varchar(50),	@Content varchar(max),	@Date smalldatetime ,	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertContent				1001			,1 				,'Home Page'			,'home'					,'Welcome home'			,'2/8/93'				,1000			,'9/29/91'					,1001				,'6/14/05'		
	
--* spInsertExpertise            		@GardenTypeId varchar(20),	@RegionID int,	@Content varchar(max),	@CreatedDate smalldatetime,	@ModifiedDate smalldatetime,	@CreatedBy int,	@ModifiedBy int,	@ExpertID int
exec Expert.spInsertExpertise			1000						,1 				,'Plant on roof'			,'3/19/09'					,'9/13/12'						,1002			,1001				,1000
	
--* spInsertNotifications        		@Type varchar(50),	@Description varchar(255),	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertNotifications		'Water Plants'		,'Water Your Plants'		,1001			,'12/14/02'					,1000				,'3/19/02'					
	
--* spInsertGardenNotifications  		@GardenID int,	@NotificationID int,	@TriggerDate smalldatetime,	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertGardenNotifications	1000			,1000					,'12/14/14'					,1003			,'4/18/02'					,1000				,'3/7/04'
	
--* spInsertGardenPlants         		@GardenID int,	@PlantID int,	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime,	@Quantity int
exec Expert.spInsertGardenPlants		1000			,1000			,1001			,'7/24/99'					,1001				,'8/24/99'						,4
	
--* spInsertGardenTypes          		@Description varchar(255),	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertGardenTypes			'RoofTop'					,1001			,'8/7/03'					,1001				,'8/8/03'				

--* spInsertNutrients            		@Name varchar(100),	@Description varchar(255),	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertNutrients			'Vitamin D'			,'Vitamin good for heart'	,1001			,'8/3/77'					,1000				,'12/2/99'
	
--* spInsertPlantNutrients       		@PlantID int,	@NutrientID int
exec Expert.spInsertPlantNutrients		1000			,1000
		
--* spInsertQuestion             		@Title varchar(50),			@Category varchar(50),	@Content varchar(max),		@RegionID int,	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertQuestion			'How do I grow a grape?'	,'fruit'				,'How do I grow a grape?'	,1 				,1002			,'6/5/08'					,1000				,'4/7/07'
	
--* spInsertQuestionResponse     		@QuestionID int,	@Date smalldatetime,	@Response varchar(250),		@UserID int
exec Expert.spInsertQuestionResponse	1000				,'3/18/04'				,'Start with a grape seed'	,1002			,null

-- Expert insert Plant Category
exec Expert.spInsertPlantCategory 		'Fruit'						,1001			,'3/12/16'
exec Expert.spInsertPlantCategory 		'Herb'					    ,1001			,'3/12/16'
exec Expert.spInsertPlantCategory 		'Flower'				    ,1001			,'3/12/16'
exec Expert.spInsertPlantCategory 		'Tree'					    ,1001			,'3/12/16'
exec Expert.spInsertPlantCategory 		'Annual'				    ,1001			,'3/12/16'
exec Expert.spInsertPlantCategory 		'Perenial'				    ,1001			,'3/12/16'
exec Expert.spInsertPlantCategory 		'Bush'					    ,1001			,'3/12/16'
	
--* spInsertRecipeCategory       		@CategoryName varchar(30),	@CreatedBy int,	@Date smalldatetime
exec Expert.spInsertRecipeCategory		'soup'						,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'main dish'					,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'side dish'					,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'dessert'					,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'salad'						,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'baked'						,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'beverage'					,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'grilled'					,1000			,'12/12/99'
exec Expert.spInsertRecipeCategory		'canning'					,1000			,'12/12/99'
	
--* spInsertRecipes              		@Title varchar(50),	@Category varchar(30),	@Directions varchar(max),	@CreatedBy int,	@CreatedDate smalldatetime,	@ModifiedBy int,	@ModifiedDate smalldatetime
exec Expert.spInsertRecipes				'Best Potato Soup'	,'soup'					,'Gather ingredents...'		,1001			,'9/12/88'					,1003				,'7/16/02'
	
--* spInsertTemplates            		@UserID int,	@Description varchar(max),	@DateCreated smalldatetime
exec Expert.spInsertTemplates			1002			,'Build a box garden'		,'4/17/02'

--gardens.announcements
insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 1')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 2')
go


insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 3')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 4')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 5')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 6')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 7')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 8')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 9')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 10')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 11')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 12')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 13')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 14')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 15')
go

insert into gardens.announcements (
username,
firstname,
lastname,
groupid,
date,
announcement)
values('jeffB','Jeff','Bridges','1000','04/28/2016 00:00:00','this is an announcement 16')
go

insert into Needs.NeedTypes(NeedType,Description)
values	('General','General need for a garden.');
go

