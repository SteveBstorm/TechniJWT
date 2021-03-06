CREATE PROCEDURE [dbo].[UserRegister]
	@Email VARCHAR(50),
	@Password VARCHAR(50),
	@IsAdmin BIT = 0
AS 
	BEGIN
		DECLARE @Salt VARCHAR(100);
		SET @Salt = CONCAT(NEWID(), NEWID(), NEWID())

		DECLARE @password_hash VARBINARY(64);
		SET @password_hash = HASHBYTES('SHA2_512', CONCAT(@Salt, @Password, @Salt));

		INSERT INTO AppUser (Email, [Password], Salt, IsAdmin)
		VALUES (@Email, @password_hash, @Salt, @IsAdmin)
	END