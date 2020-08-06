USE [dwhrms_demonoordin2]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[CharLikeIndex]
		@findit = N'''a''',
		@partten = N'''a,b'''

SELECT	@return_value as 'Return Value'

GO
