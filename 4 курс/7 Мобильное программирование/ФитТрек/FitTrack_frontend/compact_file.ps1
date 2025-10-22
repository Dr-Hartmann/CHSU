$inputFile = "all_dart_files.txt"
$outputFile = "all_dart_files_final.txt"

if (-not (Test-Path $inputFile)) {
	Write-Error "Input file not found: $inputFile"
	exit 1
}

# Read the file as lines (preserve empty lines so we can skip them explicitly)
$raw = Get-Content $inputFile -Encoding UTF8 -Raw
$lines = $raw -split "`r?`n"

$output = @()

# A line that contains only whitespace and bracket/comma characters should be
# attached to the previous non-empty line. This pattern matches any combination
# of whitespace, brackets []{}(), and commas.
$bracketOnlyPattern = '^[\s\[\]\{\}\(\),]+$'

foreach ($line in $lines) {
	if ($null -eq $line) { continue }
	$trimmed = $line.Trim()
	if ($trimmed -eq '') { continue }

	if ($trimmed -match $bracketOnlyPattern) {
		if ($output.Count -gt 0) {
			# Append directly to the previous line without adding extra spaces
			$output[$output.Count - 1] = $output[$output.Count - 1].TrimEnd() + $trimmed
		} else {
			# No previous line, push as-is
			$output += $trimmed
		}
	} else {
		# Normalize internal whitespace now to keep lines compact
		$normalized = [regex]::Replace($trimmed, '\s{2,}', ' ')
		$output += $normalized
	}
}

# Post-processing: remove spaces before common punctuation like ), ] , } ; :
for ($i = 0; $i -lt $output.Count; $i++) {
	$l = $output[$i]
	$l = [regex]::Replace($l, '\s+([\)\]\}\,;:])', '$1')
	$output[$i] = $l.Trim()
}

# Write with CRLF
Set-Content -Path $outputFile -Value ($output -join "`r`n") -Encoding UTF8