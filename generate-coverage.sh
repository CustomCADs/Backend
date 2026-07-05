#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPORT_DIR="$ROOT_DIR/coverage-report"
ARTIFACTS_DIR="$ROOT_DIR/artifacts/coverage"
RESULTS_DIR="$ARTIFACTS_DIR/results"

rm -rf "$REPORT_DIR" "$ARTIFACTS_DIR"
mkdir -p "$RESULTS_DIR"

cd "$ROOT_DIR"

echo "Running coverage for Unit Tests"
dotnet test --solution CustomCADs.UnitTests.slnx --no-restore --coverage --coverage-output-format cobertura --results-directory "$RESULTS_DIR"

find "$RESULTS_DIR" -type f -name "*.cobertura.xml" | sort > "$ARTIFACTS_DIR/coverage-files.txt"

if [ ! -s "$ARTIFACTS_DIR/coverage-files.txt" ]; then
  echo "No coverage files were generated." >&2
  exit 1
fi

filefilters=$(tr '\n' ';' < "$ROOT_DIR/.coveragefilters" | sed 's/;$//')

reportgenerator \
  -reports:"$(tr '\n' ';' < "$ARTIFACTS_DIR/coverage-files.txt")" \
  -targetdir:"$REPORT_DIR" \
  -reporttypes:HtmlSummary \
  -filefilters:"$filefilters"

echo "Coverage report created at $REPORT_DIR"
