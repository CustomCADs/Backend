#!/bin/bash

MODULE=$1
MIGRATION=$2

if [[ -z "$MODULE" || -z "$MIGRATION" ]]; then 
  echo "Usage: ./add-migration.sh Module Migration"
  exit 1
fi

STARTUP_PATH="./src/Tools/Migrations"
PROJECT_PATH="./src/Modules/$MODULE/Persistence"
CONTEXT="${MODULE}Context"

dotnet ef migrations add "$MIGRATION" -s "$STARTUP_PATH" -p "$PROJECT_PATH" -c "$CONTEXT"
