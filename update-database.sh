#!/bin/bash

MODULE=$1

if [ -z "$MODULE" ]; then
  echo "Usage: ./update-database.sh Module"
  exit 1
fi

STARTUP_PATH="./src/Tools/Migrations"
PROJECT_PATH="./src/Modules/$MODULE/Persistence"
CONTEXT="${MODULE}Context"

dotnet ef database update -s "$STARTUP_PATH" -p "$PROJECT_PATH" -c "$CONTEXT"
