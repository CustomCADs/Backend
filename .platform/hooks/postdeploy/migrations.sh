#!/bin/bash

echo "Running database migrations..."

CONTAINER_ID=$(docker ps -q | head -n 1)

echo "Container ID: $CONTAINER_ID"

if [ -n "$CONTAINER_ID" ]; then
    docker exec $CONTAINER_ID dotnet Tools.Migrations.dll --migrate-only
    if [ $? -eq 0 ]; then
        echo "Migrations applied successfully."
        
        echo "Running Identity tool..."
        docker exec $CONTAINER_ID dotnet Tools.Identity.dll --migrate-only
        if [ $? -eq 0 ]; then
            echo "Identity tool executed successfully."
        else
            echo "Identity tool execution failed." >&2
            exit 1
        fi
    else
        echo "Migration failed." >&2
        exit 1
    fi
else
    echo "No running container found." >&2
    exit 1
fi
