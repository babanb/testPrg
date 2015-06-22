
Alter Table Pet
ALTER COLUMN [ISSterile] bit NULL 

Alter Table Pet
DROP Constraint DF_Pet_IsSterile

Alter Table Pet
add Constraint DF_Pet_ISSterile DEFAULT NULL for IsSterile

