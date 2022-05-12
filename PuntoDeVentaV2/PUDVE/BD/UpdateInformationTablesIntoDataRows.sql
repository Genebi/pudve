-- -----------------------------------
-- Régimen Fiscal para el CFDI 4.0	--
-- -----------------------------------

-- -----------------------------------
-- UPDATE Régimen Fiscal 			--
-- -----------------------------------

UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'Sí' 
WHERE
	CodigoRegimen = 601;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'Sí' 
WHERE
	CodigoRegimen = 603;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 605;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 606;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 607;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 608;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'Sí' 
WHERE
	CodigoRegimen = 610;

UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 611;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 612;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 614;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 615;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 616;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'Sí' 
WHERE
	CodigoRegimen = 620;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'Sí',
AplicaMoral = 'No' 
WHERE
	CodigoRegimen = 621;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'Sí' 
WHERE
	CodigoRegimen = 622;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'Sí' 
WHERE
	CodigoRegimen = 623;
	
UPDATE RegimenFiscal 
SET AplicaFisica = 'No',
AplicaMoral = 'Sí' 
WHERE
	CodigoRegimen = 624;
	
-- -----------------------------------
-- INSERT Nuevos Régimen Fiscal		--
-- -----------------------------------
	
INSERT INTO regimenfiscal ( CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral ) SELECT
625,
'Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas',
'Sí',
'No' 
FROM
DUAL 
WHERE
	NOT EXISTS ( SELECT CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral FROM regimenfiscal WHERE CodigoRegimen = 625 );

INSERT INTO regimenfiscal ( CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral ) SELECT
626,
'Régimen Simplificado de Confianza',
'Sí',
'Sí' 
FROM
DUAL 
WHERE
	NOT EXISTS ( SELECT CodigoRegimen, Descripcion, AplicaFisica, AplicaMoral FROM regimenfiscal WHERE CodigoRegimen = 626 );