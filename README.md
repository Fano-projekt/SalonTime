# SalonTime - Frisörsalongs Bokningssystem

Ett modernt bokningssystem för frisörsalonger byggt i ASP.NET Core MVC med SQLite databas.
## Funktioner

-  **Lista tjänster** - Se alla tillgängliga frisörtjänster
-  **Lägg till tjänster** - Admin kan lägga till nya tjänster
-  **Boka tid** - Kunder kan boka tider 
-  **Rabattsystem** - 10% rabatt eller 50 kr rabatt
-  **Admin panel** - Hantera alla bokningar och tjänster
-  **Unit Tests** - Testat rabattsystem med Strategy Pattern

## Teknikstack

- **C#** med .NET 9.0
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQLite** databas
- **xUnit** för testing
- **Strategy Pattern** för rabattsystem

### Förutsättningar

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio, Rider eller VS Code
## **Viktiga delar i denna README:**

### ** Alla nödvändiga kommandon:**
- `dotnet restore`
- `dotnet ef database update`
- `dotnet run`
- `dotnet test`

### Steg 1: Öppna projektet
Öppna mappen `SalonTime` i Rider eller din editor.

### Steg 2: Återställ paket
**dotnet restore:** 
- **dotnet ef database update:** `gör detta om det inte visas något i databsen`
- **dotnet run**

### ** Admin inloggning:**
- **URL:** `/Login`
- **Användarnamn:** `admin`
- **Lösenord:** `admin123`

### **Test instruktioner:**
- `cd SalonTim.Test`
- `dotnet test`

## Förklaring till Minneshantering
Projektet använder flera tekniker för effektiv minneshantering:

###  Async/Await
- Alla databasanrop använder `async/await`
- Frigör trådar under I/O-operationer
- Förbättrar skalbarhet

### Dependency Injection
- ASP.NET Core's built-in DI hanterar livscykeln för `DbContext`
- Automatisk disposal av resurser
- Förhindrar memory leaks



