CREATE VIEW [dbo].[V_User]
	AS SELECT Id, Email AS 'Adresse', 
	CASE 
	WHEN IsAdmin = 0 THEN 'User'
	WHEN IsAdmin = 1 THEN 'Admin'
	end
	AS 'role'

	FROM AppUser