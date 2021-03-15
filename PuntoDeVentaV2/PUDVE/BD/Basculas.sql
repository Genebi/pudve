INSERT IGNORE INTO basculas ( idBascula, nombreBascula, puerto, baudRate, dataBits, handshake, parity, stopBits, sendData, idUsuario, predeterminada )
VALUES
	( 1, 'TORREY L-PCR-20 KG', 'COM3', '115200', '8 bit', 'None', 'None', 'One', 'P', 10, 0 ),
	( 2, 'TORREY L-PCR-40 KG', 'COM3', '115200', '8 bit', 'None', 'None', 'One', 'P', 10, 1 );