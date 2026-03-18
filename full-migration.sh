#!/bin/sh

echo "Application Migrations Start"
if dotnet /app/migrations/Tools.Migrations.dll --migrate-only || dotnet run --project ./src/Tools/Migrations --migrate-only; then
    echo "Application Migrations Success"
else 
    echo "Application Migrations Failed"
    exit 1
fi

echo "Identity Migrations Start"
if dotnet /app/identity/Tools.Identity.dll --migrate-only || dotnet run --project ./src/Tools/Identity --migrate-only; then
    echo "Identity Migrations Success"
else 
    echo "Identity Migrations Failed"
    exit 1
fi
