Create Table Admin_Login(
Ad_Id int identity primary key,
Ad_Pass varchar(255)
)

Create Table Officer_Login(
Off_Id int identity primary key,
Off_Pass varchar(255)
)

Create Table Officer_Info(
Off_Id int,
Off_Name varchar(255),
Off_Cnic varchar(255),
Off_Phone varchar(255),
Off_Email varchar(255),
Off_Pass varchar(255)
)

Create Table ScanFace(
C_No int identity primary key,
C_Name varchar(255),
C_Image nvarchar(max)
)

Create Table C_Info(
C_No int,
TrafficSection varchar(max),
ChallanBy varchar(max),
IssuanceDate datetime 
)

Create Table C_AddInfo(
C_No int,
VehicleType varchar(max),
VehicleNo varchar(max),
VoilationType varchar(max),
ChallanAmount varchar(max), 
ServiceCharges varchar(max)
)
