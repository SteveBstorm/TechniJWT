CREATE PROCEDURE [dbo].[UserPasswordUpdate]
	@Email VARCHAR(50),
	@Password VARCHAR(50)
AS 
	BEGIN
		DECLARE @Salt VARCHAR(100);
		SET @Salt = CONCAT(NEWID(), NEWID(), NEWID())

		DECLARE @password_hash VARBINARY(64);
		SET @password_hash = HASHBYTES('SHA2_512', CONCAT(@Salt, @Password, @Salt));

		UPDATE AppUser SET [Password] = @password_hash, Salt = @Salt WHERE Email LIKE @Email
	END