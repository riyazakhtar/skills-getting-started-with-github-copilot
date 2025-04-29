#!/usr/bin/env bash

api="./Api"
infrastructure="./Infrastructure"

toolrestore() {
  echo "Restoring tools..."
  dotnet tool restore
}

migrationsscript() {
  echo "Generating ./database.sql script..."
  dotnet ef migrations script -i -p "$infrastructure" -s "$api" -o database.sql
}

migrationsadd() {
  echo "Adding migration..."
  dotnet ef migrations add -p "$infrastructure" -s "$api" "$@"
}

migrationsremove() {
  echo "Removing migration..."
  dotnet ef migrations remove -p "$infrastructure" -s "$api" "$@"
}

databaseupdate() {
  echo "Updatind database..."
  dotnet ef database update -s "$api"
}

databaseseed() {
  echo "Seeding database..."
  dotnet ef database update -s "$api" -- --seed true
}

openapi() {
  echo "Generating openapi.json..."
  dotnet swagger tofile --output openapi.json "$api/bin/Debug/net8.0/$api.dll" v1
}

help() {
  local cyan="\e[0;36m"
  local reset="\e[0m"
  
  printf "${cyan}Usage:${reset}\n"
  printf "%-25s %s\n" "tool-restore" "Restore tools defined in the local tool manifest"
  printf "%-25s %s\n" "migrations-add <Name>" "Create a new migration"
  printf "%-25s %s\n" "migrations-remove" "Remove migration"
  printf "%-25s %s\n" "migrations-script" "Generate database script"
  printf "%-25s %s\n" "database-update" "Update database"
  printf "%-25s %s\n" "database-seed" "Seed database"
  printf "%-25s %s\n" "openapi" "Generate openapi specification"
}

main() {
  case "$1" in
    "tools-restore")
        shift
        toolrestore "$@"
        ;;
    "migrations-add")
        shift
        migrationsadd "$@"
        ;;
    "migrations-remove")
        shift
        migrationsremove "$@"
        ;;
    "migrations-script")
        shift
        migrationsscript "$@"
        ;;
    "database-update")
        shift
        databaseupdate "$@"
        ;;
    "database-seed")
        shift
        databaseseed "$@"
        ;;
    "openapi")
        shift
        openapi "$@"
        ;;
    "help")
        help
        ;;
    *)
        help
        ;;
  esac
}

main "$@"
