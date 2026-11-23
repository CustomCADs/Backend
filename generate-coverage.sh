#!/usr/bin/env bash
set -euo pipefail

SKIP_TEST=false
OPEN_REPORT=false

# Parse command-line flags
while [[ $# -gt 0 ]]; do
  case $1 in
    --skip-test)
      SKIP_TEST=true
      shift
      ;;
    --open)
      OPEN_REPORT=true
      shift
      ;;
    *)
      echo "Unknown option: $1" >&2
      exit 1
      ;;
  esac
done

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPORT_DIR="$ROOT_DIR/coverage-report"
REPORT_HTML="$REPORT_DIR/index.html"
ARTIFACTS_DIR="$ROOT_DIR/artifacts/coverage"
RESULTS_DIR="$ARTIFACTS_DIR/results"

rm -rf "$REPORT_DIR"
[ "$SKIP_TEST" = false ] && rm -rf "$ARTIFACTS_DIR"
mkdir -p "$RESULTS_DIR" "$REPORT_DIR" "$ARTIFACTS_DIR"

cd "$ROOT_DIR"

if [ "$SKIP_TEST" = false ]; then
  echo "Running coverage for Unit Tests"
  dotnet test --solution CustomCADs.UnitTests.slnx --no-restore --coverage --coverage-output-format cobertura --results-directory "$RESULTS_DIR"
else
  echo "Skipping dotnet test (--skip-test flag set)"
fi

find "$RESULTS_DIR" -type f -name "*.cobertura.xml" | sort > "$ARTIFACTS_DIR/coverage-files.txt"

if [ ! -s "$ARTIFACTS_DIR/coverage-files.txt" ]; then
  echo "No coverage files were generated." >&2
  exit 1
fi

filefilters=$(tr '\n' ';' < "$ROOT_DIR/.coveragefilters" | sed 's/;$//')

reportgenerator \
  -reports:"$(tr '\n' ';' < "$ARTIFACTS_DIR/coverage-files.txt")" \
  -targetdir:"$REPORT_DIR" \
  -reporttypes:"Html;Cobertura" \
  -filefilters:"$filefilters"

if [ -f "$REPORT_DIR/Cobertura.xml" ]; then
  if grep -qE '<sources\s*/>|<sources[^>]*>\s*</sources>' "$REPORT_DIR/Cobertura.xml"; then
    ROOT_DIR="$ROOT_DIR" perl -0pi -e '
      BEGIN { $root = $ENV{ROOT_DIR} }
      s{<sources\s*/>}{<sources><source>$root</source></sources>}g;
      s{<sources></sources>}{<sources><source>$root</source></sources>}g;
    ' "$REPORT_DIR/Cobertura.xml"
  fi
  cp "$REPORT_DIR/Cobertura.xml" "$ARTIFACTS_DIR/coverage.cobertura.xml"
fi

echo ""
echo "HTML report created at $REPORT_HTML"
echo "XML report available at $ARTIFACTS_DIR/coverage.cobertura.xml"

if [ "$OPEN_REPORT" = true ]; then
  xdg-open "$REPORT_HTML" >/dev/null 2>&1 &
fi
