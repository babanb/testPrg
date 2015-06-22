use ADOPets_Rev

ALTER TABLE Insurance 
ADD CONSTRAINT FK_InsurancePet
FOREIGN KEY  ( PetId) references Pet(Id)