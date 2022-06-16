ALTER PROCEDURE "UDOM_BARCODEV2"._USP_CALLTRANS_TENGKIMLEANG(
	in DTYPE NVARCHAR(30)
	,in par1 NVARCHAR(250)
	,in par2 NVARCHAR(250)
	,in par3 NVARCHAR(250)
	,in par4 NVARCHAR(250)
	,in par5 NVARCHAR(250)
)
AS
BEGIN
	IF :DTYPE = 'OPOR' THEN 
		SELECT
			"OPOR"."DocEntry" AS DocEntry,
			"OPOR"."CardCode" AS CardCode, 
			"OPOR"."CardName" AS CardName, 
			"OPOR"."CntctCode" AS CntctCode, 
			"OPOR"."NumAtCard"AS NumAtCard,
			"OPOR"."DocNum" AS DocNum,
			"OPOR"."DocStatus" AS DocStatus,
			"OPOR"."DocDate" AS DocDate,
			"OPOR"."DocDueDate" AS DocDueDate,
			"OPOR"."TaxDate" AS TaxDate,
			"OPOR"."DocTotal" AS DocTotal,
			"OPOR"."DiscPrcnt" AS DiscPrcnt,
			"OPOR"."DiscSum" AS DiscSum
			--"OPOR"."DiscSum" AS TEST1.
		FROM "UDOM_BARCODEV2"."OPOR"  WHERE "CardCode"=:Par1 AND "DocStatus"='O' AND "DocType"='I';
	ELSE IF :DTYPE = 'POR1' THEN 
		SELECT 
			A."ItemCode" AS ItemCode, 
			A."Dscription" AS Description, 
			A."Quantity" AS Quantity, 
			A."Price" AS Price, 
			A."PriceBefDi" AS PriceBefDi,
			A."DiscPrcnt" AS DiscPrcnt,
			CAST((A."PriceBefDi"- A."Price") * A."Quantity" AS DECIMAL(18,2)) AS DiscountAmt, 
			A."VatGroup" AS VatGroup, 
			A."LineTotal" AS LineTotal,
			A."LineNum" AS LineNum,
			A."WhsCode" AS WhsCode,
			CASE 
				WHEN B."ManSerNum"='Y' THEN 'S' 
				WHEN B."ManBtchNum"='Y' THEN 'B'
				ELSE 'N' 
			END AS ManageItem,
			A."UomEntry" AS UomName,
			A."VatGroup" AS TaxCode
		FROM "UDOM_BARCODEV2"."POR1" AS A 
		LEFT JOIN UDOM_BARCODEV2."OITM" AS B ON A."ItemCode"=B."ItemCode"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE = 'OPDN' THEN 
		SELECT TOP 50
			"OPDN"."DocEntry",
			"OPDN"."CardCode", 
			"OPDN"."CardName", 
			IFNULL("OPDN"."CntctCode",0) AS "CntctCode", 
			IFNULL("OPDN"."NumAtCard",'') AS "NumAtCard",
			IFNULL("OPDN"."DocNum",0) AS "DocNum",
			IFNULL("OPDN"."DocStatus",'') AS "DocStatus",
			IFNULL("OPDN"."DocDate",'') AS "DocDate",
			IFNULL("OPDN"."DocDueDate",'') AS "DocDueDate",
			IFNULL("OPDN"."TaxDate",'') AS "TaxDate",
			IFNULL("OPDN"."DocTotal",0) AS "DocTotal",
			IFNULL("OPDN"."DiscPrcnt",0) AS "DiscPrcnt"
		FROM "UDOM_BARCODEV2"."OPDN" WHERE "DocType"='I';
	ELSE IF :DTYPE = 'PDN1' THEN 
		SELECT 
			A."ItemCode", 
			A."Dscription", 
			A."Quantity", 
			A."Price", 
			A."DiscPrcnt", 
			A."VatGroup", 
			A."LineTotal", 
			A."WhsCode"
		FROM "UDOM_BARCODEV2"."PDN1" AS A 
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE = 'PDN1' THEN 
		SELECT 
			A."ItemCode", 
			A."Dscription", 
			A."Quantity", 
			A."Price", 
			A."DiscPrcnt", 
			A."VatGroup", 
			A."LineTotal", 
			A."WhsCode"
		FROM "UDOM_BARCODEV2"."PDN1" AS A 
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE = 'GetStcok_Batch_Serial' THEN 
		SELECT 
			 A."ItemCode"
			,A."OnHand"
			,B."DistNumber" AS SerailNumber
			,C."DistNumber" AS BatchNumber
			,A."ItemName"
			,A."InvntryUom" As "UOMCode"
		FROM "UDOM_BARCODEV2"."OITM" A
		LEFT JOIN "UDOM_BARCODEV2"."OSRN" B ON A."ItemCode"=B."ItemCode" AND B."DataSource"='N'
		LEFT JOIN "UDOM_BARCODEV2"."OBTN" C ON C."ItemCode"=A."ItemCode"
		WHERE 
			CASE WHEN :par1<>'' THEN A."CodeBars" ELSE '1' END = CASE WHEN :par1<>'' THEN :par1 ELSE '1' END
		AND (CASE WHEN :par2<>'' THEN B."DistNumber" ELSE '1' END = CASE WHEN :par2<>'' THEN :par2 ELSE '1' END
		OR CASE WHEN :par3<>'' THEN C."DistNumber" ELSE '1' END = CASE WHEN :par3<>'' THEN :par3 ELSE '1' END)
		AND A."OnHand">0;
	ELSE IF :DTYPE='OCRD' THEN
		SELECT  --TOP 10
				"CardCode" AS CardCode
			   ,"CardName" AS CardName
			   ,IFNULL("Building",'') AS Address
			   ,IFNULL("Phone1",'') AS Phone
		FROM "UDOM_BARCODEV2"."OCRD" WHERE "CardType"='S';
	--Call Master Data
	ELSE IF :DTYPE='NNM1' THEN
		SELECT 
			 "Series" AS Code
			,"SeriesName" AS Name
		FROM UDOM_BARCODEV2."NNM1" 
		WHERE	  "ObjectCode"=:par1
			  AND "Indicator"=TO_NVARCHAR(YEAR(:par2)) || '-' || CASE WHEN length(TO_NVARCHAR(MONTH(:par2)))=1 -- Count Lenght if 1= then we will plus 0 coz it 1-9 of month
																		THEN 
																			'0' || TO_NVARCHAR(MONTH(:par2)) 
																		ELSE 
																			TO_NVARCHAR(MONTH(:par2)) 
																		END;
	ELSE IF :DTYPE='OSLP' THEN
		SELECT 	 "SlpCode" AS Code
				,"SlpName" AS Name
		FROM UDOM_BARCODEV2."OSLP" 
		WHERE "Active"='Y';
	ELSE IF :DTYPE='OCRN' THEN
		DECLARE currency NVARCHAR(10);
		Declare currencyBP NVARCHAR(10);
		SELECT 
			CASE WHEN "DscntRel"='L' THEN 
				(SELECT "MainCurncy" FROM UDOM_BARCODEV2."OADM") 
			ELSE 
				(SELECT "SysCurrncy" FROM UDOM_BARCODEV2."OADM") 
			END
		INTO  currencyBP
		FROM UDOM_BARCODEV2."OCRD" 
		WHERE "CardCode"=:par1;
		SELECT "Currency" INTO currency FROM UDOM_BARCODEV2."OCRD" WHERE "CardCode"=:par1; 
		IF :currency='##' THEN
			SELECT 	 "CurrCode" AS Code
					,"CurrName" AS Name
					,CASE WHEN "CurrCode"=currencyBP THEN 1 ELSE 0 END AS DefaultCurrency
			FROM UDOM_BARCODEV2."OCRN" ORDER BY DefaultCurrency DESC ;
		ELSE
			SELECT   "CurrCode" AS Code
					,"CurrName" AS Name
					,CASE WHEN "CurrCode"=currencyBP THEN 1 ELSE 0 END AS DefaultCurrency
			FROM UDOM_BARCODEV2."OCRN"
			WHERE "CurrCode"=(SELECT "Currency" FROM UDOM_BARCODEV2."OCRD" WHERE "CardCode"=:par1) 
			ORDER BY DefaultCurrency DESC; 
		END IF;
	ELSE IF :DTYPE='OITM' THEN
		SELECT 
			 "ItemCode" AS "ITEMCODE"
			,"ItemName" AS "ITEMNAME"
			,(SELECT IFNULL("Price",0) FROM UDOM_BARCODEV2."ITM1" WHERE "ItemCode"="OITM"."ItemCode" And "PriceList"=1) AS PRICE
			,CAST(IFNULL("OnHand",0) AS INT) AS QTYONHAND
			,"IUoMEntry" AS UOMNAME
			,CASE 
				WHEN "ManSerNum"='Y' THEN 'S' 
				WHEN "ManBtchNum"='Y' THEN 'B'
				ELSE 'N' 
			 END AS MANAGEITEM
			,IFNULL("CodeBars",'') AS BarCode
		FROM UDOM_BARCODEV2."OITM" WHERE "InvntItem"='Y';
	ELSE IF :DTYPE='OVTG' THEN
		SELECT "Code","Name","Rate" FROM UDOM_BARCODEV2."OVTG" WHERE "Inactive"='N' AND "Category"='I';
	ELSE IF :DTYPE='OWHS' THEN
		SELECT "WhsCode" AS Code,"WhsName" AS Name FROM UDOM_BARCODEV2."OWHS" WHERE "Locked"='N';
	ELSE IF :DTYPE='OUOM' THEN
		SELECT "UomEntry" AS Code,"UomName" AS Name FROM UDOM_BARCODEV2."OUOM" 
		WHERE "UomEntry" IN (
			SELECT "UomEntry" FROM UDOM_BARCODEV2."ITM12" WHERE "UomType"='P' AND "ItemCode"=:par1
		);
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
	END IF;
END;
