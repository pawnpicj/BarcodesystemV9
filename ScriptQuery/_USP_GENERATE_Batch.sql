--DROP PROCEDURE BARCODESYSTEMDB."_USP_GENERATE_BATCH";
CREATE PROCEDURE BARCODESYSTEMDB."_USP_GENERATE_Batch"(
	 in ITEMCODE NVARCHAR(30)
	,in QTY NVARCHAR(250))
AS
BEGIN
	
	Declare script VARCHAR(1500);
	SELECT "SCRIPT_SERIAL" INTO script 
		FROM BARCODESYSTEMDB."TBRUNNINGNUMBER";
	execute immediate :script;
	INSERT INTO "BARCODESYSTEMDB"."TBITEMSERIAL"("ITEMCODE","QTY","SERIALNUMBER","SCRIPT","TYPE") 
		   VALUES(:ITEMCODE,:QTY
		   			,(SELECT "SerialOrBatchGen" FROM BARCODESYSTEMDB."TMP_GEN_Serial_Batch")
		   			,(SELECT "SCRIPT_SERIAL" FROM BARCODESYSTEMDB."TMP_GEN_Serial_Batch")
		   			,'S');
	SELECT "SerialOrBatchGen","SCRIPT_SERIAL" FROM BARCODESYSTEMDB."TMP_GEN_Serial_Batch";
	
END;



