$hasher = [System.Security.Cryptography.HashAlgorithm]::Create('sha256')
$hash = $hasher.ComputeHash([System.Text.Encoding]::UTF8.GetBytes("shelf-fixed-start-chins-steak"))

$hashString = [System.BitConverter]::ToString($hash)
$final = $hashString.Replace('-', '')

$final