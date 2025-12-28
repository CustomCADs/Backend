#!/bin/bash

OPERATION=$1
MIGRATION=$2

if [ -z "$OPERATION" ]; then 
  echo "Usage: ./identity.sh [add <Migration> | remove | update]"
  exit 1
fi

STARTUP_PATH="./src/Tools/Identity"
PROJECT_PATH="./src/Modules/Identity/Infrastructure"
CONTEXT="IdentityContext"

case $OPERATION in
"add")
    if [ -z "$MIGRATION" ]; then
      echo "Error: Migration name required for 'add' operation."
      echo "Usage: ./identity.sh add <Migration>"
      exit 1
    fi
    dotnet ef migrations add "$MIGRATION" -s "$STARTUP_PATH" -p "$PROJECT_PATH" -c "$CONTEXT";;
"remove")
    dotnet ef migrations remove -s "$STARTUP_PATH" -p "$PROJECT_PATH" -c "$CONTEXT";;
"update")
    dotnet ef database update -s "$STARTUP_PATH" -p "$PROJECT_PATH" -c "$CONTEXT";;
*)
    echo "Usage: ./identity.sh [add <Migration> | remove | update]";;
esac
