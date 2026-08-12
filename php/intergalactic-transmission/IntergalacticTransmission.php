<?php
// Intergalactic Transmission - Parity Bit exercise.
// Yes this is just based on my GO version I did recently. 
// Requiring everything returned as formatted hex strings is new.

declare(strict_types=1);

// Return a value up to 8 bits as a two digit hex string
// example: 3 -> 0x03. DecHex would only return 3
// @param $value: Value to change to a hex string
// @returns: Hex value padded to two digits and with a 0x prefix added.
function ToHex($value): string {
    $result = "00" . dechex($value);
    return "0x" . substr($result, strlen($result) - 2, 2);
}

// Prepare a list of bytes formatted as hex strings for transmission by adding
// the parity bits and reformatting as hex strings
// @param $sequence: The bits to reformat with parity bits.
// @returns: New array with the value reformatted to include parity bits (will be longer)
function transmitSequence(array $sequence): array
{
    $result = array();
    $bits = array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    $bitCount = 0;
    $written = 0;
    foreach($sequence as $data) {
        $data = hexdec($data);        
		$written = 0;
		while ($bitCount >= 7 || $written < 8) {
			while ($bitCount < 7) {
				$bits[$bitCount] = ($data & 0b10000000) >> 7;
				$bitCount++;
				$data = $data << 1;
				$written++;
			}
            $payload = 0b00000000;
			$count1s = 0;
			for ($i = 0; $i < 7; $i++) {
				$bitCount--;
				if ($bits[$bitCount] == 1) {
					$count1s++;
					$payload |= 1 << $i;
				}
			}
			$payload = $payload << 1;
			if ($count1s%2 == 1) {
				$payload |= 1;
			}
            $result[] = ToHex($payload);
			while ($written < 8) {
				$bits[$bitCount] = ($data & 0b10000000) >> 7;
				$bitCount++;
				$data = $data << 1;
				$written++;
				if ($bitCount == 7) {
					break;
				}
			}
		}
	}

    if ($bitCount > 0) {
		while ($bitCount < 7) {
			$bits[$bitCount] = 0;
			$bitCount++;
		}
		$payload = 0b00000000;
		$count1s = 0;
		for ($i = 0; $i < 7; $i++) {
			$bitCount--;
			if ($bits[$bitCount] == 1) {
				$count1s++;
				$payload |= 1 << $i;
			}
		}
		$payload = $payload << 1;
		if ($count1s%2 == 1) {
			$payload |= 1;
		}
		$result[] = ToHex($payload);

	}

    return $result;
}

// Decode a message that was encoded hex strings to the original hex strings checking
// for parity errors
// @param $message: The array of hex strings to decode.
// @returns: Array of decoded hex strings (will normally be shorter).
function decodeMessage(array $message): array
{
	$result = array();

	$written = 0;
	$payload = 0;
	$bitCount = 0;

	foreach($message as $data) {
		// Check for parity error.
        $data = hexdec($data);
		$count1s = 0;
		$parity = $data & 0b1;
		for ($i = 1; $i < 8; $i++) {
			if (($data & (1<<$i)) != 0) {
				$count1s++;
			}
		}
		if ($count1s%2 != $parity) {
            throw new Exception("wrong parity");
		}

		$written = 0;
		while ($written < 7) {
			$payload = ($payload << 1) | (($data & 0b10000000) >> 7);
			$data = $data << 1;
			$written++;
			$bitCount++;
			if ($bitCount == 8) {
				$result[] = ToHex($payload);
				$payload = 0;
				$bitCount = 0;
			}
		}
	}
	return $result;
}
