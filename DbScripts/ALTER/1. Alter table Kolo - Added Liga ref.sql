alter table dbo.kolo
add Liga_Id int 
go

alter table dbo.kolo
ADD FOREIGN KEY (Liga_Id) REFERENCES Lige(Id);
go