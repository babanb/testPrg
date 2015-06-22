

IF EXISTS(SELECT * FROM sys.columns 
        WHERE [name] = N'PetIDs' AND [object_id] = OBJECT_ID(N'ShareCategoryTypeContact'))
BEGIN
   ALTER TABLE ShareCategoryTypeContact DROP COLUMN PetIDs
END

GO

BEGIN	
	ALTER TABLE ShareCategoryTypeContact ADD PetId int 
	ALTER TABLE ShareCategoryTypeContact ADD CONSTRAINT FK_ShareCTypeContact_Pet
				FOREIGN KEY (PetId) REFERENCES Pet(Id)
END


