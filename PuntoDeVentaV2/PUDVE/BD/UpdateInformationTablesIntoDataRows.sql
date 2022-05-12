-- -----------------------------------
-- R�gimen Fiscal para el CFDI 4.0	--
-- -----------------------------------

-- -----------------------------------
-- UPDATE R�gimen Fiscal 			--
-- -----------------------------------

UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'S�' 
WHERE
	CodigoRegimen = 601;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'S�' 
WHERE
	CodigoRegimen = 603;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 605;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 606;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 607;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 608;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'S�' 
WHERE
	CodigoRegimen = 610;

UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 611;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 612;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 614;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 615;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 616;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'S�' 
WHERE
	CodigoRegimen = 620;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'S�',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 621;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'S�' 
WHERE
	CodigoRegimen = 622;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'S�' 
WHERE
	CodigoRegimen = 623;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'S�' 
WHERE
	CodigoRegimen = 624;
	
-- -----------------------------------
-- INSERT Nuevos R�gimen Fiscal		--
-- -----------------------------------
	
INSERT INTO regimenfiscal ( CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral ) SELECT
625,
'R�gimen de las Actividades Empresariales con ingresos a trav�s de Plataformas Tecnol�gicas',
'S�',
'No' 
FROM
DUAL 
WHERE
	NOT EXISTS ( SELECT CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral FROM regimenfiscal WHERE CodigoRegimen = 625 );

INSERT INTO regimenfiscal ( CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral ) SELECT
626,
'R�gimen Simplificado de Confianza',
'S�',
'S�' 
FROM
DUAL 
WHERE
	NOT EXISTS ( SELECT CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral FROM regimenfiscal WHERE CodigoRegimen = 626 );