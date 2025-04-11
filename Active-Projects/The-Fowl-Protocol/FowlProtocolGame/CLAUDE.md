# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands
- Build: `dotnet build`
- Run: `dotnet run --project FowlProtocolGame/FowlProtocolGame.csproj`
- Content Pipeline: `dotnet mgcb-editor Content/Content.mgcb`
- Clean: `dotnet clean`

## Code Style Guidelines
- Use C# modern syntax with top-level statements where appropriate
- Private fields use underscore prefix (_fieldName)
- Follow MonoGame standard lifecycle structure (Initialize, LoadContent, Update, Draw)
- Namespaces should match directory structure
- Group imports with Microsoft.Xna.Framework imports first
- Use descriptive variable/method names following PascalCase for public, camelCase for private
- For game entities, implement dedicated classes instead of handling all logic in Game1.cs
- When throwing exceptions, include context information in message
- Implement interfaces for common behaviors (IDrawable, IUpdateable, etc.)