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
	ELSE IF :DTYPE = 'ORDR' THEN 
		SELECT
			"ORDR"."DocEntry" AS DocEntry,
			"ORDR"."CardCode" AS CardCode, 
			"ORDR"."CardName" AS CardName, 
			"ORDR"."CntctCode" AS CntctCode, 
			"ORDR"."NumAtCard"AS NumAtCard,
			"ORDR"."DocNum" AS DocNum,
			"ORDR"."DocStatus" AS DocStatus,
			"ORDR"."DocDate" AS DocDate,
			"ORDR"."DocDueDate" AS DocDueDate,
			"ORDR"."TaxDate" AS TaxDate,
			"ORDR"."DocTotal" AS DocTotal,
			"ORDR"."DiscPrcnt" AS DiscPrcnt,
			"ORDR"."DiscSum" AS DiscSum
			--"OPOR"."DiscSum" AS TEST1.
		FROM "UDOM_BARCODEV2"."ORDR"  WHERE "CardCode"=:Par1 AND "DocStatus"='O' AND "DocType"='I';
	ELSE IF :DTYPE = 'RDR1' THEN 
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
		FROM "UDOM_BARCODEV2"."RDR1" AS A 
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
			IFNULL("OPDN"."DiscPrcnt",0) AS "DiscPrcnt",
			IFNULL("OPDN"."Comments",'') AS "Remark",
			IFNULL("OPDN"."SlpCode",0) AS "SlpCode",
			IFNULL("OSLP"."SlpName",'') AS "SlpName",
			IFNULL("OPDN"."DocCur",'') AS "BPCurrency"
		FROM "UDOM_BARCODEV2"."OPDN" 
		LEFT JOIN "UDOM_BARCODEV2"."OSLP" ON "OSLP"."SlpCode"="OPDN"."SlpCode"
										WHERE "DocType"='I' 
										AND "OPDN"."DocStatus"='O'
										AND IFNULL("OPDN"."U_WebID",'')<>'' 
										AND "OPDN"."CardCode"=CASE WHEN :par1='' THEN "OPDN"."CardCode" ELSE :par1 END 
										AND CAST("OPDN"."DocNum" AS NVARCHAR(100))=CASE WHEN :par2='' THEN CAST("OPDN"."DocNum" AS NVARCHAR(100)) ELSE :par2 END;
	ELSE IF :DTYPE = 'PDN1' THEN 
		SELECT 
			A."ItemCode", 
			A."Dscription", 
			A."Quantity", 
			A."Price", 
			A."DiscPrcnt", 
			A."VatGroup", 
			A."LineTotal", 
			A."WhsCode",
			A."LineNum",
			CASE 
				WHEN B."ManSerNum"='Y' THEN 'S' 
				WHEN B."ManBtchNum"='Y' THEN 'B'
				ELSE 'N' 
			END AS ManageItem
		FROM "UDOM_BARCODEV2"."PDN1" AS A 
		LEFT JOIN UDOM_BARCODEV2."OITM" AS B ON A."ItemCode"=B."ItemCode"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE = 'ORPD' THEN 
		SELECT TOP 50
			"ORPD"."DocEntry",
			"ORPD"."CardCode", 
			"ORPD"."CardName", 
			IFNULL("ORPD"."CntctCode",0) AS "CntctCode", 
			IFNULL("ORPD"."NumAtCard",'') AS "NumAtCard",
			IFNULL("ORPD"."DocNum",0) AS "DocNum",
			IFNULL("ORPD"."DocStatus",'') AS "DocStatus",
			IFNULL("ORPD"."DocDate",'') AS "DocDate",
			IFNULL("ORPD"."DocDueDate",'') AS "DocDueDate",
			IFNULL("ORPD"."TaxDate",'') AS "TaxDate",
			IFNULL("ORPD"."DocTotal",0) AS "DocTotal",
			IFNULL("ORPD"."DiscPrcnt",0) AS "DiscPrcnt",
			IFNULL("ORPD"."Comments",'') AS "Remark",
			IFNULL("ORPD"."SlpCode",0) AS "SlpCode",
			IFNULL("ORPD"."DocCur",'') AS "BPCurrency"
		FROM "UDOM_BARCODEV2"."ORPD" WHERE "DocType"='I' AND IFNULL("ORPD"."U_WebID",'')<>'';
	ELSE IF :DTYPE = 'RPD1' THEN 
		SELECT 
			A."ItemCode", 
			A."Dscription", 
			A."Quantity", 
			A."Price", 
			A."DiscPrcnt", 
			A."VatGroup", 
			A."LineTotal", 
			A."WhsCode"
		FROM "UDOM_BARCODEV2"."RPD1" AS A 
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
		FROM "UDOM_BARCODEV2"."OCRD" WHERE "CardType"=:par1;
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
			SELECT "UomEntry" FROM UDOM_BARCODEV2."ITM12" WHERE "UomType"=:par2 AND "ItemCode"=:par1 --'P'
		);
	ELSE IF :DTYPE='GetSerialNumber' THEN
		SELECT 
		B."DistNumber",1 AS "Quantity"
		FROM "UDOM_BARCODEV2"."SRI1" AS A 
		LEFT JOIN "UDOM_BARCODEV2"."OSRN" AS B ON A."SysSerial"=B."SysNumber" AND A."ItemCode"=B."ItemCode"
		WHERE A."BaseType"=:par4 AND A."BaseEntry"=:par1 AND A."BaseLinNum"=:par2 AND A."ItemCode"=:par3;
	ELSE IF :DTYPE='GetBatchNumber' THEN
		SELECT 
			A."BatchNum",A."Quantity"
		FROM "UDOM_BARCODEV2"."IBT1" AS A 
		WHERE A."BaseType"=:par4 AND A."BaseEntry"=:par1 AND A."BaseLinNum"=:par2 AND A."ItemCode"=:par3; --'20'
	ELSE IF :DTYPE='OBCD' THEN
		SELECT 
			 A."BcdCode" AS "BarCode"
			,A."BcdName" AS "BarCodeName"
			,C."ItemCode" AS "ItemCode"
			,C."ItemName" AS "ItemName"
			,B."UomCode" AS "UOMCode"
			,(SELECT IFNULL("Price",0) FROM UDOM_BARCODEV2."ITM1" WHERE "ItemCode"="C"."ItemCode" And "PriceList"=1) AS "Price"
			,"IUoMEntry" AS "UOMNAME"
			,CASE 
				WHEN "ManSerNum"='Y' THEN 'S' 
				WHEN "ManBtchNum"='Y' THEN 'B'
				ELSE 'N' 
			 END AS "MANAGEITEM"
		FROM UDOM_BARCODEV2."OBCD" AS A
		LEFT JOIN UDOM_BARCODEV2."OUOM" AS B ON A."UomEntry"=B."UomEntry"
		LEFT JOIN UDOM_BARCODEV2."OITM" AS C ON A."ItemCode"=C."ItemCode"
		WHERE A."BcdCode"=:par1
			  AND A."UomEntry" IN (SELECT "UomEntry" FROM UDOM_BARCODEV2."ITM12" WHERE "UomType"='P' AND "ItemCode"=A."ItemCode");
	ELSE IF :DTYPE='Batch' THEN
		SELECT 
				A."ItemCode" AS "ItemCode",
				A."DistNumber" AS "DistNumber",
				B."Quantity" AS "Quantity",
				A."ExpDate" AS "ExpDate",
				0 AS "InputQty"
			FROM UDOM_BARCODEV2."OBTN" AS A
			LEFT JOIN UDOM_BARCODEV2."OBTQ" AS B ON A."ItemCode"=B."ItemCode" AND A."SysNumber"=B."SysNumber"
			WHERE A."ItemCode"=:par1 AND B."WhsCode"=:par2 AND B."Quantity"!=0; 
	ELSE IF :DTYPE='Serial' THEN
		SELECT 
			  T0."ItemCode"
			, T0."IntrSerial" AS "Serial"
			, 1 AS "Qty"
		FROM UDOM_BARCODEV2."OSRI" T0
		WHERE T0."Status" <> 1 AND T0."ItemCode"=:par1 AND T0."WhsCode"=:par2
		Group by T0."ItemCode", T0."ItemName", T0."SuppSerial", T0."IntrSerial";
	ELSE IF :DTYPE = 'ODLN' THEN 
		SELECT TOP 50
			"ODLN"."DocEntry",
			"ODLN"."CardCode", 
			"ODLN"."CardName", 
			IFNULL("ODLN"."CntctCode",0) AS "CntctCode", 
			IFNULL("ODLN"."NumAtCard",'') AS "NumAtCard",
			IFNULL("ODLN"."DocNum",0) AS "DocNum",
			IFNULL("ODLN"."DocStatus",'') AS "DocStatus",
			IFNULL("ODLN"."DocDate",'') AS "DocDate",
			IFNULL("ODLN"."DocDueDate",'') AS "DocDueDate",
			IFNULL("ODLN"."TaxDate",'') AS "TaxDate",
			IFNULL("ODLN"."DocTotal",0) AS "DocTotal",
			IFNULL("ODLN"."DiscPrcnt",0) AS "DiscPrcnt",
			IFNULL("ODLN"."Comments",'') AS "Remark",
			IFNULL("ODLN"."SlpCode",0) AS "SlpCode",
			IFNULL("OSLP"."SlpName",'') AS "SlpName",
			IFNULL("ODLN"."DocCur",'') AS "BPCurrency"
		FROM "UDOM_BARCODEV2"."ODLN" 
		LEFT JOIN "UDOM_BARCODEV2"."OSLP" ON "OSLP"."SlpCode"="ODLN"."SlpCode"
										WHERE "DocType"='I' 
										AND "ODLN"."DocStatus"='O'
										AND IFNULL("ODLN"."U_WebID",'')<>'' 
										AND "ODLN"."CardCode"=CASE WHEN :par1='' THEN "ODLN"."CardCode" ELSE :par1 END 
										AND CAST("ODLN"."DocNum" AS NVARCHAR(100))=CASE WHEN :par2='' THEN CAST("ODLN"."DocNum" AS NVARCHAR(100)) ELSE :par2 END;
	ELSE IF :DTYPE = 'DLN1' THEN 
		SELECT 
			A."ItemCode", 
			A."Dscription", 
			A."Quantity", 
			A."Price", 
			A."DiscPrcnt", 
			A."VatGroup", 
			A."LineTotal", 
			A."WhsCode",
			A."LineNum",
			CASE 
				WHEN B."ManSerNum"='Y' THEN 'S' 
				WHEN B."ManBtchNum"='Y' THEN 'B'
				ELSE 'N' 
			END AS ManageItem
		FROM "UDOM_BARCODEV2"."DLN1" AS A 
		LEFT JOIN UDOM_BARCODEV2."OITM" AS B ON A."ItemCode"=B."ItemCode"
		WHERE A."DocEntry"=:par1;
	ELSE IF :DTYPE = 'GETSALEORDER' THEN 
		SELECT
			A."DocEntry" AS DocEntry,
			A."DocNum" AS DocNum,
			A."CardCode" AS CardCode,
			B."CardName" AS CardName,
			'บริษัท อุดมเมดิคอล อิควิปเม้นท์ จำกัด<br>
			เลขที่ 80 ซอยพัฒนาการ69 แขวงประเวศ<br>
			เขตประเวศ กรุงเทพฯ 10250<br>
			โทร.02-320-1234 , 085-344-3444<br>' AS AddressFrom,
			CASE WHEN IFNULL(AA."Address2",'')!=''THEN IFNULL(AA."Address2",'')||', ' ELSE '' END
			||CASE WHEN IFNULL(AA."Address3",'')!=''THEN IFNULL(AA."Address3",'')||', ' ELSE '' END
			||CASE WHEN IFNULL(AA."City",'')!=''THEN IFNULL(AA."City",'')||', 'ELSE '' END
			||CASE WHEN IFNULL(AA."State",'')!=''THEN IFNULL(AA."State",'')||', 'ELSE '' END
			||CASE WHEN IFNULL(AA."County",'')!=''THEN IFNULL(AA."County",'')||', 'ELSE '' END
			||CASE WHEN IFNULL(AA."ZipCode",'')!=''THEN IFNULL(AA."ZipCode",'')||', 'ELSE '' END AS AddressTo,
			A."DocDueDate" AS DeliveryDate
		FROM "UDOM_BARCODEV2"."ORDR" AS A
		LEFT JOIN "UDOM_BARCODEV2"."OCRD" AS B ON A."CardCode"=B."CardCode"
		LEFT JOIN "UDOM_BARCODEV2"."CRD1" AS AA ON AA."Address"=A."ShipToCode"
		WHERE A."DocEntry" IN (
			SELECT DISTINCT
				A."DocEntry" 
			FROM "UDOM_BARCODEV2"."ODLN" AS A 
			LEFT JOIN "UDOM_BARCODEV2"."DLN1" AS B ON A."DocEntry"=B."DocEntry" 
			WHERE IFNULL(A."U_WebID",'')<>'' );
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
