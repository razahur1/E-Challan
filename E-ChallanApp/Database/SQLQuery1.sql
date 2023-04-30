Create Table Admin_Login(
Ad_Id int primary key,
Ad_Pass varchar(255)
)

Insert into Admin_Login values(123,'hur')
Insert into Admin_Login values(456,'zain')
Insert into Admin_Login values(789,'ali')


Create Table Officer_Login(
Of_Id int foreign key references Officer_Info(Of_Id),
Of_Pass varchar(255)
)

Create Table Officer_Info(
Of_Id int primary key,
Of_Name varchar(255),
Of_Pass varchar(255),
Of_Cnic varchar(255),
Of_Email varchar(255),
Of_Phone varchar(255)
)

Create Table Above18(
C_ID int identity(1,1) primary key,
C_Image image,
C_Name varchar(max),
C_Cnic varchar(max),
C_LicenceNo varchar(max)
)

Create Table Under18(
C_ID int identity(1,1) primary key,
C_Image image,
C_Name varchar(max),
)

Create Table Challan_Info(
Challan_No int identity(100,1) primary key,
C_ID int ,
C_Name varchar(255),
IssuanceDate datetime,
ChallanBy varchar(max),
TrafficSection varchar(max), 
VehicleType varchar(max),
VehicleNo varchar(max),
VoilationType varchar(max),
ChallanAmount varchar(max), 
ServiceCharges varchar(max)
)
foreign key (C_ID) references Under18(C_ID),
foreign key (C_ID) references Above18(C_ID)

select * from Officer_Info 
select * from Officer_Login
select * from Above18

delete from Officer_Info 
delete from Officer_Login



Insert into Challan_Info values (2,'rr',12/05/2023,'hhbg','ghv','rtyyb','6778','ggg',150,20)