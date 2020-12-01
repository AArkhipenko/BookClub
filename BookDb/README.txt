1. Create DbContext
2. Execute command for create migration strategy:
	Add-Migration Initial
3. Execute command for generate ORM classes:
	scaffold-dbcontext "Host=localhost;Database=bookclub;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -context BookDbContext -f